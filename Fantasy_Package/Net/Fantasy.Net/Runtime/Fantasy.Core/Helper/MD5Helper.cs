using System.IO;
using System.Security.Cryptography;

namespace Fantasy.Helper
{
    /// <summary>
    /// �ṩ���� MD5 ɢ��ֵ�ĸ���������
    /// </summary>
    public static class MD5Helper
    {
        /// <summary>
        /// ����ָ���ļ��� MD5 ɢ��ֵ��
        /// </summary>
        /// <param name="filePath">Ҫ����ɢ��ֵ���ļ�·����</param>
        /// <returns>��ʾ�ļ��� MD5 ɢ��ֵ���ַ�����</returns>
        public static string FileMD5(string filePath)
        {
            using var file = new FileStream(filePath, FileMode.Open);
            return FileMD5(file);
        }

        /// <summary>
        /// ��������ļ����� MD5 ɢ��ֵ��
        /// </summary>
        /// <param name="fileStream">Ҫ����ɢ��ֵ���ļ�����</param>
        /// <returns>��ʾ�ļ����� MD5 ɢ��ֵ���ַ�����</returns>
        public static string FileMD5(FileStream fileStream)
        {
            var md5 = MD5.Create();
            return md5.ComputeHash(fileStream).ToHex("x2");
        }

        /// <summary>
        /// ��������ֽ������ MD5 ɢ��ֵ��
        /// </summary>
        /// <param name="bytes">Ҫ����ɢ��ֵ���ֽ����顣</param>
        /// <returns>��ʾ�ֽ������ MD5 ɢ��ֵ���ַ�����</returns>
        public static string BytesMD5(byte[] bytes)
        {
            var md5 = MD5.Create();
            bytes = md5.ComputeHash(bytes);
            return bytes.ToHex("x2");
        }
    }
}