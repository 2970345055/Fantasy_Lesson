#if FANTASY_NET
using NLog;

namespace Fantasy;

/// <summary>
/// ʹ�� NLog ʵ�ֵ���־��¼����
/// </summary>
public class NLog : ILog
{
    private readonly Logger _logger; // NLog ��־��¼��ʵ��

    /// <summary>
    /// ��ʼ�� NLog ʵ����
    /// </summary>
    /// <param name="name">��־��¼�������ơ�</param>
    public NLog(string name)
    {
        // ��ȡָ�����Ƶ� NLog ��־��¼��
        _logger = LogManager.GetLogger(name);
    }

    /// <summary>
    /// ��¼���ټ������־��Ϣ��
    /// </summary>
    /// <param name="message">��־��Ϣ��</param>
    public void Trace(string message)
    {
        _logger.Trace(message);
    }

    /// <summary>
    /// ��¼���漶�����־��Ϣ��
    /// </summary>
    /// <param name="message">��־��Ϣ��</param>
    public void Warning(string message)
    {
        _logger.Warn(message);
    }

    /// <summary>
    /// ��¼��Ϣ�������־��Ϣ��
    /// </summary>
    /// <param name="message">��־��Ϣ��</param>
    public void Info(string message)
    {
        _logger.Info(message);
    }

    /// <summary>
    /// ��¼���Լ������־��Ϣ��
    /// </summary>
    /// <param name="message">��־��Ϣ��</param>
    public void Debug(string message)
    {
        _logger.Debug(message);
    }

    /// <summary>
    /// ��¼���󼶱����־��Ϣ��
    /// </summary>
    /// <param name="message">��־��Ϣ��</param>
    public void Error(string message)
    {
        _logger.Error(message);
    }

    /// <summary>
    /// ��¼���ش��󼶱����־��Ϣ��
    /// </summary>
    /// <param name="message">��־��Ϣ��</param>
    public void Fatal(string message)
    {
        _logger.Fatal(message);
    }

    /// <summary>
    /// ��¼���ټ���ĸ�ʽ����־��Ϣ��
    /// </summary>
    /// <param name="message">��־��Ϣģ�塣</param>
    /// <param name="args">��ʽ��������</param>
    public void Trace(string message, params object[] args)
    {
        _logger.Trace(message, args);
    }

    /// <summary>
    /// ��¼���漶��ĸ�ʽ����־��Ϣ��
    /// </summary>
    /// <param name="message">��־��Ϣģ�塣</param>
    /// <param name="args">��ʽ��������</param>
    public void Warning(string message, params object[] args)
    {
        _logger.Warn(message, args);
    }

    /// <summary>
    /// ��¼��Ϣ����ĸ�ʽ����־��Ϣ��
    /// </summary>
    /// <param name="message">��־��Ϣģ�塣</param>
    /// <param name="args">��ʽ��������</param>
    public void Info(string message, params object[] args)
    {
        _logger.Info(message, args);
    }

    /// <summary>
    /// ��¼���Լ���ĸ�ʽ����־��Ϣ��
    /// </summary>
    /// <param name="message">��־��Ϣģ�塣</param>
    /// <param name="args">��ʽ��������</param>
    public void Debug(string message, params object[] args)
    {
        _logger.Debug(message, args);
    }

    /// <summary>
    /// ��¼���󼶱�ĸ�ʽ����־��Ϣ��
    /// </summary>
    /// <param name="message">��־��Ϣģ�塣</param>
    /// <param name="args">��ʽ��������</param>
    public void Error(string message, params object[] args)
    {
        _logger.Error(message, args);
    }

    /// <summary>
    /// ��¼���ش��󼶱�ĸ�ʽ����־��Ϣ��
    /// </summary>
    /// <param name="message">��־��Ϣģ�塣</param>
    /// <param name="args">��ʽ��������</param>
    public void Fatal(string message, params object[] args)
    {
        _logger.Fatal(message, args);
    }
}
#endif