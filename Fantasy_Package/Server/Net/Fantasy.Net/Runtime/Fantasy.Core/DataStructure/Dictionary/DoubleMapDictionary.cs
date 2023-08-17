using System;
using System.Collections.Generic;
#pragma warning disable CS8601
#pragma warning disable CS8604
#pragma warning disable CS8603

namespace Fantasy.DataStructure
{
    /// <summary>
    /// �ṩһ��˫��ӳ���ֵ������࣬����˫���ֵ��ӳ�䡣
    /// </summary>
    /// <typeparam name="TKey">�ֵ��м������͡�</typeparam>
    /// <typeparam name="TValue">�ֵ���ֵ�����͡�</typeparam>
    public class DoubleMapDictionaryPool<TKey, TValue> : DoubleMapDictionary<TKey, TValue>, IDisposable
        where TKey : notnull where TValue : notnull
    {
        private bool _isDispose;

        /// <summary>
        /// ����һ���µ� <see cref="DoubleMapDictionaryPool{TKey, TValue}"/> ʵ����
        /// </summary>
        /// <returns>�´�����ʵ����</returns>
        public static DoubleMapDictionaryPool<TKey, TValue> Create()
        {
            var a = Pool<DoubleMapDictionaryPool<TKey, TValue>>.Rent();
            a._isDispose = false;
            return a;
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
            Pool<DoubleMapDictionaryPool<TKey, TValue>>.Return(this);
        }
    }

    /// <summary>
    /// ����ʵ��˫��ӳ����ֵ��࣬���ڽ�����ֵ����˫��ӳ�䡣
    /// </summary>
    /// <typeparam name="TK">�������ͣ�����Ϊ null��</typeparam>
    /// <typeparam name="TV">ֵ�����ͣ�����Ϊ null��</typeparam>
    public class DoubleMapDictionary<TK, TV> where TK : notnull where TV : notnull
    {
        private readonly Dictionary<TK, TV> _kv = new Dictionary<TK, TV>();
        private readonly Dictionary<TV, TK> _vk = new Dictionary<TV, TK>();

        /// <summary>
        /// ����һ���µĿյ� <see cref="DoubleMapDictionary{TK, TV}"/> ʵ����
        /// </summary>
        public DoubleMapDictionary()
        {
        }

        /// <summary>
        /// ����һ���µľ���ָ����ʼ������ <see cref="DoubleMapDictionary{TK, TV}"/> ʵ����
        /// </summary>
        /// <param name="capacity">��ʼ������</param>
        public DoubleMapDictionary(int capacity)
        {
            _kv = new Dictionary<TK, TV>(capacity);
            _vk = new Dictionary<TV, TK>(capacity);
        }

        /// <summary>
        /// ��ȡ�����ֵ������м����б�
        /// </summary>
        public List<TK> Keys => new List<TK>(_kv.Keys);

        /// <summary>
        /// ��ȡ�����ֵ�������ֵ���б�
        /// </summary>
        public List<TV> Values => new List<TV>(_vk.Keys);

        /// <summary>
        /// ���ֵ��е�ÿ����ֵ��ִ��ָ���Ĳ�����
        /// </summary>
        /// <param name="action">Ҫִ�еĲ�����</param>
        public void ForEach(Action<TK, TV> action)
        {
            if (action == null)
            {
                return;
            }

            var keys = _kv.Keys;
            foreach (var key in keys)
            {
                action(key, _kv[key]);
            }
        }

        /// <summary>
        /// ��ָ���ļ�ֵ����ӵ��ֵ��С�
        /// </summary>
        /// <param name="key">Ҫ��ӵļ���</param>
        /// <param name="value">Ҫ��ӵ�ֵ��</param>
        public void Add(TK key, TV value)
        {
            if (key == null || value == null || _kv.ContainsKey(key) || _vk.ContainsKey(value))
            {
                return;
            }

            _kv.Add(key, value);
            _vk.Add(value, key);
        }

        /// <summary>
        /// ����ָ���ļ���ȡ��Ӧ��ֵ��
        /// </summary>
        /// <param name="key">Ҫ����ֵ�ļ���</param>
        /// <returns>��ָ����������ֵ������Ҳ��������򷵻�Ĭ��ֵ��</returns>
        public TV GetValueByKey(TK key)
        {
            if (key != null && _kv.ContainsKey(key))
            {
                return _kv[key];
            }

            return default;
        }

        /// <summary>
        /// ���Ը���ָ���ļ���ȡ��Ӧ��ֵ��
        /// </summary>
        /// <param name="key">Ҫ����ֵ�ļ���</param>
        /// <param name="value">����ҵ�����Ϊ��ָ����������ֵ������Ϊֵ��Ĭ��ֵ��</param>
        /// <returns>����ҵ�������Ϊ true������Ϊ false��</returns>
        public bool TryGetValueByKey(TK key, out TV value)
        {
            var result = key != null && _kv.ContainsKey(key);

            value = result ? _kv[key] : default;

            return result;
        }

        /// <summary>
        /// ����ָ����ֵ��ȡ��Ӧ�ļ���
        /// </summary>
        /// <param name="value">Ҫ���Ҽ���ֵ��</param>
        /// <returns>��ָ��ֵ�����ļ�������Ҳ���ֵ���򷵻�Ĭ�ϼ���</returns>
        public TK GetKeyByValue(TV value)
        {
            if (value != null && _vk.ContainsKey(value))
            {
                return _vk[value];
            }

            return default;
        }

        /// <summary>
        /// ���Ը���ָ����ֵ��ȡ��Ӧ�ļ���
        /// </summary>
        /// <param name="value">Ҫ���Ҽ���ֵ��</param>
        /// <param name="key">����ҵ�����Ϊ��ָ��ֵ�����ļ�������Ϊ����Ĭ��ֵ��</param>
        /// <returns>����ҵ�ֵ����Ϊ true������Ϊ false��</returns>
        public bool TryGetKeyByValue(TV value, out TK key)
        {
            var result = value != null && _vk.ContainsKey(value);

            key = result ? _vk[value] : default;

            return result;
        }

        /// <summary>
        /// ����ָ���ļ��Ƴ���ֵ�ԡ�
        /// </summary>
        /// <param name="key">Ҫ�Ƴ��ļ���</param>
        public void RemoveByKey(TK key)
        {
            if (key == null)
            {
                return;
            }

            if (!_kv.TryGetValue(key, out var value))
            {
                return;
            }

            _kv.Remove(key);
            _vk.Remove(value);
        }

        /// <summary>
        /// ����ָ����ֵ�Ƴ���ֵ�ԡ�
        /// </summary>
        /// <param name="value">Ҫ�Ƴ���ֵ��</param>
        public void RemoveByValue(TV value)
        {
            if (value == null)
            {
                return;
            }

            if (!_vk.TryGetValue(value, out var key))
            {
                return;
            }

            _kv.Remove(key);
            _vk.Remove(value);
        }

        /// <summary>
        /// ����ֵ��е����м�ֵ�ԡ�
        /// </summary>
        public void Clear()
        {
            _kv.Clear();
            _vk.Clear();
        }

        /// <summary>
        /// �ж��ֵ��Ƿ����ָ���ļ���
        /// </summary>
        /// <param name="key">Ҫ���ļ���</param>
        /// <returns>����ֵ����ָ���ļ�����Ϊ true������Ϊ false��</returns>
        public bool ContainsKey(TK key)
        {
            return key != null && _kv.ContainsKey(key);
        }

        /// <summary>
        /// �ж��ֵ��Ƿ����ָ����ֵ��
        /// </summary>
        /// <param name="value">Ҫ����ֵ��</param>
        /// <returns>����ֵ����ָ����ֵ����Ϊ true������Ϊ false��</returns>
        public bool ContainsValue(TV value)
        {
            return value != null && _vk.ContainsKey(value);
        }

        /// <summary>
        /// �ж��ֵ��Ƿ����ָ���ļ�ֵ�ԡ�
        /// </summary>
        /// <param name="key">Ҫ���ļ���</param>
        /// <param name="value">Ҫ����ֵ��</param>
        /// <returns>����ֵ����ָ���ļ�ֵ�ԣ���Ϊ true������Ϊ false��</returns>
        public bool Contains(TK key, TV value)
        {
            if (key == null || value == null)
            {
                return false;
            }

            return _kv.ContainsKey(key) && _vk.ContainsKey(value);
        }
    }
}