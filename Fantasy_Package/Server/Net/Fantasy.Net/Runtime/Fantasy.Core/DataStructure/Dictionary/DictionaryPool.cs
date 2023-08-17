using System;
using System.Collections.Generic;

namespace Fantasy.DataStructure
{
    /// <summary>
    /// �ṩһ������ʹ�ö���ع�����ֵ��ࡣ
    /// </summary>
    /// <typeparam name="TM">�ֵ��м������͡�</typeparam>
    /// <typeparam name="TN">�ֵ���ֵ�����͡�</typeparam>
    public sealed class DictionaryPool<TM, TN> : Dictionary<TM, TN>, IDisposable where TM : notnull
    {
        private bool _isDispose;

        /// <summary>
        /// �ͷ�ʵ��ռ�õ���Դ��
        /// </summary>
        public void Dispose()
        {
            if (_isDispose)
            {
                return;
            }

            _isDispose = true;
            Clear();
            Pool<DictionaryPool<TM, TN>>.Return(this);
        }

        /// <summary>
        /// ����һ���µ� <see cref="DictionaryPool{TM, TN}"/> ʵ����
        /// </summary>
        /// <returns>�´�����ʵ����</returns>
        public static DictionaryPool<TM, TN> Create()
        {
            var dictionary = Pool<DictionaryPool<TM, TN>>.Rent();
            dictionary._isDispose = false;
            return dictionary;
        }
    }
}