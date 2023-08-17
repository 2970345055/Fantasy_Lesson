using System;
using System.Collections.Concurrent;
using System.Threading;
using Fantasy.Core.Network;
#pragma warning disable CS8765
#pragma warning disable CS8601
#pragma warning disable CS8618

namespace Fantasy
{
    /// <summary>
    /// һ�������߳�ͬ���������ġ�
    /// </summary>
    public sealed class ThreadSynchronizationContext : SynchronizationContext
    {
        /// <summary>
        /// ��ȡ�̵߳�Ψһ��ʶ����
        /// </summary>
        public readonly int ThreadId;
        private Action _actionHandler;
        private readonly ConcurrentQueue<Action> _queue = new();
        /// <summary>
        /// ��ȡ���̵߳�ͬ��������ʵ����
        /// </summary>
        public static ThreadSynchronizationContext Main { get; } = new(Environment.CurrentManagedThreadId);

        /// <summary>
        /// ��ʼ�� ThreadSynchronizationContext �����ʵ����
        /// </summary>
        /// <param name="threadId">�̵߳�Ψһ��ʶ����</param>
        public ThreadSynchronizationContext(int threadId)
        {
            ThreadId = threadId;
        }

        /// <summary>
        /// ����ͬ���������еĲ�����
        /// </summary>
        public void Update()
        {
            while (_queue.TryDequeue(out _actionHandler))
            {
                try
                {
                    _actionHandler();
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        /// <summary>
        /// �������Ŷ�����ͬ�����������첽ִ�С�
        /// </summary>
        /// <param name="callback">Ҫִ�еĻص�������</param>
        /// <param name="state">���ݸ��ص�������״̬����</param>
        public override void Post(SendOrPostCallback callback, object state)
        {
            Post(() => callback(state));
        }

        /// <summary>
        /// �������Ŷ�����ͬ�����������첽ִ�С�
        /// </summary>
        /// <param name="action">Ҫִ�еĲ�����</param>
        public void Post(Action action)
        {
            _queue.Enqueue(action);
        }
    }
}