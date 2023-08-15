using System;
using System.Diagnostics;

namespace Fantasy
{
    /// <summary>
    /// �ṩ��־��¼���ܵľ�̬�ࡣ
    /// </summary>
    public static class Log
    {
        private static readonly ILog LogCore;

        static Log()
        {
#if FANTASY_NET
            LogCore = new NLog("Server");
#elif FANTASY_UNITY
            LogCore = new UnityLog();
#endif
        }

        /// <summary>
        /// ��¼���ټ������־��Ϣ��
        /// </summary>
        /// <param name="msg">��־��Ϣ��</param>
        public static void Trace(string msg)
        {
            var st = new StackTrace(1, true);
            LogCore.Trace($"{msg}\n{st}");
        }

        /// <summary>
        /// ��¼���Լ������־��Ϣ��
        /// </summary>
        /// <param name="msg">��־��Ϣ��</param>
        public static void Debug(string msg)
        {
            LogCore.Debug(msg);
        }

        /// <summary>
        /// ��¼��Ϣ�������־��Ϣ��
        /// </summary>
        /// <param name="msg">��־��Ϣ��</param>
        public static void Info(string msg)
        {
            LogCore.Info(msg);
        }

        /// <summary>
        /// ��¼���ټ������־��Ϣ������������ջ��Ϣ��
        /// </summary>
        /// <param name="msg">��־��Ϣ��</param>
        public static void TraceInfo(string msg)
        {
            var st = new StackTrace(1, true);
            LogCore.Trace($"{msg}\n{st}");
        }

        /// <summary>
        /// ��¼���漶�����־��Ϣ��
        /// </summary>
        /// <param name="msg">��־��Ϣ��</param>
        public static void Warning(string msg)
        {
            LogCore.Warning(msg);
        }

        /// <summary>
        /// ��¼���󼶱����־��Ϣ������������ջ��Ϣ��
        /// </summary>
        /// <param name="msg">��־��Ϣ��</param>
        public static void Error(string msg)
        {
            var st = new StackTrace(1, true);
            LogCore.Error($"{msg}\n{st}");
        }

        /// <summary>
        /// ��¼�쳣�Ĵ��󼶱����־��Ϣ������������ջ��Ϣ��
        /// </summary>
        /// <param name="e">�쳣����</param>
        public static void Error(Exception e)
        {
            if (e.Data.Contains("StackTrace"))
            {
                LogCore.Error($"{e.Data["StackTrace"]}\n{e}");
                return;
            }
            var str = e.ToString();
            LogCore.Error(str);
        }

        /// <summary>
        /// ��¼���ټ���ĸ�ʽ����־��Ϣ������������ջ��Ϣ��
        /// </summary>
        /// <param name="message">��־��Ϣģ�塣</param>
        /// <param name="args">��ʽ��������</param>
        public static void Trace(string message, params object[] args)
        {
            var st = new StackTrace(1, true);
            LogCore.Trace($"{string.Format(message, args)}\n{st}");
        }

        /// <summary>
        /// ��¼���漶��ĸ�ʽ����־��Ϣ��
        /// </summary>
        /// <param name="message">��־��Ϣģ�塣</param>
        /// <param name="args">��ʽ��������</param>
        public static void Warning(string message, params object[] args)
        {
            LogCore.Warning(string.Format(message, args));
        }

        /// <summary>
        /// ��¼��Ϣ����ĸ�ʽ����־��Ϣ��
        /// </summary>
        /// <param name="message">��־��Ϣģ�塣</param>
        /// <param name="args">��ʽ��������</param>
        public static void Info(string message, params object[] args)
        {
            LogCore.Info(string.Format(message, args));
        }

        /// <summary>
        /// ��¼���Լ���ĸ�ʽ����־��Ϣ��
        /// </summary>
        /// <param name="message">��־��Ϣģ�塣</param>
        /// <param name="args">��ʽ��������</param>
        public static void Debug(string message, params object[] args)
        {
            LogCore.Debug(string.Format(message, args));
        }

        /// <summary>
        /// ��¼���󼶱�ĸ�ʽ����־��Ϣ������������ջ��Ϣ��
        /// </summary>
        /// <param name="message">��־��Ϣģ�塣</param>
        /// <param name="args">��ʽ��������</param>
        public static void Error(string message, params object[] args)
        {
            var st = new StackTrace(1, true);
            var s = string.Format(message, args) + '\n' + st;
            LogCore.Error(s);
        }
    }
}