using System;

namespace Fantasy.Helper
{
    /// <summary>
    /// �ṩ�������ɲ�ͬ���� ID �Ĺ����ࡣ
    /// </summary>
    public static class IdFactory
    {
        // ʱ���������س���
        private static readonly long Epoch1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks / 10000;
        private static readonly long Epoch2023 = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks / 10000 - Epoch1970;
        private static readonly long EpochThisYear = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks / 10000 - Epoch1970;
        // ����ʱ ID ����ֶ�
        private static uint _lastRunTimeIdTime;
        private static uint _lastRunTimeIdSequence;
        // ʵ�� ID ����ֶ�
        private static uint _lastEntityIdTime;
        private static uint _lastEntityIdSequence;

        /// <summary>
        /// ������һ������ʱ ID��
        /// </summary>
        /// <returns>���ɵ�����ʱ ID��</returns>
        public static long NextRunTimeId()
        {
            var time = (uint) ((TimeHelper.Now - EpochThisYear) / 1000);

            if (time > _lastRunTimeIdTime)
            {
                _lastRunTimeIdTime = time;
                _lastRunTimeIdSequence = 0;
            }
            else if (++_lastRunTimeIdSequence > uint.MaxValue - 1)
            {
                ++_lastRunTimeIdTime;
                _lastRunTimeIdSequence = 0;
            }

            return new RuntimeIdStruct(_lastRunTimeIdTime, _lastRunTimeIdSequence);
        }

        /// <summary>
        /// ������һ��ʵ�� ID��
        /// </summary>
        /// <param name="locationId">λ�� ID��</param>
        /// <returns>���ɵ�ʵ�� ID��</returns>
        public static long NextEntityId(uint locationId)
        {
            var time = (uint)((TimeHelper.Now - Epoch2023) / 1000);

            if (time > _lastEntityIdTime)
            {
                _lastEntityIdTime = time;
                _lastEntityIdSequence = 0;
            }
            else if (++_lastEntityIdSequence > EntityIdStruct.MaskSequence - 1)
            {
                ++_lastEntityIdTime;
                _lastEntityIdSequence = 0;
            }

            return new EntityIdStruct(locationId, _lastEntityIdTime, _lastEntityIdSequence);
        }

        /// <summary>
        /// ��ȡʵ�� ID ��Ӧ��·�� ID��
        /// </summary>
        /// <param name="entityId">ʵ�� ID��</param>
        /// <returns>·�� ID��</returns>
        public static uint GetRouteId(long entityId)
        {
            return (ushort)(entityId >> 16 & EntityIdStruct.MaskRouteId);
        }

        /// <summary>
        /// ��ȡʵ�� ID ��Ӧ��Ӧ�� ID��
        /// </summary>
        /// <param name="entityId">ʵ�� ID��</param>
        /// <returns>Ӧ�� ID��</returns>
        public static ushort GetAppId(long entityId)
        {
            return (ushort)(entityId >> 26 & RouteIdStruct.MaskAppId);
        }

        /// <summary>
        /// ��ȡʵ�� ID ��Ӧ������ ID��
        /// </summary>
        /// <param name="entityId">ʵ�� ID��</param>
        /// <returns>���� ID��</returns>
        public static int GetWordId(long entityId)
        {
            return (ushort)(entityId >> 16 & RouteIdStruct.MaskWordId);
        }
    }
}

