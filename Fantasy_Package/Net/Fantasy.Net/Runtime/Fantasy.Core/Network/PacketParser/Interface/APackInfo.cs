using System;
using System.Buffers;
using System.IO;
using Fantasy.Helper;

namespace Fantasy.Core.Network
{
    /// <summary>
    /// ��������ݰ���Ϣ���࣬���ڴ洢�����õ������ݰ���Ϣ��
    /// </summary>
    public abstract class APackInfo : IDisposable
    {
        /// <summary>
        /// ���ݰ��� RPC ID��
        /// </summary>
        public uint RpcId;
        /// <summary>
        /// ���ݰ���·�� ID��
        /// </summary>
        public long RouteId;
        /// <summary>
        /// ���ݰ���Э���š�
        /// </summary>
        public uint ProtocolCode;
        /// <summary>
        /// ���ݰ���·�����ͱ��롣
        /// </summary>
        public long RouteTypeCode;
        /// <summary>
        /// ���ݰ���Ϣ��ĳ��ȡ�
        /// </summary>
        public int MessagePacketLength;
        /// <summary>
        /// �ڴ��������ߣ����ڴ洢���ݰ����ڴ����ݡ�
        /// </summary>
        public IMemoryOwner<byte> MemoryOwner;
        /// <summary>
        /// ��ȡһ��ֵ����ʾ�Ƿ��Ѿ����ͷš�
        /// </summary>
        public bool IsDisposed;

        /// <summary>
        /// �Ӷ�����л�ȡһ������Ϊ T ��δ�ͷ�ʵ����
        /// </summary>
        /// <typeparam name="T">Ҫ��ȡ��ʵ�����ͣ������� APackInfo �����ࡣ</typeparam>
        /// <returns>��ȡ��δ�ͷ�ʵ����</returns>
        public static T Rent<T>() where T : APackInfo
        {
            var aPackInfo = Pool<T>.Rent();
            aPackInfo.IsDisposed = false;
            return aPackInfo;
        }

        /// <summary>
        /// ����ָ�����ͷ����л���Ϣ�塣
        /// </summary>
        /// <param name="messageType">Ҫ�����л��ɵ����͡�</param>
        /// <returns>�����л��õ�����Ϣ�塣</returns>
        public abstract object Deserialize(Type messageType);
        /// <summary>
        /// ��������д�����ݰ���Ϣ����ڴ�����
        /// </summary>
        /// <returns>�������ڴ�����</returns>
        public abstract MemoryStream CreateMemoryStream();
        /// <summary>
        /// �ͷ���Դ�������ڴ��ȡ�
        /// </summary>
        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }
            
            RpcId = 0;
            RouteId = 0;
            ProtocolCode = 0;
            RouteTypeCode = 0;
            MessagePacketLength = 0;
            MemoryOwner.Dispose();
            MemoryOwner = null;
            IsDisposed = true;
        }
    }
}