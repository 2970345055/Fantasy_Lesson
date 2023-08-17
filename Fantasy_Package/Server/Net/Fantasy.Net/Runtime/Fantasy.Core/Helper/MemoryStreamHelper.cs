using System.IO;
using Fantasy.IO;

namespace Fantasy.Helper
{
    /// <summary>
    /// �ṩ��ȡ�ɻ����ڴ����İ���������
    /// </summary>
    public static class MemoryStreamHelper
    {
        private static readonly RecyclableMemoryStreamManager Manager = new RecyclableMemoryStreamManager();

        /// <summary>
        /// ��ȡһ���ɻ����ڴ���ʵ����
        /// </summary>
        /// <returns>�ɻ����ڴ���ʵ����</returns>
        public static MemoryStream GetRecyclableMemoryStream()
        {
            return Manager.GetStream();
        }
    }
}