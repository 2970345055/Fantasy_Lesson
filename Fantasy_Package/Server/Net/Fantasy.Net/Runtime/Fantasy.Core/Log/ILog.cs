namespace Fantasy
{
    /// <summary>
    /// ������־��¼���ܵĽӿڡ�
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// ��¼���ټ������־��Ϣ��
        /// </summary>
        /// <param name="message">��־��Ϣ��</param>
        void Trace(string message);
        /// <summary>
        /// ��¼���漶�����־��Ϣ��
        /// </summary>
        /// <param name="message">��־��Ϣ��</param>
        void Warning(string message);
        /// <summary>
        /// ��¼��Ϣ�������־��Ϣ��
        /// </summary>
        /// <param name="message">��־��Ϣ��</param>
        void Info(string message);
        /// <summary>
        /// ��¼���Լ������־��Ϣ��
        /// </summary>
        /// <param name="message">��־��Ϣ��</param>
        void Debug(string message);
        /// <summary>
        /// ��¼���󼶱����־��Ϣ��
        /// </summary>
        /// <param name="message">��־��Ϣ��</param>
        void Error(string message);
        /// <summary>
        /// ��¼���ټ���ĸ�ʽ����־��Ϣ��
        /// </summary>
        /// <param name="message">��־��Ϣģ�塣</param>
        /// <param name="args">��ʽ��������</param>
        void Trace(string message, params object[] args);
        /// <summary>
        /// ��¼���漶��ĸ�ʽ����־��Ϣ��
        /// </summary>
        /// <param name="message">��־��Ϣģ�塣</param>
        /// <param name="args">��ʽ��������</param>
        void Warning(string message, params object[] args);
        /// <summary>
        /// ��¼��Ϣ����ĸ�ʽ����־��Ϣ��
        /// </summary>
        /// <param name="message">��־��Ϣģ�塣</param>
        /// <param name="args">��ʽ��������</param>
        void Info(string message, params object[] args);
        /// <summary>
        /// ��¼���Լ���ĸ�ʽ����־��Ϣ��
        /// </summary>
        /// <param name="message">��־��Ϣģ�塣</param>
        /// <param name="args">��ʽ��������</param>
        void Debug(string message, params object[] args);
        /// <summary>
        /// ��¼���󼶱�ĸ�ʽ����־��Ϣ��
        /// </summary>
        /// <param name="message">��־��Ϣģ�塣</param>
        /// <param name="args">��ʽ��������</param>
        void Error(string message, params object[] args);
    }
}