using System;
using System.IO;
using System.Net;
#pragma warning disable CS8625
#pragma warning disable CS8618

namespace Fantasy.Core.Network
{
    /// <summary>
    /// ���������ͨ�����ࡣ
    /// </summary>
    public abstract class ANetworkChannel
    {
        /// <summary>
        /// ��ȡͨ����Ψһ��ʶ ID��
        /// </summary>
        public uint Id { get; private set; }
        /// <summary>
        /// ��ȡ������ͨ�������ĳ�����
        /// </summary>
        public Scene Scene { get; protected set; }
        /// <summary>
        /// ��ȡͨ������������ ID��
        /// </summary>
        public long NetworkId { get; private set; }
        /// <summary>
        /// ��ȡͨ���Ƿ��Ѿ����ͷš�
        /// </summary>
        public bool IsDisposed { get; protected set; }
        /// <summary>
        /// ��ȡͨ����Զ���ն˵㡣
        /// </summary>
        public EndPoint RemoteEndPoint { get; protected set; }
        /// <summary>
        /// ��ȡͨ�������ݰ���������
        /// </summary>
        public APacketParser PacketParser { get; protected set; }

        /// <summary>
        /// ��ͨ�����ͷ�ʱ�������¼���
        /// </summary>
        public abstract event Action OnDispose;
        /// <summary>
        /// ��ͨ�����յ��ڴ������ݰ�ʱ�������¼���
        /// </summary>
        public abstract event Action<APackInfo> OnReceiveMemoryStream;

        /// <summary>
        /// ��ʼ����������ͨ���������ʵ����
        /// </summary>
        /// <param name="scene">ͨ�������ĳ�����</param>
        /// <param name="id">ͨ����Ψһ��ʶ ID��</param>
        /// <param name="networkId">ͨ������������ ID��</param>
        protected ANetworkChannel(Scene scene, uint id, long networkId)
        {
            Id = id;
            Scene = scene;
            NetworkId = networkId;
        }

        /// <summary>
        /// �ͷ�ͨ����Դ��
        /// </summary>
        public virtual void Dispose()
        {
            NetworkThread.Instance.RemoveChannel(NetworkId, Id);
            
            Id = 0;
            Scene = null;
            NetworkId = 0;
            IsDisposed = true;
            RemoteEndPoint = null;

            if (PacketParser != null)
            {
                PacketParser.Dispose();
                PacketParser = null;
            }
        }
    }
}