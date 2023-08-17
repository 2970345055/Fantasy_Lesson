using System;
using System.Collections.Generic;

namespace Fantasy.DataStructure
{
    /// <summary>
    /// ʵ������б��̳��� List&lt;T&gt;����ʵ�� IDisposable �ӿڣ����ڴ����͹���ʵ�����ļ��ϡ�
    /// </summary>
    /// <typeparam name="T">ʵ���������͡�</typeparam>
    public sealed class EntityList<T> : List<T>, IDisposable where T : IDisposable
    {
        private bool _isDispose;

        /// <summary>
        /// ����һ�� <see cref="EntityList{T}"/> ʵ������б��ʵ����
        /// </summary>
        /// <returns>������ʵ����</returns>
        public static EntityList<T> Create()
        {
            var list = Pool<EntityList<T>>.Rent();
            list._isDispose = false;
            return list;
        }

        /// <summary>
        /// ����б����ͷ�����ʵ��������Դ��
        /// </summary>
        public new void Clear()
        {
            // ����ͷ�ʵ��������Դ
            for (var i = 0; i < this.Count; i++)
            {
                this[i].Dispose();
            }
            // ���û���� Clear ����������б�
            base.Clear();
        }

        /// <summary>
        /// ����б������ͷ�ʵ��������Դ��
        /// </summary>
        public void ClearNotDispose()
        {
            base.Clear();
        }

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
            Pool<EntityList<T>>.Return(this);
        }
    }
}