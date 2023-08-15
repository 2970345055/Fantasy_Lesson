using System;
using System.Collections.Generic;

namespace Fantasy.Core
{
    /// <summary>
    /// ָ������Э�����������ͣ����ڹ���ͬ���͵�Э�������С�
    /// </summary>
    public sealed class CoroutineLockQueueType
    {
        /// <summary>
        /// ��ȡЭ�����������͵����ơ�
        /// </summary>
        public readonly string Name;
        private readonly Dictionary<long, CoroutineLockQueue> _coroutineLockQueues = new Dictionary<long, CoroutineLockQueue>();

        // ˽�й��캯������ֹ�ⲿʵ����
        private CoroutineLockQueueType() { }

        /// <summary>
        /// ��ʼ��Э�����������͵�ʵ����
        /// </summary>
        /// <param name="name">Э�����������͵����ơ�</param>
        public CoroutineLockQueueType(string name)
        {
            Name = name;
        }

        /// <summary>
        /// ����Э��������ȡָ������Э������
        /// </summary>
        /// <param name="key">Э�������еļ���</param>
        /// <param name="tag">����ʶ��</param>
        /// <param name="time">�ȴ�ʱ�䡣</param>
        /// <returns>�ȴ�Э����������</returns>
        public async FTask<WaitCoroutineLock> Lock(long key, string tag = null, int time = 10000)
        {
            if (_coroutineLockQueues.TryGetValue(key, out var coroutineLockQueue))
            {
                return await coroutineLockQueue.Lock(tag,time);
            }

            coroutineLockQueue = CoroutineLockQueue.Create(key, time, this);
            _coroutineLockQueues.Add(key, coroutineLockQueue);
            return WaitCoroutineLock.Create(coroutineLockQueue, tag, time);
        }

        /// <summary>
        /// ��Э���������������Ƴ�ָ������Э�������С�
        /// </summary>
        /// <param name="key">Ҫ�Ƴ���Э�������еļ���</param>
        public void Remove(long key)
        {
            if (_coroutineLockQueues.Remove(key, out var coroutineLockQueue))
            {
                coroutineLockQueue.Dispose();
            }
        }
    }
}