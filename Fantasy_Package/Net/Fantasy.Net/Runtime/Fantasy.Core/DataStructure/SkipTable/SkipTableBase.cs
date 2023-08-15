using System;
using System.Collections;
using System.Collections.Generic;
#pragma warning disable CS8601
#pragma warning disable CS8603
#pragma warning disable CS8625
#pragma warning disable CS8604

namespace Fantasy.DataStructure
{
    /// <summary>
    /// �����������࣬�ṩ����Ļ������ܺͲ�����
    /// </summary>
    /// <typeparam name="TValue">�����д洢��ֵ�����͡�</typeparam>
    public abstract class SkipTableBase<TValue> : IEnumerable<SkipTableNode<TValue>>
    {
        /// <summary>
        /// �����������
        /// </summary>
        public readonly int MaxLayer;
        /// <summary>
        /// ����Ķ���ͷ�ڵ�
        /// </summary>
        public readonly SkipTableNode<TValue> TopHeader;
        /// <summary>
        /// ����ĵײ�ͷ�ڵ�
        /// </summary>
        public SkipTableNode<TValue> BottomHeader;
        /// <summary>
        /// �����нڵ��������ʹ���� Node �ֵ�ļ���
        /// </summary>
        public int Count => Node.Count;
        /// <summary>
        /// ��������������������������
        /// </summary>
        protected readonly Random Random = new Random();
        /// <summary>
        /// �洢����ڵ���ֵ�
        /// </summary>
        protected readonly Dictionary<long, SkipTableNode<TValue>> Node = new();
        /// <summary>
        /// ���ڸ���������ҵ�ջ
        /// </summary>
        protected readonly Stack<SkipTableNode<TValue>> AntiFindStack = new Stack<SkipTableNode<TValue>>();

        /// <summary>
        /// ��ʼ��һ���µ�����ʵ����
        /// </summary>
        /// <param name="maxLayer">�������������Ĭ��Ϊ 8��</param>
        protected SkipTableBase(int maxLayer = 8)
        {
            MaxLayer = maxLayer;
            var cur = TopHeader = new SkipTableNode<TValue>(long.MinValue, 0, 0, default, 0, null, null, null);

            for (var layer = MaxLayer - 1; layer >= 1; --layer)
            {
                cur.Down = new SkipTableNode<TValue>(long.MinValue, 0, 0, default, 0, null, null, null);
                cur = cur.Down;
            }

            BottomHeader = cur;
        }

        /// <summary>
        /// ��ȡָ�����Ľڵ��ֵ�����������򷵻�Ĭ��ֵ��
        /// </summary>
        /// <param name="key">Ҫ���ҵļ���</param>
        public TValue this[long key] => !TryGetValueByKey(key, out TValue value) ? default : value;

        /// <summary>
        /// ��ȡָ�����Ľڵ��������е�������
        /// </summary>
        /// <param name="key">Ҫ���ҵļ���</param>
        /// <returns>�ڵ��������</returns>
        public int GetRanking(long key)
        {
            if (!Node.TryGetValue(key, out var node))
            {
                return 0;
            }

            return node.Index;
        }

        /// <summary>
        /// ��ȡָ�����ķ������������ڱȸü�����Ľڵ��е�������
        /// </summary>
        /// <param name="key">Ҫ���ҵļ���</param>
        /// <returns>����������</returns>
        public int GetAntiRanking(long key)
        {
            var ranking = GetRanking(key);

            if (ranking == 0)
            {
                return 0;
            }

            return Count + 1 - ranking;
        }

        /// <summary>
        /// ����ͨ������ȡ�ڵ��ֵ��
        /// </summary>
        /// <param name="key">Ҫ���ҵļ���</param>
        /// <param name="value">��ȡ���Ľڵ��ֵ���������������ΪĬ��ֵ��</param>
        /// <returns>�Ƿ�ɹ���ȡ�ڵ��ֵ��</returns>
        public bool TryGetValueByKey(long key, out TValue value)
        {
            if (!Node.TryGetValue(key, out var node))
            {
                value = default;
                return false;
            }

            value = node.Value;
            return true;
        }

        /// <summary>
        /// ����ͨ������ȡ�ڵ㡣
        /// </summary>
        /// <param name="key">Ҫ���ҵļ���</param>
        /// <param name="node">��ȡ���Ľڵ㣬�������������Ϊ <c>null</c>��</param>
        /// <returns>�Ƿ�ɹ���ȡ�ڵ㡣</returns>
        public bool TryGetNodeByKey(long key, out SkipTableNode<TValue> node)
        {
            if (Node.TryGetValue(key, out node))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// �������в��ҽڵ㣬���ش���ʼλ�õ�����λ�õĽڵ��б�
        /// </summary>
        /// <param name="start">��ʼλ�õ�������</param>
        /// <param name="end">����λ�õ�������</param>
        /// <param name="list">���ڴ洢�ڵ��б�� <see cref="ListPool{T}"/> ʵ����</param>
        public void Find(int start, int end, ListPool<SkipTableNode<TValue>> list)
        {
            var cur = BottomHeader;
            var count = end - start;

            for (var i = 0; i < start; i++)
            {
                cur = cur.Right;
            }

            for (var i = 0; i <= count; i++)
            {
                if (cur == null)
                {
                    break;
                }

                list.Add(cur);
                cur = cur.Right;
            }
        }

        /// <summary>
        /// �������н��з�����ҽڵ㣬���شӽ���λ�õ���ʼλ�õĽڵ��б�
        /// </summary>
        /// <param name="start">����λ�õ�������</param>
        /// <param name="end">��ʼλ�õ�������</param>
        /// <param name="list">���ڴ洢�ڵ��б�� <see cref="ListPool{T}"/> ʵ����</param>
        public void AntiFind(int start, int end, ListPool<SkipTableNode<TValue>> list)
        {
            var cur = BottomHeader;
            start = Count + 1 - start;
            end = start - end;

            for (var i = 0; i < start; i++)
            {
                cur = cur.Right;

                if (cur == null)
                {
                    break;
                }

                if (i < end)
                {
                    continue;
                }

                AntiFindStack.Push(cur);
            }

            while (AntiFindStack.TryPop(out var node))
            {
                list.Add(node);
            }
        }

        /// <summary>
        /// ��ȡ���������һ���ڵ��ֵ��
        /// </summary>
        /// <returns>���һ���ڵ��ֵ��</returns>
        public TValue GetLastValue()
        {
            var cur = TopHeader;

            while (cur.Right != null || cur.Down != null)
            {
                while (cur.Right != null)
                {
                    cur = cur.Right;
                }

                if (cur.Down != null)
                {
                    cur = cur.Down;
                }
            }

            return cur.Value;
        }

        /// <summary>
        /// �Ƴ�������ָ�����Ľڵ㡣
        /// </summary>
        /// <param name="key">Ҫ�Ƴ��Ľڵ�ļ���</param>
        /// <returns>�Ƴ��Ƿ�ɹ���</returns>
        public bool Remove(long key)
        {
            if (!Node.TryGetValue(key, out var node))
            {
                return false;
            }

            return Remove(node.SortKey, node.ViceKey, key, out _);
        }

        /// <summary>
        /// ����������ӽڵ㡣
        /// </summary>
        /// <param name="sortKey">�ڵ���������</param>
        /// <param name="viceKey">�ڵ�ĸ�����</param>
        /// <param name="key">�ڵ�ļ���</param>
        /// <param name="value">�ڵ��ֵ��</param>
        public abstract void Add(long sortKey, long viceKey, long key, TValue value);

        /// <summary>
        /// ���������Ƴ�ָ�����Ľڵ㡣
        /// </summary>
        /// <param name="sortKey">�ڵ���������</param>
        /// <param name="viceKey">�ڵ�ĸ�����</param>
        /// <param name="key">�ڵ�ļ���</param>
        /// <param name="value">���Ƴ��Ľڵ��ֵ��</param>
        /// <returns>�Ƴ��Ƿ�ɹ���</returns>
        public abstract bool Remove(long sortKey, long viceKey, long key, out TValue value);

        /// <summary>
        /// ����һ��ö���������ڱ��������еĽڵ㡣
        /// </summary>
        /// <returns>һ�������ڱ�������ڵ��ö������</returns>
        public IEnumerator<SkipTableNode<TValue>> GetEnumerator()
        {
            var cur = BottomHeader.Right;
            while (cur != null)
            {
                yield return cur;
                cur = cur.Right;
            }
        }

        /// <summary>
        /// ����һ���Ƿ���ö���������ڱ��������еĽڵ㡣
        /// </summary>
        /// <returns>һ���Ƿ���ö�����������ڱ�������ڵ㡣</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}