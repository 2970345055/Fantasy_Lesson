#if FANTASY_NET
using Fantasy.Helper;

namespace Fantasy.Core.Network;

/// <summary>
/// ��ʱ����������Ϣ��ʱ������
/// </summary>
public sealed class OnNetworkMessageUpdateCheckTimeout : TimerHandler<MessageHelper.NetworkMessageUpdate>
{
    /// <summary>
    /// ����������Ϣ��ʱ���߼���
    /// </summary>
    /// <param name="self">��ʱ���ص����������ᱻʹ�á�</param>
    public override void Handler(MessageHelper.NetworkMessageUpdate self)
    {
        var timeNow = TimeHelper.Now;

        // ��������ص��ֵ䣬����Ƿ��г�ʱ�����󣬽���ʱ������ӵ���ʱ��Ϣ�����б��С�
        foreach (var (rpcId, value) in MessageHelper.RequestCallback)
        {
            if (timeNow < value.CreateTime + MessageHelper.Timeout)
            {
                break;
            }

            MessageHelper.TimeoutRouteMessageSenders.Add(rpcId, value);
        }

        // ���û�г�ʱ������ֱ�ӷ��ء�
        if (MessageHelper.TimeoutRouteMessageSenders.Count == 0)
        {
            return;
        }

        // ����ʱ�����󣬸�����������������Ӧ����Ӧ��Ϣ�������д���
        foreach (var (rpcId, routeMessageSender) in MessageHelper.TimeoutRouteMessageSenders)
        {
            uint responseRpcId = 0;

            try
            {
                switch (routeMessageSender.Request)
                {
                    case IRouteMessage iRouteMessage:
                    {
                        // TODO: ����·����Ϣ������Ӧ�������д���
                        // var routeResponse = RouteMessageDispatcher.CreateResponse(iRouteMessage, ErrorCode.ErrRouteTimeout);
                        // responseRpcId = routeResponse.RpcId;
                        // routeResponse.RpcId = routeMessageSender.RpcId;
                        // MessageHelper.ResponseHandler(routeResponse);
                        break;
                    }
                    case IRequest iRequest:
                    {
                        // ������ͨ����������Ӧ�������д���
                        var response = MessageDispatcherSystem.Instance.CreateResponse(iRequest, CoreErrorCode.ErrRpcFail);
                        responseRpcId = routeMessageSender.RpcId;
                        MessageHelper.ResponseHandler(responseRpcId, response);
                        Log.Warning($"timeout rpcId:{rpcId} responseRpcId:{responseRpcId} {iRequest.ToJson()}");
                        break;
                    }
                    default:
                    {
                        // ����֧�ֵ��������͡�
                        Log.Error(routeMessageSender.Request != null
                            ? $"Unsupported protocol type {routeMessageSender.Request.GetType()} rpcId:{rpcId}"
                            : $"Unsupported protocol type:{routeMessageSender.MessageType.FullName} rpcId:{rpcId}");

                        MessageHelper.RequestCallback.Remove(rpcId);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error($"responseRpcId:{responseRpcId} routeMessageSender.RpcId:{routeMessageSender.RpcId} {e}");
            }
        }

        // ��ճ�ʱ��Ϣ�����б�
        MessageHelper.TimeoutRouteMessageSenders.Clear();
    }
}
#endif