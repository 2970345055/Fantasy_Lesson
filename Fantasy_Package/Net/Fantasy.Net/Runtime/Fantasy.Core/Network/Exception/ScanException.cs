using System;

namespace Fantasy.Core.Network
{
    /// <summary>
    /// ��ɨ������з������쳣��
    /// </summary>
    public class ScanException : Exception
    {
        /// <summary>
        /// ��ʼ�� <see cref="ScanException"/> �����ʵ����
        /// </summary>
        public ScanException() { }

        /// <summary>
        /// ʹ��ָ���Ĵ�����Ϣ��ʼ�� <see cref="ScanException"/> �����ʵ����
        /// </summary>
        /// <param name="msg">������Ϣ��</param>
        public ScanException(string msg) : base(msg) { }
    }
}