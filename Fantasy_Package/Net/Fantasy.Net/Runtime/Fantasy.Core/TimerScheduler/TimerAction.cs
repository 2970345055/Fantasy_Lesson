using System;
using Fantasy.Helper;
#pragma warning disable CS8625
#pragma warning disable CS8618

namespace Fantasy
{
    /// <summary>
    /// ��ʱ�������࣬���ڹ���ʱ�������Ϣ��
    /// </summary>
    public sealed class TimerAction : IDisposable
    {
        /// <summary>
        /// Ψһ��ʶ����
        /// </summary>
        public long Id;
        /// <summary>
        /// ����ʱ�䡣
        /// </summary>
        public long Time;
        /// <summary>
        /// �ص�����
        /// </summary>
        public object Callback;
        /// <summary>
        /// ��ʱ�����͡�
        /// </summary>
        public TimerType TimerType;

        /// <summary>
        /// ����һ�� <see cref="TimerAction"/> ʵ����
        /// </summary>
        /// <returns>�´����� <see cref="TimerAction"/> ʵ����</returns>
        public static TimerAction Create()
        {
            var timerAction = Pool<TimerAction>.Rent();
            timerAction.Id = IdFactory.NextRunTimeId();
            return timerAction;
        }

        /// <summary>
        /// �ͷ���Դ��
        /// </summary>
        public void Dispose()
        {
            Id = 0;
            Time = 0;
            Callback = null;
            TimerType = TimerType.None;
            Pool<TimerAction>.Return(this);
        }
    }
}