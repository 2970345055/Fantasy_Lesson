using System;
using System.Collections.Generic;

namespace Fantasy.DataStructure
{
    /// <summary>
    /// �ṩһ������Դ�ͷŹ��ܵ�ʵ���ֵ��֧࣬��ʹ�ö���ع���
    /// </summary>
    /// <typeparam name="TM">�ֵ��м������͡�</typeparam>
    /// <typeparam name="TN">�ֵ���ֵ�����ͣ�����ʵ�� IDisposable �ӿڡ�</typeparam>
    public sealed class EntityDictionary<TM, TN> : Dictionary<TM, TN>, IDisposable where TN : IDisposable where TM : notnull
    {
        private bool _isDispose;

        /// <summary>
        /// ����һ���µ� <see cref="EntityDictionary{TM, TN}"/> ʵ����
        /// </summary>
        /// <returns>�´�����ʵ����</returns>
        public static EntityDictionary<TM, TN> Create()
        {
            var entityDictionary = Pool<EntityDictionary<TM, TN>>.Rent();
            entityDictionary._isDispose = false;
            return entityDictionary;
        }

        /// <summary>
        /// ����ֵ��е����м�ֵ�ԣ����ͷ�ֵ����Դ��
        /// </summary>
        public new void Clear()
        {
            foreach (var keyValuePair in this)
            {
                keyValuePair.Value.Dispose();
            }

            base.Clear();
        }

        /// <summary>
        /// ����ֵ��е����м�ֵ�ԣ������ͷ�ֵ����Դ��
        /// </summary>
        public void ClearNotDispose()
        {
            base.Clear();
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
            Pool<EntityDictionary<TM, TN>>.Return(this);
        }
    }
}