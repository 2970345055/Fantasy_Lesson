namespace Fantasy.IO
{
    using System;

    /// <summary>
    /// �ṩ���ڹ���ɻ����ڴ����ķֲ��ࡣ
    /// </summary>
    public sealed partial class RecyclableMemoryStreamManager
    {
        /// <summary>
        /// ���� <see cref="StreamCreated"/> �¼��Ĳ����ࡣ
        /// </summary>
        public sealed class StreamCreatedEventArgs : EventArgs
        {
            /// <summary>
            /// Stream����Ψһ ID��
            /// </summary>
            public Guid Id { get; }

            /// <summary>
            /// ��ѡ���¼���ǩ��
            /// </summary>
            public string Tag { get; }

            /// <summary>
            /// ���������С��
            /// </summary>
            public long RequestedSize { get; }

            /// <summary>
            /// ʵ�ʵ�����С��
            /// </summary>
            public long ActualSize { get; }

            /// <summary>
            /// ��ʼ�� <see cref="StreamCreatedEventArgs"/> �����ʵ����
            /// </summary>
            /// <param name="guid">����Ψһ ID��</param>
            /// <param name="tag">���ı�ǩ��</param>
            /// <param name="requestedSize">���������С��</param>
            /// <param name="actualSize">ʵ�ʵ�����С��</param>
            public StreamCreatedEventArgs(Guid guid, string tag, long requestedSize, long actualSize)
            {
                this.Id = guid;
                this.Tag = tag;
                this.RequestedSize = requestedSize;
                this.ActualSize = actualSize;
            }
        }

        /// <summary>
        /// �ṩ���� <see cref="StreamDisposed"/> �¼��Ĳ����ࡣ
        /// </summary>
        public sealed class StreamDisposedEventArgs : EventArgs
        {
            /// <summary>
            /// ����Ψһ ID��
            /// </summary>
            public Guid Id { get; }

            /// <summary>
            /// ��ѡ���¼���ǩ��
            /// </summary>
            public string Tag { get; }

            /// <summary>
            /// �������Ķ�ջ
            /// </summary>
            public string AllocationStack { get; }

            /// <summary>
            ///�������Ķ�ջ��
            /// </summary>
            public string DisposeStack { get; }

            /// <summary>
            /// �����������ڡ�
            /// </summary>
            public TimeSpan Lifetime { get; }

            /// <summary>
            /// ��ʼ�� <see cref="StreamDisposedEventArgs"/> �����ʵ����
            /// </summary>
            /// <param name="guid">����Ψһ ID��</param>
            /// <param name="tag">���ı�ǩ��</param>
            /// <param name="allocationStack">ԭʼ����Ķ�ջ��</param>
            /// <param name="disposeStack">���ö�ջ��</param>
            [Obsolete("Use another constructor override")]
            public StreamDisposedEventArgs(Guid guid, string tag, string allocationStack, string disposeStack)
                :this(guid, tag, TimeSpan.Zero, allocationStack, disposeStack)
            {
                
            }

            /// <summary>
            /// ��ʼ�� <see cref="StreamDisposedEventArgs"/> �����ʵ����
            /// </summary>
            /// <param name="guid">����Ψһ ID��</param>
            /// <param name="tag">���ı�ǩ��</param>
            /// <param name="lifetime">�����������ڡ�</param>
            /// <param name="allocationStack">ԭʼ����Ķ�ջ��</param>
            /// <param name="disposeStack">���ö�ջ��</param>
            public StreamDisposedEventArgs(Guid guid, string tag, TimeSpan lifetime, string allocationStack, string disposeStack)
            {
                this.Id = guid;
                this.Tag = tag;
                this.Lifetime = lifetime;
                this.AllocationStack = allocationStack;
                this.DisposeStack = disposeStack;
            }
        }

        /// <summary>
        /// �ṩ���� <see cref="StreamDoubleDisposed"/> �¼��Ĳ����ࡣ
        /// </summary>
        public sealed class StreamDoubleDisposedEventArgs : EventArgs
        {
            /// <summary>
            /// ����Ψһ ID��
            /// </summary>
            public Guid Id { get; }

            /// <summary>
            /// ��ѡ���¼���ǩ��
            /// </summary>
            public string Tag { get; }

            /// <summary>
            /// �������Ķ�ջ��
            /// </summary>
            public string AllocationStack { get; }

            /// <summary>
            /// ��һ�����ö�ջ��
            /// </summary>
            public string DisposeStack1 { get; }

            /// <summary>
            /// �ڶ������ö�ջ��
            /// </summary>
            public string DisposeStack2 { get; }

            /// <summary>
            /// ��ʼ�� <see cref="StreamDoubleDisposedEventArgs"/> �����ʵ����
            /// </summary>
            /// <param name="guid">����Ψһ ID��</param>
            /// <param name="tag">���ı�ǩ��</param>
            /// <param name="allocationStack">ԭʼ����Ķ�ջ��</param>
            /// <param name="disposeStack1">��һ�����ö�ջ��</param>
            /// <param name="disposeStack2">�ڶ������ö�ջ��</param>
            public StreamDoubleDisposedEventArgs(Guid guid, string tag, string allocationStack, string disposeStack1, string disposeStack2)
            {
                this.Id = guid;
                this.Tag = tag;
                this.AllocationStack = allocationStack;
                this.DisposeStack1 = disposeStack1;
                this.DisposeStack2 = disposeStack2;
            }
        }

        /// <summary>
        /// �ṩ���� <see cref="StreamFinalized"/> �¼��Ĳ����ࡣ
        /// </summary>
        public sealed class StreamFinalizedEventArgs : EventArgs
        {
            /// <summary>
            /// ����Ψһ ID��
            /// </summary>
            public Guid Id { get; }

            /// <summary>
            /// ��ѡ���¼���ǩ��
            /// </summary>
            public string Tag { get; }

            /// <summary>
            /// �������Ķ�ջ��
            /// </summary>
            public string AllocationStack { get; }

            /// <summary>
            /// ��ʼ�� <see cref="StreamFinalizedEventArgs"/> �����ʵ����
            /// </summary>
            /// <param name="guid">����Ψһ ID��</param>
            /// <param name="tag">���ı�ǩ��</param>
            /// <param name="allocationStack">ԭʼ����Ķ�ջ��</param>
            public StreamFinalizedEventArgs(Guid guid, string tag, string allocationStack)
            {
                this.Id = guid;
                this.Tag = tag;
                this.AllocationStack = allocationStack;
            }
        }

        /// <summary>
        /// �ṩ���� <see cref="StreamConvertedToArray"/> �¼��Ĳ����ࡣ
        /// </summary>
        public sealed class StreamConvertedToArrayEventArgs : EventArgs
        {
            /// <summary>
            /// ����Ψһ ID��
            /// </summary>
            public Guid Id { get; }

            /// <summary>
            /// ��ѡ���¼���ǩ��
            /// </summary>
            public string Tag { get; }

            /// <summary>
            /// ���� ToArray �Ķ�ջ��
            /// </summary>
            public string Stack { get; }

            /// <summary>
            /// ��ջ�ĳ��ȡ�
            /// </summary>
            public long Length { get; }

            /// <summary>
            /// ��ʼ�� <see cref="StreamConvertedToArrayEventArgs"/> �����ʵ����
            /// </summary>
            /// <param name="guid">����Ψһ ID��</param>
            /// <param name="tag">���ı�ǩ��</param>
            /// <param name="stack">ToArray ���õĶ�ջ��</param>
            /// <param name="length">���ĳ��ȡ�</param>
            public StreamConvertedToArrayEventArgs(Guid guid, string tag, string stack, long length)
            {
                this.Id = guid;
                this.Tag = tag;
                this.Stack = stack;
                this.Length = length;
            }
        }

        /// <summary>
        /// �ṩ���� <see cref="StreamOverCapacity"/> �¼��Ĳ����ࡣ
        /// </summary>
        public sealed class StreamOverCapacityEventArgs : EventArgs
        {
            /// <summary>
            /// ����Ψһ ID��
            /// </summary>
            public Guid Id { get; }

            /// <summary>
            /// ��ѡ���¼���ǩ��
            /// </summary>
            public string Tag { get; }

            /// <summary>
            /// ԭʼ�����ջ��
            /// </summary>
            public string AllocationStack { get; }

            /// <summary>
            /// �����������
            /// </summary>
            public long RequestedCapacity { get; }

            /// <summary>
            /// ���������
            /// </summary>
            public long MaximumCapacity { get; }

            /// <summary>
            /// ��ʼ�� <see cref="StreamOverCapacityEventArgs"/> �����ʵ����
            /// </summary>
            /// <param name="guid">����Ψһ ID��</param>
            /// <param name="tag">���ı�ǩ��</param>
            /// <param name="requestedCapacity">�����������</param>
            /// <param name="maximumCapacity">�������������������</param>
            /// <param name="allocationStack">ԭʼ�����ջ��</param>
            internal StreamOverCapacityEventArgs(Guid guid, string tag, long requestedCapacity, long maximumCapacity, string allocationStack)
            {
                this.Id = guid;
                this.Tag = tag;
                this.RequestedCapacity = requestedCapacity;
                this.MaximumCapacity = maximumCapacity;
                this.AllocationStack = allocationStack;
            }
        }

        /// <summary>
        /// �ṩ���� <see cref="BlockCreated"/> �¼��Ĳ����ࡣ
        /// </summary>
        public sealed class BlockCreatedEventArgs : EventArgs
        {
            /// <summary>
            /// ��ǰ��С�ͳ���ʹ�õ��ֽ�����
            /// </summary>
            public long SmallPoolInUse { get; }

            /// <summary>
            /// ��ʼ�� <see cref="BlockCreatedEventArgs"/> �����ʵ����
            /// </summary>
            /// <param name="smallPoolInUse">��ǰ��С�ͳ���ʹ�õ��ֽ�����</param>
            internal BlockCreatedEventArgs(long smallPoolInUse)
            {
                this.SmallPoolInUse = smallPoolInUse;
            }
        }

        /// <summary>
        /// �ṩ���� <see cref="LargeBufferCreated"/> �¼��Ĳ����ࡣ
        /// </summary>
        public sealed class LargeBufferCreatedEventArgs : EventArgs
        {
            /// <summary>
            ///  ����Ψһ ID��
            /// </summary>
            public Guid Id { get; }

            /// <summary>
            /// ��ѡ���¼���ǩ��
            /// </summary>
            public string Tag { get; }

            /// <summary>
            /// �������Ƿ��������Գص�����
            /// </summary>
            public bool Pooled { get; }

            /// <summary>
            /// ����Ļ�������С��
            /// </summary>
            public long RequiredSize { get; }

            /// <summary>
            /// �Ӵ��ͳ��е�ǰʹ�õ��ֽ�����
            /// </summary>
            public long LargePoolInUse { get; }

            /// <summary>
            /// ���������δ�ӳ����������󣬲��� <see cref="GenerateCallStacks"/> �Ѵ򿪣�
            /// �������������ĵ��ö�ջ��
            /// </summary>
            public string CallStack { get; }

            /// <summary>
            /// ��ʼ�� <see cref="LargeBufferCreatedEventArgs"/> �����ʵ����
            /// </summary>
            /// <param name="guid">����Ψһ ID��</param>
            /// <param name="tag">���ı�ǩ��</param>
            /// <param name="requiredSize">�»������������С��</param>
            /// <param name="largePoolInUse">�Ӵ��ͳ��е�ǰʹ�õ��ֽ�����</param>
            /// <param name="pooled">�������Ƿ��������Գص�����</param>
            /// <param name="callStack">��������ĵ��ö�ջ�����δ�ӳ����������������� <see cref="GenerateCallStacks"/>����</param>
            internal LargeBufferCreatedEventArgs(Guid guid, string tag, long requiredSize, long largePoolInUse, bool pooled, string callStack)
            {
                this.RequiredSize = requiredSize;
                this.LargePoolInUse = largePoolInUse;
                this.Pooled = pooled;
                this.Id = guid;
                this.Tag = tag;
                this.CallStack = callStack;
            }
        }

        /// <summary>
        /// �ṩ���� <see cref="BufferDiscarded"/> �¼��Ĳ����ࡣ
        /// </summary>
        public sealed class BufferDiscardedEventArgs : EventArgs
        {
            /// <summary>
            /// ����Ψһ ID��
            /// </summary>
            public Guid Id { get; }

            /// <summary>
            /// ��ѡ���¼���ǩ��
            /// </summary>
            public string Tag { get; }

            /// <summary>
            /// �����������͡�
            /// </summary>
            public Events.MemoryStreamBufferType BufferType { get; }

            /// <summary>
            /// �����˻�������ԭ��
            /// </summary>
            public Events.MemoryStreamDiscardReason Reason { get; }

            /// <summary>
            /// ��ʼ�� <see cref="BufferDiscardedEventArgs"/> �����ʵ����
            /// </summary>
            /// <param name="guid">����Ψһ ID��</param>
            /// <param name="tag">���ı�ǩ��</param>
            /// <param name="bufferType">���ڶ����Ļ����������͡�</param>
            /// <param name="reason">������������ԭ��</param>
            internal BufferDiscardedEventArgs(Guid guid, string tag, Events.MemoryStreamBufferType bufferType, Events.MemoryStreamDiscardReason reason)
            {
                this.Id = guid;
                this.Tag = tag;
                this.BufferType = bufferType;
                this.Reason = reason;
            }
        }

        /// <summary>
        /// �ṩ���� <see cref="StreamLength"/> �¼��Ĳ����ࡣ
        /// </summary>
        public sealed class StreamLengthEventArgs : EventArgs
        {
            /// <summary>
            /// ���ĳ��ȡ�
            /// </summary>
            public long Length { get; }

            /// <summary>
            /// ��ʼ�� <see cref="StreamLengthEventArgs"/> �����ʵ����
            /// </summary>
            /// <param name="length">���ĳ��ȡ�</param>
            public StreamLengthEventArgs(long length)
            {
                this.Length = length;
            }
        }

        /// <summary>
        /// �ṩ���� <see cref="UsageReport"/> �¼��Ĳ����ࡣ
        /// </summary>
        public sealed class UsageReportEventArgs : EventArgs
        {
            /// <summary>
            /// ��ǰ����ʹ�õ�С�ͳ��ֽ�����
            /// </summary>
            public long SmallPoolInUseBytes { get; }

            /// <summary>
            /// ��ǰ���õ�С�ͳ��ֽ�����
            /// </summary>
            public long SmallPoolFreeBytes { get; }

            /// <summary>
            /// ��ǰ����ʹ�õĴ��ͳ��ֽ�����
            /// </summary>
            public long LargePoolInUseBytes { get; }

            /// <summary>
            /// ��ǰ���õĴ��ͳ��ֽ�����
            /// </summary>
            public long LargePoolFreeBytes { get; }

            /// <summary>
            /// ��ʼ�� <see cref="UsageReportEventArgs"/> �����ʵ����
            /// </summary>
            /// <param name="smallPoolInUseBytes">��ǰ����ʹ�õ�С�ͳ��ֽ�����</param>
            /// <param name="smallPoolFreeBytes">��ǰ���õ�С�ͳ��ֽ�����</param>
            /// <param name="largePoolInUseBytes">��ǰ����ʹ�õĴ��ͳ��ֽ�����</param>
            /// <param name="largePoolFreeBytes">��ǰ���õĴ��ͳ��ֽ�����</param>
            public UsageReportEventArgs(
                long smallPoolInUseBytes,
                long smallPoolFreeBytes,
                long largePoolInUseBytes,
                long largePoolFreeBytes)
            {
                this.SmallPoolInUseBytes = smallPoolInUseBytes;
                this.SmallPoolFreeBytes = smallPoolFreeBytes;
                this.LargePoolInUseBytes = largePoolInUseBytes;
                this.LargePoolFreeBytes = largePoolFreeBytes;
            }
        }
    }
}
