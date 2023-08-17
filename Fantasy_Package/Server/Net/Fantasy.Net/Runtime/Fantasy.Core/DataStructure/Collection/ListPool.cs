using System;
using System.Collections.Generic;

namespace Fantasy.DataStructure
{
    /// <summary>
    /// ���ͷŵ��б�List������ء�
    /// </summary>
    /// <typeparam name="T">�б���Ԫ�ص����͡�</typeparam>
    public sealed class ListPool<T> : List<T>, IDisposable
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
            Pool<ListPool<T>>.Return(this);
        }

        /// <summary>
        /// ʹ��ָ����Ԫ�ش���һ�� <see cref="ListPool{T}"/> �б�List������ص�ʵ����
        /// </summary>
        /// <param name="args">Ҫ��ӵ��б��Ԫ�ء�</param>
        /// <returns>������ʵ����</returns>
        public static ListPool<T> Create(params T[] args)
        {
            var list = Pool<ListPool<T>>.Rent();
            list._isDispose = false;
            if (args != null) list.AddRange(args);
            return list;
        }

        /// <summary>
        /// ʹ��ָ�����б���һ�� <see cref="ListPool{T}"/> �б�List������ص�ʵ����
        /// </summary>
        /// <param name="args">Ҫ��ӵ��б��Ԫ���б�</param>
        /// <returns>������ʵ����</returns>
        public static ListPool<T> Create(List<T> args)
        {
            var list = Pool<ListPool<T>>.Rent();
            list._isDispose = false;
            if (args != null) list.AddRange(args);
            return list;
        }
    }
}