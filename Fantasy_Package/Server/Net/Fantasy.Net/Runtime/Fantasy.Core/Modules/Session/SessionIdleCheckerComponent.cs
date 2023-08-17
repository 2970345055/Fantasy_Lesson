#if FANTASY_NET
using Fantasy.Core.Network;
using Fantasy.Helper;

namespace Fantasy.Core;

/// <summary>
/// ������Ự���г�ʱ�������
/// </summary>
public class SessionIdleCheckerComponent : Entity
{
    private long _timeOut;  // ���г�ʱʱ�䣨���룩
    private long _timerId;  // ����ʱ���� ID
    private long _selfRuntimeId;  // ����ȷ����������Ե���������ʱ ID
    private Session _session;  // �ԻỰ���������

    /// <summary>
    /// ��д Dispose �������ͷ���Դ��
    /// </summary>
    public override void Dispose()
    {
        Stop(); // ֹͣ����ʱ��
        _timeOut = 0; // ���ÿ��г�ʱʱ��
        _selfRuntimeId = 0; // ������������ʱ ID
        _session = null; // ����Ự����
        base.Dispose();
    }

    /// <summary>
    /// ʹ��ָ���ļ���Ϳ��г�ʱʱ���������м�鹦�ܡ�
    /// </summary>
    /// <param name="interval">�Ժ���Ϊ��λ�ļ������</param>
    /// <param name="timeOut">�Ժ���Ϊ��λ�Ŀ��г�ʱʱ�䡣</param>
    public void Start(int interval, int timeOut)
    {
        _timeOut = timeOut;
        _session = (Session)Parent;
        _selfRuntimeId = RuntimeId;
        // �����ظ���ʱ������ָ���ļ����ִ�� Check ����
        _timerId = TimerScheduler.Instance.Core.RepeatedTimer(interval, Check);
    }

    /// <summary>
    /// ֹͣ���м�鹦�ܡ�
    /// </summary>
    public void Stop()
    {
        if (_timerId == 0)
        {
            return;
        }

        TimerScheduler.Instance.Core.RemoveByRef(ref _timerId);
    }

    /// <summary>
    /// ִ�п��м�������
    /// </summary>
    private void Check()
    {
        if (_selfRuntimeId != RuntimeId)
        {
            Stop();
        }
        
        var timeNow = TimeHelper.Now;
        
        if (timeNow - _session.LastReceiveTime < _timeOut)
        {
            return;
        }
        
        Log.Warning($"session timeout id:{Id} timeNow:{timeNow} _session.LastReceiveTime:{_session.LastReceiveTime} _timeOut:{_timeOut}");
        _session.Dispose();
    }
}
#endif