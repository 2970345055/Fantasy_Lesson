using System;
using System.Collections.Generic;
using Fantasy.DataStructure;
using Fantasy.Helper;
#pragma warning disable CS8625

namespace Fantasy
{
    /// <summary>
    /// ��ʱ�����Ⱥ����࣬�ṩ��ʱ���ĺ��Ĺ��ܡ�
    /// </summary>
    public class TimerSchedulerCore
    {
        private long _minTime; // ��Сʱ��
        private readonly Func<long> _now; // ��ȡ��ǰʱ���ί��
        private readonly Queue<long> _timeOutTime = new(); // ��ʱʱ�����
        private readonly Queue<long> _timeOutTimerIds = new(); // ��ʱ��ʱ��ID����
        private readonly Dictionary<long, TimerAction> _timers = new(); // ��ʱ���ֵ䣬��ID�洢��ʱ������
        private readonly SortedOneToManyList<long, long> _timeId = new(); // ʱ�����ʱ��ID������һ�Զ��б�

        /// <summary>
        /// ���캯������ʼ����ʱ�����ġ�
        /// </summary>
        /// <param name="now">��ȡ��ǰʱ���ί�С�</param>
        public TimerSchedulerCore(Func<long> now)
        {
            _now = now;
        }

        /// <summary>
        /// ���¼�ʱ������鲢ִ�г�ʱ�ļ�ʱ������
        /// </summary>
        public void Update()
        {
            try
            {
                var currentTime = _now(); // ��ȡ��ǰʱ��

                if (_timeId.Count == 0)
                {
                    return;
                }

                if (currentTime < _minTime)
                {
                    return;
                }

                _timeOutTime.Clear();
                _timeOutTimerIds.Clear();

                // ����ʱ��ID�б����ҳ�ʱ�ļ�ʱ������
                foreach (var (key, _) in _timeId)
                {
                    if (key > currentTime)
                    {
                        _minTime = key;
                        break;
                    }

                    _timeOutTime.Enqueue(key);
                }

                // ����ʱ�ļ�ʱ������
                while (_timeOutTime.TryDequeue(out var time))
                {
                    foreach (var timerId in _timeId[time])
                    {
                        _timeOutTimerIds.Enqueue(timerId);
                    }

                    _timeId.RemoveKey(time);
                }

                // ִ�г�ʱ�ļ�ʱ������Ļص�����
                while (_timeOutTimerIds.TryDequeue(out var timerId))
                {
                    if (!_timers.TryGetValue(timerId, out var timer))
                    {
                        continue;
                    }

                    _timers.Remove(timer.Id);

                    // ���ݼ�ʱ������ִ�в�ͬ�Ĳ���
                    switch (timer.TimerType)
                    {
                        case TimerType.OnceWaitTimer:
                        {
                            var tcs = (FTask<bool>) timer.Callback;
                            timer.Dispose();
                            tcs.SetResult(true);
                            break;
                        }
                        case TimerType.OnceTimer:
                        {
                            var action = (Action) timer.Callback;
                            timer.Dispose();

                            if (action == null)
                            {
                                Log.Error($"timer {timer.ToJson()}");
                                break;
                            }

                            action();
                            break;
                        }
                        case TimerType.RepeatedTimer:
                        {
                            var action = (Action) timer.Callback;
                            AddTimer(_now() + timer.Time, timer);

                            if (action == null)
                            {
                                Log.Error($"timer {timer.ToJson()}");
                                break;
                            }

                            action();
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
        
        private void AddTimer(long tillTime, TimerAction timer)
        {
            _timers.Add(timer.Id, timer);
            _timeId.Add(tillTime, timer.Id);

            if (tillTime < _minTime)
            {
                _minTime = tillTime;
            }
        }

        /// <summary>
        /// �첽�ȴ�һ֡ʱ�䡣
        /// </summary>
        /// <returns>�ȴ��Ƿ�ɹ���</returns>
        public async FTask<bool> WaitFrameAsync()
        {
            return await WaitAsync(1);
        }

        /// <summary>
        /// �첽�ȴ�ָ��ʱ�䡣
        /// </summary>
        /// <param name="time">�ȴ���ʱ�䳤�ȡ�</param>
        /// <param name="cancellationToken">��ѡ��ȡ�����ơ�</param>
        /// <returns>�ȴ��Ƿ�ɹ���</returns>
        public async FTask<bool> WaitAsync(long time, FCancellationToken cancellationToken = null)
        {
            return await WaitTillAsync(_now() + time, cancellationToken);
        }

        /// <summary>
        /// �첽�ȴ�ֱ��ָ��ʱ�䡣
        /// </summary>
        /// <param name="tillTime">�ȴ���Ŀ��ʱ�䡣</param>
        /// <param name="cancellationToken">��ѡ��ȡ�����ơ�</param>
        /// <returns>�ȴ��Ƿ�ɹ���</returns>
        public async FTask<bool> WaitTillAsync(long tillTime, FCancellationToken cancellationToken = null)
        {
            if (_now() > tillTime)
            {
                return true;
            }

            var tcs = FTask<bool>.Create();
            var timerAction = TimerAction.Create();
            var timerId = timerAction.Id;
            timerAction.Callback = tcs;
            timerAction.TimerType = TimerType.OnceWaitTimer;

            // ����ȡ�������ķ���
            void CancelActionVoid()
            {
                if (!_timers.ContainsKey(timerId))
                {
                    return;
                }

                Remove(timerId);
                tcs.SetResult(false);
            }

            bool b;
            try
            {
                cancellationToken?.Add(CancelActionVoid);
                AddTimer(tillTime, timerAction);
                b = await tcs;
            }
            finally
            {
                cancellationToken?.Remove(CancelActionVoid);
            }

            return b;
        }

        /// <summary>
        /// ����һ������һִ֡�еļ�ʱ����
        /// </summary>
        /// <param name="action">��ʱ���ص�������</param>
        /// <returns>��ʱ���� ID��</returns>
        public long NewFrameTimer(Action action)
        {
            return RepeatedTimer(100, action);
        }

        /// <summary>
        /// ����һ���ظ�ִ�еļ�ʱ����
        /// </summary>
        /// <param name="time">��ʱ���ظ������ʱ�䡣</param>
        /// <param name="action">��ʱ���ص�������</param>
        /// <returns>��ʱ���� ID��</returns>
        public long RepeatedTimer(long time, Action action)
        {
            if (time <= 0)
            {
                throw new Exception("repeated time <= 0");
            }

            var tillTime = _now() + time;
            var timer = TimerAction.Create();
            timer.TimerType = TimerType.RepeatedTimer;
            timer.Time = time;
            timer.Callback = action;
            AddTimer(tillTime, timer);
            return timer.Id;
        }

        /// <summary>
        /// ����һ���ظ�ִ�еļ�ʱ�������ڷ���ָ�����͵��¼���
        /// </summary>
        /// <typeparam name="T">�¼����͡�</typeparam>
        /// <param name="time">��ʱ���ظ������ʱ�䡣</param>
        /// <param name="timerHandlerType">�¼����������͡�</param>
        /// <returns>��ʱ���� ID��</returns>
        public long RepeatedTimer<T>(long time, T timerHandlerType) where T : struct
        {
            void RepeatedTimerVoid()
            {
                EventSystem.Instance.Publish(timerHandlerType);
            }

            return RepeatedTimer(time, RepeatedTimerVoid);
        }

        /// <summary>
        /// ����һ��ִֻ��һ�εļ�ʱ����
        /// </summary>
        /// <param name="time">��ʱ��ִ�е��ӳ�ʱ�䡣</param>
        /// <param name="action">��ʱ���ص�������</param>
        /// <returns>��ʱ���� ID��</returns>
        public long OnceTimer(long time, Action action)
        {
            return OnceTillTimer(_now() + time, action);
        }

        /// <summary>
        /// ����һ��ִֻ��һ�εļ�ʱ�������ڷ���ָ�����͵��¼���
        /// </summary>
        /// <typeparam name="T">�¼����͡�</typeparam>
        /// <param name="time">��ʱ��ִ�е��ӳ�ʱ�䡣</param>
        /// <param name="timerHandlerType">�¼����������͡�</param>
        /// <returns>��ʱ���� ID��</returns>
        public long OnceTimer<T>(long time, T timerHandlerType) where T : struct
        {
            void OnceTimerVoid()
            {
                EventSystem.Instance.Publish(timerHandlerType);
            }

            return OnceTimer(time, OnceTimerVoid);
        }

        /// <summary>
        /// ����һ��ִֻ��һ�εļ�ʱ����ֱ��ָ��ʱ�䡣
        /// </summary>
        /// <param name="tillTime">��ʱ��ִ�е�Ŀ��ʱ�䡣</param>
        /// <param name="action">��ʱ���ص�������</param>
        /// <returns>��ʱ���� ID��</returns>
        public long OnceTillTimer(long tillTime, Action action)
        {
            if (tillTime < _now())
            {
                Log.Error($"new once time too small tillTime:{tillTime} Now:{_now()}");
            }

            var timer = TimerAction.Create();
            timer.TimerType = TimerType.OnceTimer;
            timer.Callback = action;
            AddTimer(tillTime, timer);
            return timer.Id;
        }

        /// <summary>
        /// ����һ��ִֻ��һ�εļ�ʱ����ֱ��ָ��ʱ�䣬���ڷ���ָ�����͵��¼���
        /// </summary>
        /// <typeparam name="T">�¼����͡�</typeparam>
        /// <param name="tillTime">��ʱ��ִ�е�Ŀ��ʱ�䡣</param>
        /// <param name="timerHandlerType">�¼����������͡�</param>
        /// <returns>��ʱ���� ID��</returns>
        public long OnceTillTimer<T>(long tillTime, T timerHandlerType) where T : struct
        {
            void OnceTillTimerVoid()
            {
                EventSystem.Instance.Publish(timerHandlerType);
            }

            return OnceTillTimer(tillTime, OnceTillTimerVoid);
        }

        /// <summary>
        /// ͨ�������Ƴ���ʱ����
        /// </summary>
        /// <param name="id">��ʱ���� ID��</param>
        public void RemoveByRef(ref long id)
        {
            Remove(id);
            id = 0;
        }

        /// <summary>
        /// �Ƴ�ָ�� ID �ļ�ʱ����
        /// </summary>
        /// <param name="id">��ʱ���� ID��</param>
        public void Remove(long id)
        {
            if (id == 0 || !_timers.Remove(id, out var timer))
            {
                return;
            }

            timer?.Dispose();
        }
    }
}