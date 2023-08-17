using System;
using System.Collections.Generic;

namespace Fantasy.DataStructure
{
    /// <summary>
    /// ���ͷŵĹ�ϣ���϶���ء�
    /// </summary>
    /// <typeparam name="T">��ϣ������Ԫ�ص����͡�</typeparam>
    public sealed class HashSetPool<T> : HashSet<T>, IDisposable
    {
        private bool _isDispose;

        /// <summary>
        /// �ͷ�ʵ����ռ�õ���Դ������ʵ�����ص�������У��Ա����á�
        /// </summary>
        public void Dispose()
        {
            if (_isDispose)
            {
                return;
            }

            _isDispose = true;
            Clear();
            Pool<HashSetPool<T>>.Return(this);
        }

        /// <summary>
        /// ����һ�� <see cref="HashSetPool{T}"/> ��ϣ���ϳص�ʵ����
        /// </summary>
        /// <returns>������ʵ����</returns>
        public static HashSetPool<T> Create()
        {
            var list = Pool<HashSetPool<T>>.Rent();
            list._isDispose = false;
            return list;
        }
    }

    /// <summary>
    /// ������ϣ���϶���أ����Գ���ʵ�ʵĹ�ϣ���ϡ�
    /// </summary>
    /// <typeparam name="T">��ϣ������Ԫ�ص����͡�</typeparam>
    public sealed class HashSetBasePool<T> : IDisposable
    {
        /// <summary>
        /// �洢ʵ�ʵĹ�ϣ����
        /// </summary>
        public HashSet<T> Set = new HashSet<T>();

        /// <summary>
        /// ����һ�� <see cref="HashSetBasePool{T}"/> ������ϣ���϶���ص�ʵ����
        /// </summary>
        /// <returns>������ʵ����</returns>
        public static HashSetBasePool<T> Create()
        {
            return Pool<HashSetBasePool<T>>.Rent();
        }

        /// <summary>
        /// �ͷ�ʵ����ռ�õ���Դ������ʵ�����ص�������У��Ա����á�
        /// </summary>
        public void Dispose()
        {
            Set.Clear();
            Pool<HashSetBasePool<T>>.Return(this);
        }
    }
}