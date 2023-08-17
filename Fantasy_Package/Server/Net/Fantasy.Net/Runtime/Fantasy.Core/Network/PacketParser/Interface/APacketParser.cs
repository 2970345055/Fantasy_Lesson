using System;
using System.Buffers;
using System.IO;
using Fantasy.DataStructure;
using Fantasy.Helper;

namespace Fantasy.Core.Network
{
    /// <summary>
    /// ����İ����������࣬���ڽ�������ͨ�����ݰ���
    /// </summary>
    public abstract class APacketParser : IDisposable
    {
        /// <summary>
        /// �ڴ�أ����ڷ����ڴ�顣
        /// </summary>
        protected MemoryPool<byte> MemoryPool;
        /// <summary>
        /// ��ȡһ��ֵ����ʾ�Ƿ��Ѿ����ͷš�
        /// </summary>
        protected bool IsDisposed { get; private set; }

        /// <summary>
        /// ��������Ŀ�괴����Ӧ�İ�������ʵ����
        /// </summary>
        /// <param name="networkTarget">����Ŀ�ָ꣬ʾ���ڲ�����ͨ�Ż����ⲿ����ͨ�š�</param>
        /// <returns>�����İ�������ʵ����</returns>
        public static APacketParser CreatePacketParser(NetworkTarget networkTarget)
        {
            switch (networkTarget)
            {
                case NetworkTarget.Inner:
                {
#if FANTASY_NET
                    return new InnerPacketParser();
#else
                    throw new NotSupportedException($"PacketParserHelper Create NotSupport {networkTarget}");
#endif
                }
                case NetworkTarget.Outer:
                {
                    return new OuterPacketParser();
                }
                default:
                {
                    throw new NotSupportedException($"PacketParserHelper Create NotSupport {networkTarget}");
                }
            }
        }

        /// <summary>
        /// ��ѭ���������������ݰ���
        /// </summary>
        /// <param name="buffer">ѭ����������</param>
        /// <param name="packInfo">�����õ������ݰ���Ϣ��</param>
        /// <returns>����ɹ��������ݰ����򷵻� true�����򷵻� false��</returns>
        public abstract bool UnPack(CircularBuffer buffer, out APackInfo packInfo);
        /// <summary>
        /// ���ڴ��������ݰ���
        /// </summary>
        /// <param name="memoryOwner">�ڴ��������ߡ�</param>
        /// <param name="packInfo">�����õ������ݰ���Ϣ��</param>
        /// <returns>����ɹ��������ݰ����򷵻� true�����򷵻� false��</returns>
        public abstract bool UnPack(IMemoryOwner<byte> memoryOwner, out APackInfo packInfo);
        /// <summary>
        /// �ͷ���Դ�������ڴ�صȡ�
        /// </summary>
        public virtual void Dispose()
        {
            IsDisposed = true;
            MemoryPool.Dispose();
        }
    }
}