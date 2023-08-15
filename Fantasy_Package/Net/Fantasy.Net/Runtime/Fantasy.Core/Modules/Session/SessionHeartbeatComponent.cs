#if FANTASY_UNITY
using Fantasy.Core.Network;
using Fantasy.Helper;

namespace Fantasy.Model
{
    /// <summary>
    /// �������Ự�����������
    /// </summary>
    public class SessionHeartbeatComponent : Entity
    {
        private long _timerId;  // ���������ʱ���� ID
        private long _selfRunTimeId;  // ����ȷ����������Ե���������ʱ ID
        private Session _session;  // �ԻỰ���������
        private readonly PingRequest _pingRequest = new PingRequest(); // ������ Ping �������
        
        /// <summary>
        /// ��ȡ��ǰ�� Ping ֵ��
        /// </summary>
        public int Ping { get; private set; }

        /// <summary>
        /// ��д Dispose �������ͷ���Դ��
        /// </summary>
        public override void Dispose()
        {
            Stop();
            Ping = 0;
            _session = null;
            _selfRunTimeId = 0;
            base.Dispose();
        }

        /// <summary>
        /// ʹ��ָ���ļ�������������ܡ�
        /// </summary>
        /// <param name="interval">�Ժ���Ϊ��λ�����������ͼ����</param>
        public void Start(int interval)
        {
            _session = (Session)Parent;
            _selfRunTimeId = RuntimeId;
            _timerId = TimerScheduler.Instance.Unity.RepeatedTimer(interval, () => RepeatedSend().Coroutine());
        }

        /// <summary>
        /// ֹͣ�������ܡ�
        /// </summary>
        public void Stop()
        {
            if (_timerId == 0)
            {
                return; // �����ʱ�� ID Ϊ 0�����ʱ��δ���ֱ�ӷ���
            }
            
            TimerScheduler.Instance?.Unity.RemoveByRef(ref _timerId);
        }

        /// <summary>
        /// �첽�����������󲢴�����Ӧ��
        /// </summary>
        /// <returns>��ʾ�����в������첽����</returns>
        private async FTask RepeatedSend()
        {
            if (_selfRunTimeId != RuntimeId)
            {
                Stop();
            }
            
            var requestTime = TimeHelper.Now;
            var pingResponse = (PingResponse)await _session.Call(_pingRequest);

            if (pingResponse.ErrorCode != 0)
            {
                return;
            }
            
            var responseTime = TimeHelper.Now; // ��¼����������Ӧ��ʱ��
            Ping = (int)(responseTime - requestTime) / 2;
            TimeHelper.TimeDiff = pingResponse.Now + Ping - responseTime;
        }
    }
}
#endif