using System;
using System.Collections.Generic;

namespace Fantasy.DataStructure
{
    /// <summary>
    /// �����õ��б��̳��� <see cref="List{T}"/> �ࡣ����֧��ͨ������������б�ʵ�����Լ��ٶ��������ͷŵĿ�����
    /// </summary>
    /// <typeparam name="T">�б���Ԫ�ص����͡�</typeparam>
    public sealed class ReuseList<T> : List<T>, IDisposable
    {
        private bool _isDispose;

        /// <summary>
        /// ����һ�� <see cref="ReuseList{T}"/> �����õ��б��ʵ����
        /// </summary>
        /// <returns>������ʵ����</returns>
        public static ReuseList<T> Create()
        {
            var list = Pool<ReuseList<T>>.Rent();
            list._isDispose = false;
            return list;
        }

        /// <summary>
        /// �ͷŸ�ʵ����ռ�õ���Դ������ʵ�����ص�������У��Ա����á�
        /// </summary>
        public void Dispose()
        {
            if (_isDispose)
            {
                return;
            }

            _isDispose = true;
            Clear();
            Pool<ReuseList<T>>.Return(this);
        }
    }
}