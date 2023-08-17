using System;
using System.Collections.Generic;

namespace Fantasy.DataStructure
{
    /// <summary>
    /// �ṩһ���������õ��ֵ��֧࣬��ʹ�ö���ع���
    /// </summary>
    /// <typeparam name="TM">�ֵ��м������͡�</typeparam>
    /// <typeparam name="TN">�ֵ���ֵ�����͡�</typeparam>
    public sealed class ReuseDictionary<TM, TN> : Dictionary<TM, TN>, IDisposable where TM : notnull
    {
        private bool _isDispose;

        /// <summary>
        /// ����һ���µ� <see cref="ReuseDictionary{TM, TN}"/> ʵ����
        /// </summary>
        /// <returns>�´�����ʵ����</returns>
        public static ReuseDictionary<TM, TN> Create()
        {
            var entityDictionary = Pool<ReuseDictionary<TM, TN>>.Rent();
            entityDictionary._isDispose = false;
            return entityDictionary;
        }

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
            Pool<ReuseDictionary<TM, TN>>.Return(this);
        }
    }
}