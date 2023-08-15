namespace Fantasy.Core.Network
{
    /// <summary>
    /// ��ʾ�������л�Ϊ BSON ��ʽ����Ϣ�ӿڡ�
    /// </summary>
    public interface IBsonMessage : IMessage
    {
    
    }

    /// <summary>
    /// ��ʾ�������л�Ϊ BSON ��ʽ��������Ϣ�ӿڡ�
    /// </summary>
    public interface IBsonRequest : IBsonMessage, IRequest
    {
        
    }

    /// <summary>
    /// ��ʾ�������л�Ϊ BSON ��ʽ����Ӧ��Ϣ�ӿڡ�
    /// </summary>
    public interface IBsonResponse : IBsonMessage, IResponse
    {
        
    }
}