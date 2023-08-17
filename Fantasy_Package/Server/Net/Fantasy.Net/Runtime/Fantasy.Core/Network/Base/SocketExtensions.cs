using System.Net.Sockets;

namespace Fantasy.Core.Network
{
    /// <summary>
    /// �ṩ��չ�����Բ��� Socket ��������С��
    /// </summary>
    public static class SocketExtensions
    {
        /// <summary>
        /// �� Socket ��������С����Ϊ����ϵͳ���ơ�
        /// </summary>
        /// <param name="socket">Ҫ���û�������С�� Socket��</param>
        public static void SetSocketBufferToOsLimit(this Socket socket)
        {
            socket.SetReceiveBufferToOSLimit();
            socket.SetSendBufferToOSLimit();
        }

        /// <summary>
        /// �� Socket ���ջ�������С����Ϊ����ϵͳ���ơ�
        /// �������ӽ��ջ�������С�Ĵ��� = Ĭ�� + ������� 100 MB��
        /// </summary>
        /// <param name="socket">Ҫ���ý��ջ�������С�� Socket��</param>
        /// <param name="stepSize">ÿ�����ӵĲ�����С��</param>
        /// <param name="attempts">�������ӻ�������С�Ĵ�����</param>
        public static void SetReceiveBufferToOSLimit(this Socket socket, int stepSize = 1024, int attempts = 100_000)
        {
            // setting a too large size throws a socket exception.
            // so let's keep increasing until we encounter it.
            for (int i = 0; i < attempts; ++i)
            {
                // increase in 1 KB steps
                try
                {
                    socket.ReceiveBufferSize += stepSize;
                }
                catch (SocketException)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// �� Socket ���ͻ�������С����Ϊ����ϵͳ���ơ�
        /// �������ӷ��ͻ�������С�Ĵ��� = Ĭ�� + ������� 100 MB��
        /// </summary>
        /// <param name="socket">Ҫ���÷��ͻ�������С�� Socket��</param>
        /// <param name="stepSize">ÿ�����ӵĲ�����С��</param>
        /// <param name="attempts">�������ӻ�������С�Ĵ�����</param>
        public static void SetSendBufferToOSLimit(this Socket socket, int stepSize = 1024, int attempts = 100_000)
        {
            // setting a too large size throws a socket exception.
            // so let's keep increasing until we encounter it.
            for (var i = 0; i < attempts; ++i)
            {
                // increase in 1 KB steps
                try
                {
                    socket.SendBufferSize += stepSize;
                }
                catch (SocketException)
                {
                    break;
                }
            }
        }
    }
}