using System.Collections.Generic;
using ProtoBuf;

namespace Fantasy.Core
{
    /// <summary>
    /// ʹ�� ProtoBuf ���л����ַ����ֵ������ࡣ
    /// </summary>
    [ProtoContract]
    public sealed class StringDictionaryConfig
    {
        /// <summary>
        /// ʹ�� ProtoBuf ���л����ֵ䡣
        /// </summary>
        [ProtoMember(1, IsRequired = true)] 
        public Dictionary<int, string> Dic;

        /// <summary>
        /// ��ȡ������ָ�������ַ���ֵ��
        /// </summary>
        /// <param name="key">����</param>
        /// <returns>�ַ���ֵ��</returns>
        public string this[int key] => GetValue(key);

        /// <summary>
        /// ���Ի�ȡָ�������ַ���ֵ��
        /// </summary>
        /// <param name="key">����</param>
        /// <param name="value">��ȡ�����ַ���ֵ��</param>
        /// <returns>����ɹ���ȡ��ֵ���򷵻� true�����򷵻� false��</returns>
        public bool TryGetValue(int key, out string value)
        {
            value = default;

            if (!Dic.ContainsKey(key))
            {
                return false;
            }
        
            value = Dic[key];
            return true;
        }

        private string GetValue(int key)
        {
            return Dic.TryGetValue(key, out var value) ? value : default;
        }
    }
}