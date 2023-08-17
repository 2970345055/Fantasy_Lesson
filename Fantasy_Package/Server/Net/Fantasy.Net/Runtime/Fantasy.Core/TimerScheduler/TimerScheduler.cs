using Fantasy.Helper;
#if FANTASY_UNITY
using UnityEngine;
#endif
namespace Fantasy
{
    /// <summary>
    /// ��ʱ���������࣬���ڹ����ʱ������ĵ��ȡ�
    /// </summary>
    public sealed class TimerScheduler : Singleton<TimerScheduler>, IUpdateSingleton
    {
        /// <summary>
        /// ʹ��ϵͳʱ�䴴���ļ�ʱ�����ġ�
        /// </summary>
        public readonly TimerSchedulerCore Core = new TimerSchedulerCore(() => TimeHelper.Now);
#if FANTASY_UNITY
        /// <summary>
        /// ʹ�� Unity ʱ�䴴���ļ�ʱ�����ġ�
        /// </summary>
        public readonly TimerSchedulerCore Unity = new TimerSchedulerCore(() => (long) (Time.time * 1000));
#endif
        /// <summary>
        /// ���¼�ʱ������
        /// </summary>
        public void Update()
        {
            Core.Update();
#if FANTASY_UNITY
            Unity.Update();
#endif
        }
    }
}