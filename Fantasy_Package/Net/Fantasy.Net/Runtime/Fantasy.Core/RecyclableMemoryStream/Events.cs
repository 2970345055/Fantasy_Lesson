// ---------------------------------------------------------------------
// Copyright (c) 2015 Microsoft
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// ---------------------------------------------------------------------

namespace Fantasy.IO
{
    using System;
    using System.Diagnostics.Tracing;

    /// <summary>
    /// �ṩ���ڹ���ɻ����ڴ����Ĺ��ܵĹ������ࡣ�˹��������𴴽��͹����ڴ��������ṩ�˶��ڴ����ͻ��յ�ϸ���ȿ��ƣ������̶ȵؼ����ڴ������������յĿ�����
    /// </summary>
    public sealed partial class RecyclableMemoryStreamManager
    {
        /// <summary>
        /// ���� RecyclableMemoryStream �� ETW �¼���
        /// </summary>
        [EventSource(Name = "Microsoft-IO-RecyclableMemoryStream", Guid = "{B80CD4E4-890E-468D-9CBA-90EB7C82DFC7}")]
        public sealed class Events : EventSource
        {
            /// <summary>
            /// ��̬��־����ͨ����д�������¼���
            /// </summary>
            public static Events Writer = new();

            /// <summary>
            /// ����������ö�١�
            /// </summary>
            public enum MemoryStreamBufferType
            {
                /// <summary>
                /// С�黺������
                /// </summary>
                Small,
                /// <summary>
                /// ��ػ�������
                /// </summary>
                Large
            }

            /// <summary>
            /// �����������Ŀ���ԭ��ö�١�
            /// </summary>
            public enum MemoryStreamDiscardReason
            {
                /// <summary>
                /// ������̫���޷����·�����С�
                /// </summary>
                TooLarge,
                /// <summary>
                /// �������㹻�Ŀ����ֽڡ�
                /// </summary>
                EnoughFree
            }

            /// <summary>
            /// �ڴ���������ʱ��¼���¼���
            /// </summary>
            /// <param name="guid">������Ψһ ID��</param>
            /// <param name="tag">��ʱ ID��ͨ����ʾ��ǰʹ�������</param>
            /// <param name="requestedSize">���������С��</param>
            /// <param name="actualSize">�ӳ��з��������ʵ�ʴ�С��</param>
            [Event(1, Level = EventLevel.Verbose, Version = 2)]
            public void MemoryStreamCreated(Guid guid, string tag, long requestedSize, long actualSize)
            {
                if (this.IsEnabled(EventLevel.Verbose, EventKeywords.None))
                {
                    WriteEvent(1, guid, tag ?? string.Empty, requestedSize, actualSize);
                }
            }

            /// <summary>
            /// �������ͷ�ʱ��¼���¼���
            /// </summary>
            /// <param name="guid">������Ψһ ID��</param>
            /// <param name="tag">��ʱ ID��ͨ����ʾ��ǰʹ�������</param>
            /// <param name="lifetimeMs">�����������ڣ����룩��</param>
            /// <param name="allocationStack">��ʼ����ĵ��ö�ջ��</param>
            /// <param name="disposeStack">�ͷŵĵ��ö�ջ��</param>
            [Event(2, Level = EventLevel.Verbose, Version = 3)]
            public void MemoryStreamDisposed(Guid guid, string tag, long lifetimeMs, string allocationStack, string disposeStack)
            {
                if (this.IsEnabled(EventLevel.Verbose, EventKeywords.None))
                {
                    WriteEvent(2, guid, tag ?? string.Empty, lifetimeMs, allocationStack ?? string.Empty, disposeStack ?? string.Empty);
                }
            }

            /// <summary>
            /// �����ڶ��α��ͷ�ʱ��¼���¼���
            /// </summary>
            /// <param name="guid">������Ψһ ID��</param>
            /// <param name="tag">��ʱ ID��ͨ����ʾ��ǰʹ�������</param>
            /// <param name="allocationStack">��ʼ����ĵ��ö�ջ��</param>
            /// <param name="disposeStack1">��һ���ͷŵĵ��ö�ջ��</param>
            /// <param name="disposeStack2">�ڶ����ͷŵĵ��ö�ջ��</param>
            /// <remarks>ע�⣺ֻ���� RecyclableMemoryStreamManager.GenerateCallStacks Ϊ true ʱ����ջ�Żᱻ��䡣</remarks>
            [Event(3, Level = EventLevel.Critical)]
            public void MemoryStreamDoubleDispose(Guid guid, string tag, string allocationStack, string disposeStack1,
                                                  string disposeStack2)
            {
                if (this.IsEnabled())
                {
                    this.WriteEvent(3, guid, tag ?? string.Empty, allocationStack ?? string.Empty,
                                    disposeStack1 ?? string.Empty, disposeStack2 ?? string.Empty);
                }
            }

            /// <summary>
            /// �������ս�ʱ��¼���¼���
            /// </summary>
            /// <param name="guid">������Ψһ ID��</param>
            /// <param name="tag">��ʱ ID��ͨ����ʾ��ǰʹ�������</param>
            /// <param name="allocationStack">��ʼ����ĵ��ö�ջ��</param>
            /// <remarks>ע�⣺ֻ���� RecyclableMemoryStreamManager.GenerateCallStacks Ϊ true ʱ����ջ�Żᱻ��䡣</remarks>
            [Event(4, Level = EventLevel.Error)]
            public void MemoryStreamFinalized(Guid guid, string tag, string allocationStack)
            {
                if (this.IsEnabled())
                {
                    WriteEvent(4, guid, tag ?? string.Empty, allocationStack ?? string.Empty);
                }
            }

            /// <summary>
            /// ������ ToArray ����������ʱ��¼���¼���
            /// </summary>
            /// <param name="guid">������Ψһ ID��</param>
            /// <param name="tag">��ʱ ID��ͨ����ʾ��ǰʹ�������</param>
            /// <param name="stack">ToArray �����ĵ��ö�ջ��</param>
            /// <param name="size">���ĳ��ȡ�</param>
            /// <remarks>ע�⣺ֻ���� RecyclableMemoryStreamManager.GenerateCallStacks Ϊ true ʱ����ջ�Żᱻ��䡣</remarks>
            [Event(5, Level = EventLevel.Verbose, Version = 2)]
            public void MemoryStreamToArray(Guid guid, string tag, string stack, long size)
            {
                if (this.IsEnabled(EventLevel.Verbose, EventKeywords.None))
                {
                    WriteEvent(5, guid, tag ?? string.Empty, stack ?? string.Empty, size);
                }
            }

            /// <summary>
            /// �� RecyclableMemoryStreamManager ����ʼ��ʱ��¼���¼���
            /// </summary>
            /// <param name="blockSize">��Ĵ�С�����ֽ�Ϊ��λ��</param>
            /// <param name="largeBufferMultiple">�󻺳����ı��������ֽ�Ϊ��λ��</param>
            /// <param name="maximumBufferSize">��󻺳�����С�����ֽ�Ϊ��λ��</param>
            [Event(6, Level = EventLevel.Informational)]
            public void MemoryStreamManagerInitialized(int blockSize, int largeBufferMultiple, int maximumBufferSize)
            {
                if (this.IsEnabled())
                {
                    WriteEvent(6, blockSize, largeBufferMultiple, maximumBufferSize);
                }
            }

            /// <summary>
            /// �������µĿ�ʱ��¼���¼���
            /// </summary>
            /// <param name="smallPoolInUseBytes">��ǰ��С�����ʹ�õ��ֽ�����</param>
            [Event(7, Level = EventLevel.Warning, Version = 2)]
            public void MemoryStreamNewBlockCreated(long smallPoolInUseBytes)
            {
                if (this.IsEnabled(EventLevel.Warning, EventKeywords.None))
                {
                    WriteEvent(7, smallPoolInUseBytes);
                }
            }

            /// <summary>
            /// �������µĴ󻺳���ʱ��¼���¼���
            /// </summary>
            /// <param name="requiredSize">����Ĵ�С��</param>
            /// <param name="largePoolInUseBytes">��ǰ�ڴ󻺳�������ʹ�õ��ֽ�����</param>
            [Event(8, Level = EventLevel.Warning, Version = 3)]
            public void MemoryStreamNewLargeBufferCreated(long requiredSize, long largePoolInUseBytes)
            {
                if (this.IsEnabled(EventLevel.Warning, EventKeywords.None))
                {
                    WriteEvent(8, requiredSize, largePoolInUseBytes);
                }
            }

            /// <summary>
            /// �������Ļ����������޷��������ʱ��¼���¼���
            /// </summary>
            /// <param name="guid">Ψһ���� ID��</param>
            /// <param name="tag">��ʱ ID��ͨ����ʾ��ǰʹ�������</param>
            /// <param name="requiredSize">����������Ĵ�С��</param>
            /// <param name="allocationStack">�������ĵ��ö�ջ��</param>
            /// <remarks>ע�⣺ֻ���� RecyclableMemoryStreamManager.GenerateCallStacks Ϊ true ʱ����ջ�Żᱻ��䡣</remarks>
            [Event(9, Level = EventLevel.Verbose, Version = 3)]
            public void MemoryStreamNonPooledLargeBufferCreated(Guid guid, string tag, long requiredSize, string allocationStack)
            {
                if (this.IsEnabled(EventLevel.Verbose, EventKeywords.None))
                {
                    WriteEvent(9, guid, tag ?? string.Empty, requiredSize, allocationStack ?? string.Empty);
                }
            }

            /// <summary>
            /// ��������������ʱ��¼���¼���û�зŻس��У����ǽ��� GC ������
            /// </summary>
            /// <param name="guid">Ψһ���� ID��</param>
            /// <param name="tag">��ʱ ID��ͨ����ʾ��ǰʹ�������</param>
            /// <param name="bufferType">�������Ļ��������͡�</param>
            /// <param name="reason">����ԭ��</param>
            /// <param name="smallBlocksFree">С����еĿ��п�����</param>
            /// <param name="smallPoolBytesFree">С����еĿ����ֽ�����</param>
            /// <param name="smallPoolBytesInUse">��С�����ʹ�õ��ֽ�����</param>
            /// <param name="largeBlocksFree">�󻺳������еĿ��п�����</param>
            /// <param name="largePoolBytesFree">�󻺳������еĿ����ֽ�����</param>
            /// <param name="largePoolBytesInUse">�Ӵ󻺳�������ʹ�õ��ֽ�����</param>
            [Event(10, Level = EventLevel.Warning, Version = 2)]
            public void MemoryStreamDiscardBuffer(Guid guid, string tag, MemoryStreamBufferType bufferType,
                                                  MemoryStreamDiscardReason reason, long smallBlocksFree, long smallPoolBytesFree, long smallPoolBytesInUse, long largeBlocksFree, long largePoolBytesFree, long largePoolBytesInUse)
            {
                if (this.IsEnabled(EventLevel.Warning, EventKeywords.None))
                {
                    WriteEvent(10, guid, tag ?? string.Empty, bufferType, reason, smallBlocksFree, smallPoolBytesFree, smallPoolBytesInUse, largeBlocksFree, largePoolBytesFree, largePoolBytesInUse);
                }
            }

            /// <summary>
            /// �����������������ֵʱ��¼���¼���
            /// </summary>
            /// <param name="guid">Ψһ���� ID��</param>
            /// <param name="requestedCapacity">�����������</param>
            /// <param name="maxCapacity">����������� RecyclableMemoryStreamManager ���á�</param>
            /// <param name="tag">��ʱ ID��ͨ����ʾ��ǰʹ�������</param>
            /// <param name="allocationStack">��������ĵ��ö�ջ��</param>
            /// <remarks>ע�⣺ֻ���� RecyclableMemoryStreamManager.GenerateCallStacks Ϊ true ʱ����ջ�Żᱻ��䡣</remarks>
            [Event(11, Level = EventLevel.Error, Version = 3)]
            public void MemoryStreamOverCapacity(Guid guid, string tag, long requestedCapacity, long maxCapacity, string allocationStack)
            {
                if (this.IsEnabled())
                {
                    WriteEvent(11, guid, tag ?? string.Empty, requestedCapacity, maxCapacity, allocationStack ?? string.Empty);
                }
            }
        }
    }
}
