using ProtoBuf;

namespace Fantasy
{
    /// <summary>
    /// �ṩ ProtoBuf ���л��ͷ����л�֧�ֵĳ�����ࡣ
    /// </summary>
    [ProtoContract]
    public abstract class AProto
    {
        /// <summary>
        /// �ڷ����л���ɺ�ִ�еĲ�������������������д����ɳ�ʼ����
        /// </summary>
        public virtual void AfterDeserialization() => EndInit();
        /// <summary>
        /// �� <see cref="AfterDeserialization"/> �е��ã������������ĳ�ʼ��������
        /// </summary>
        protected virtual void EndInit() { }
    }
}