#if FANTASY_NET
namespace Fantasy.Core.Network;

/// <summary>
/// ����������ڲ��Ự��
/// </summary>
public sealed class ServerInnerSession : Session
{
    /// <summary>
    /// ������Ϣ���������ڲ���
    /// </summary>
    /// <param name="message">Ҫ���͵���Ϣ��</param>
    /// <param name="rpcId">RPC ��ʶ����</param>
    /// <param name="routeId">·�ɱ�ʶ����</param>
    public override void Send(object message, uint rpcId = 0, long routeId = 0)
    {
        if (IsDisposed)
        {
            return;
        }
        
        NetworkMessageScheduler.InnerScheduler(this, rpcId, routeId, ((IMessage)message).OpCode(), 0, message).Coroutine();
    }

    /// <summary>
    /// ����·����Ϣ���������ڲ���
    /// </summary>
    /// <param name="routeMessage">Ҫ���͵�·����Ϣ��</param>
    /// <param name="rpcId">RPC ��ʶ����</param>
    /// <param name="routeId">·�ɱ�ʶ����</param>
    public override void Send(IRouteMessage routeMessage, uint rpcId = 0, long routeId = 0)
    {
        if (IsDisposed)
        {
            return;
        }

        NetworkMessageScheduler.InnerScheduler(this, rpcId, routeId, routeMessage.OpCode(), routeMessage.RouteTypeOpCode(), routeMessage).Coroutine();
    }

    /// <summary>
    /// �����ڴ������������ڲ�����֧�֣���
    /// </summary>
    /// <param name="memoryStream">Ҫ���͵��ڴ�����</param>
    /// <param name="rpcId">RPC ��ʶ����</param>
    /// <param name="routeTypeOpCode">·�����ͺͲ����롣</param>
    /// <param name="routeId">·�ɱ�ʶ����</param>
    public override void Send(MemoryStream memoryStream, uint rpcId = 0, long routeTypeOpCode = 0, long routeId = 0)
    {
        throw new Exception("The use of this method is not supported");
    }

    /// <summary>
    /// �������󲢵ȴ���Ӧ����֧�֣���
    /// </summary>
    /// <param name="request">Ҫ���õ�����</param>
    /// <param name="routeId">·�ɱ�ʶ����</param>
    /// <returns>һ�������첽���������񣬷�����Ӧ��</returns>
    public override FTask<IResponse> Call(IRequest request, long routeId = 0)
    {
        throw new Exception("The use of this method is not supported");
    }
}
#endif