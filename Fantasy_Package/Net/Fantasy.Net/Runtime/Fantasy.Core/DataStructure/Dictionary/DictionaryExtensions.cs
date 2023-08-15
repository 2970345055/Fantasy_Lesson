using System.Collections.Generic;
#pragma warning disable CS8601

namespace Fantasy.DataStructure
{
    /// <summary>
    /// �ṩ���ֵ����չ������
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// ���Դ��ֵ����Ƴ�ָ��������������Ӧ��ֵ��
        /// </summary>
        /// <typeparam name="T">�ֵ��м������͡�</typeparam>
        /// <typeparam name="TV">�ֵ���ֵ�����͡�</typeparam>
        /// <param name="self">Ҫ�������ֵ�ʵ����</param>
        /// <param name="key">Ҫ�Ƴ��ļ���</param>
        /// <param name="value">���ֵ����Ƴ���ֵ������ɹ��Ƴ�����</param>
        /// <returns>����ɹ��Ƴ���ֵ�ԣ���Ϊ true������Ϊ false��</returns>
        public static bool TryRemove<T, TV>(this IDictionary<T, TV> self, T key, out TV value)
        {
            if (!self.TryGetValue(key, out value))
            {
                return false;
            }

            self.Remove(key);
            return true;
        }
    }
}