using System;
using System.Net;
#pragma warning disable CS8625
#pragma warning disable CS8618

namespace Fantasy.Core.Network
{
    /// <summary>
    /// �����Network���������
    /// </summary>
    public sealed class ServerNetworkComponent : Entity, INotSupportedPool
    {
        /// <summary>
        /// ��ȡ�����ķ����Network����ʵ����
        /// </summary>
        public ANetwork Network { get; private set; }

        /// <summary>
        /// ��ʼ����������������
        /// </summary>
        /// <param name="networkProtocolType">����Э�����͡�</param>
        /// <param name="networkTarget">����Ŀ�ꡣ</param>
        /// <param name="address">�󶨵�IP��ַ�Ͷ˿ڡ�</param>
        public void Initialize(NetworkProtocolType networkProtocolType, NetworkTarget networkTarget, IPEndPoint address)
        {
            switch (networkProtocolType)
            {
                case NetworkProtocolType.KCP:
                {
                    Network = new KCPServerNetwork(Scene, networkTarget, address);
                    Log.Info($"NetworkProtocol:KCP IPEndPoint:{address}");
                    return;
                }
                case NetworkProtocolType.TCP:
                {
                    Network = new TCPServerNetwork(Scene, networkTarget, address);
                    Log.Info($"NetworkProtocol:TCP IPEndPoint:{address}");
                    return;
                }
                default:
                {
                    throw new NotSupportedException($"Unsupported NetworkProtocolType:{networkProtocolType}");
                }
            }
        }

        /// <summary>
        /// �ͷŷ���������������������Դ��
        /// </summary>
        public override void Dispose()
        {
            if (Network != null)
            {
                Network.Dispose();
                Network = null;
            }
            
            base.Dispose();
        }
    }
}