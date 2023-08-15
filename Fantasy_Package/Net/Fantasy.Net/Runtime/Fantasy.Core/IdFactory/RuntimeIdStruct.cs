using System.Runtime.InteropServices;

namespace Fantasy.Helper
{
    /// <summary>
    /// ��ʾһ������ʱ ID �Ľṹ��
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RuntimeIdStruct
    {
        // +----------+------------+
        // | time(32) | sequence(32)
        // +----------+------------+

        private uint Time; // ʱ�䲿��
        private uint Sequence; // ���кŲ���

        /// <summary>
        /// ��ʼ��һ���µ�����ʱ ID �ṹ��
        /// </summary>
        /// <param name="time">ʱ�䲿�֡�</param>
        /// <param name="sequence">���кŲ��֡�</param>
        public RuntimeIdStruct(uint time, uint sequence)
        {
            Time = time;
            Sequence = sequence;
        }

        /// <summary>
        /// ������ʱ ID �ṹ��ʽת��Ϊ�����͡�
        /// </summary>
        /// <param name="runtimeId">Ҫת��������ʱ ID �ṹ��</param>
        public static implicit operator long(RuntimeIdStruct runtimeId)
        {
            ulong result = 0;
            result |= runtimeId.Sequence;
            result |= (ulong) runtimeId.Time << 32;
            return (long) result;
        }

        /// <summary>
        /// ����������ʽת��Ϊ����ʱ ID �ṹ��
        /// </summary>
        /// <param name="id">Ҫת���ĳ����� ID��</param>
        public static implicit operator RuntimeIdStruct(long id)
        {
            var result = (ulong) id;
            var idStruct = new RuntimeIdStruct()
            {
                Time = (uint) (result >> 32),
                Sequence = (uint) (result & 0xFFFFFFFF)
            };
            return idStruct;
        }
    }
}