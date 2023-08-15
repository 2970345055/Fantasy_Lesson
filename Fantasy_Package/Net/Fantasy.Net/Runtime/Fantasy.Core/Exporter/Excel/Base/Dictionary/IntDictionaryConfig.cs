using System.Collections.Generic;
using ProtoBuf;

namespace Fantasy.Core
{
    /// <summary>
    /// ʹ�� ProtoBuf ���л��������ֵ������ࡣ
    /// </summary>
    [ProtoContract]
    public class IntDictionaryConfig
    {
        /// <summary>
        /// ʹ�� ProtoBuf ���л����ֵ䡣
        /// </summary>
        [ProtoMember(1, IsRequired = true)] 
        public Dictionary<int, int> Dic;

        /// <summary>
        /// ��ȡ������ָ����������ֵ��
        /// </summary>
        /// <param name="key">����</param>
        /// <returns>����ֵ��</returns>
        public int this[int key] => GetValue(key);

        /// <summary>
        /// ���Ի�ȡָ����������ֵ��
        /// </summary>
        /// <param name="key">����</param>
        /// <param name="value">��ȡ��������ֵ��</param>
        /// <returns>����ɹ���ȡ��ֵ���򷵻� true�����򷵻� false��</returns>
        public bool TryGetValue(int key, out int value)
        {
            value = default;

            if (!Dic.ContainsKey(key))
            {
                return false;
            }
        
            value = Dic[key];
            return true;
        }

        private int GetValue(int key)
        {
            return Dic.TryGetValue(key, out var value) ? value : default;
        }
    }
}