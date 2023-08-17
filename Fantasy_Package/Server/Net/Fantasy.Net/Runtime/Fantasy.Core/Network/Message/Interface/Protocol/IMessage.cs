namespace Fantasy.Core.Network
{
    /// <summary>
    /// ��ʾͨ����Ϣ�ӿڡ�
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// ��ȡ��Ϣ�Ĳ������롣
        /// </summary>
        /// <returns>�������롣</returns>
        uint OpCode();
    }

    /// <summary>
    /// ��ʾ������Ϣ�ӿڡ�
    /// </summary>
    public interface IRequest : IMessage
    {
        
    }

    /// <summary>
    /// ��ʾ��Ӧ��Ϣ�ӿڡ�
    /// </summary>
    public interface IResponse : IMessage
    {
        /// <summary>
        /// ��ȡ�����ô�����롣
        /// </summary>
        uint ErrorCode { get; set; }
    }
}