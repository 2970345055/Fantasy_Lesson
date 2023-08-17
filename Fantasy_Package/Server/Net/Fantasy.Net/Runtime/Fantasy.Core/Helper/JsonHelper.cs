using System;
using Newtonsoft.Json;
#pragma warning disable CS8603

namespace Fantasy.Helper
{
    /// <summary>
    /// �ṩ���� JSON ���ݵĸ���������
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// ���������л�Ϊ JSON �ַ�����
        /// </summary>
        /// <typeparam name="T">Ҫ���л��Ķ������͡�</typeparam>
        /// <param name="t">Ҫ���л��Ķ���</param>
        /// <returns>��ʾ���л������ JSON �ַ�����</returns>
        public static string ToJson<T>(this T t)
        {
            return JsonConvert.SerializeObject(t);
        }

        /// <summary>
        /// �����л� JSON �ַ���Ϊָ�����͵Ķ���
        /// </summary>
        /// <param name="json">Ҫ�����л��� JSON �ַ�����</param>
        /// <param name="type">Ŀ���������͡�</param>
        /// <param name="reflection">�Ƿ�ʹ�÷�����з����л���Ĭ��Ϊ true����</param>
        /// <returns>�����л���Ķ���</returns>
        public static object Deserialize(this string json, Type type, bool reflection = true)
        {
            return JsonConvert.DeserializeObject(json, type);
        }

        /// <summary>
        /// �����л� JSON �ַ���Ϊָ�����͵Ķ���
        /// </summary>
        /// <typeparam name="T">Ŀ���������͡�</typeparam>
        /// <param name="json">Ҫ�����л��� JSON �ַ�����</param>
        /// <returns>�����л���Ķ���</returns>
        public static T Deserialize<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// ��¡����ͨ�����������л�Ϊ JSON��Ȼ���ٽ��з����л���
        /// </summary>
        /// <typeparam name="T">Ҫ��¡�Ķ������͡�</typeparam>
        /// <param name="t">Ҫ��¡�Ķ���</param>
        /// <returns>��¡��Ķ���</returns>
        public static T Clone<T>(T t)
        {
            return t.ToJson().Deserialize<T>();
        }
    }
}