namespace Fantasy.DataStructure
{
    /// <summary>
    /// ��Ծ��ڵ㡣
    /// </summary>
    /// <typeparam name="TValue">�ڵ��ֵ�����͡�</typeparam>
    public class SkipTableNode<TValue>
    {
        /// <summary>
        /// �ڵ�����Ծ���е�������
        /// </summary>
        public int Index;
        /// <summary>
        /// �ڵ��������
        /// </summary>
        public long Key;
        /// <summary>
        /// �ڵ���������
        /// </summary>
        public long SortKey;
        /// <summary>
        /// �ڵ�ĸ�����
        /// </summary>
        public long ViceKey;
        /// <summary>
        /// �ڵ�洢��ֵ��
        /// </summary>
        public TValue Value;
        /// <summary>
        /// ָ�����ڵ�����á�
        /// </summary>
        public SkipTableNode<TValue> Left;
        /// <summary>
        /// ָ���Ҳ�ڵ�����á�
        /// </summary>
        public SkipTableNode<TValue> Right;
        /// <summary>
        /// ָ����һ��ڵ�����á�
        /// </summary>
        public SkipTableNode<TValue> Down;

        /// <summary>
        /// ��ʼ����Ծ��ڵ����ʵ����
        /// </summary>
        /// <param name="sortKey">�ڵ���������</param>
        /// <param name="viceKey">�ڵ�ĸ�����</param>
        /// <param name="key">�ڵ��������</param>
        /// <param name="value">�ڵ�洢��ֵ��</param>
        /// <param name="index">�ڵ�����Ծ���е�������</param>
        /// <param name="l">ָ�����ڵ�����á�</param>
        /// <param name="r">ָ���Ҳ�ڵ�����á�</param>
        /// <param name="d">ָ����һ��ڵ�����á�</param>
        public SkipTableNode(long sortKey, long viceKey, long key, TValue value, int index,
            SkipTableNode<TValue> l,
            SkipTableNode<TValue> r,
            SkipTableNode<TValue> d)
        {
            Left = l;
            Right = r;
            Down = d;
            Value = value;
            Key = key;
            Index = index;
            SortKey = sortKey;
            ViceKey = viceKey;
        }
    }
}