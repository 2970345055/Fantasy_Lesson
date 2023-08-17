using System;
using System.Threading.Tasks;

namespace Fantasy.Helper
{
    /// <summary>
    /// ����һ�������ӿڣ���ʾ���Ա���ʼ����������ʱ���д���
    /// </summary>
    public interface ISingleton : IDisposable
    {
        /// <summary>
        /// ��ȡ������һ��ֵ��ָʾʵ���Ƿ��ѱ����١�
        /// </summary>
        public bool IsDisposed { get; set; }
        /// <summary>
        /// �첽��ʼ������ʵ���ķ�����
        /// </summary>
        /// <returns>��ʾ�첽����������</returns>
        public Task Initialize();
    }
}