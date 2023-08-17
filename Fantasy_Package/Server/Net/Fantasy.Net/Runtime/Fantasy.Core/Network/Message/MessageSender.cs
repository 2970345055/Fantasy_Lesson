using System;
using Fantasy.Helper;
#pragma warning disable CS8625
#pragma warning disable CS8618

namespace Fantasy.Core.Network
{
    /// <summary>
    /// ������Ϣ�����ߵ��ࡣ
    /// </summary>
    public sealed class MessageSender : IDisposable
    {
        /// <summary>
        /// ��ȡ������ RPC ID��
        /// </summary>
        public uint RpcId { get; private set; }
        /// <summary>
        /// ��ȡ������·�� ID��
        /// </summary>
        public long RouteId { get; private set; }
        /// <summary>
        /// ��ȡ�����ô���ʱ�䡣
        /// </summary>
        public long CreateTime { get; private set; }
        /// <summary>
        /// ��ȡ��������Ϣ���͡�
        /// </summary>
        public Type MessageType { get; private set; }
        /// <summary>
        /// ��ȡ������������Ϣ��
        /// </summary>
        public IMessage Request { get; private set; }
        /// <summary>
        /// ��ȡ����������
        /// </summary>
        public FTask<IResponse> Tcs { get; private set; }

        /// <summary>
        /// �ͷ���Դ��
        /// </summary>
        public void Dispose()
        {
            RpcId = 0;
            RouteId = 0;
            CreateTime = 0;
            Tcs = null;
            Request = null;
            MessageType = null;
            Pool<MessageSender>.Return(this);
        }

        /// <summary>
        /// ����һ�� <see cref="MessageSender"/> ʵ����
        /// </summary>
        /// <param name="rpcId">RPC ID��</param>
        /// <param name="requestType">������Ϣ���͡�</param>
        /// <param name="tcs">����</param>
        /// <returns>������ <see cref="MessageSender"/> ʵ����</returns>
        public static MessageSender Create(uint rpcId, Type requestType, FTask<IResponse> tcs)
        {
            var routeMessageSender = Pool<MessageSender>.Rent();
            routeMessageSender.Tcs = tcs;
            routeMessageSender.RpcId = rpcId;
            routeMessageSender.MessageType = requestType;
            routeMessageSender.CreateTime = TimeHelper.Now;
            return routeMessageSender;
        }

        /// <summary>
        /// ����һ�� <see cref="MessageSender"/> ʵ����
        /// </summary>
        /// <param name="rpcId">RPC ID��</param>
        /// <param name="request">������Ϣ��</param>
        /// <param name="tcs">����</param>
        /// <returns>������ <see cref="MessageSender"/> ʵ����</returns>
        public static MessageSender Create(uint rpcId, IRequest request, FTask<IResponse> tcs)
        {
            var routeMessageSender = Pool<MessageSender>.Rent();
            routeMessageSender.Tcs = tcs;
            routeMessageSender.RpcId = rpcId;
            routeMessageSender.Request = request;
            routeMessageSender.CreateTime = TimeHelper.Now;
            return routeMessageSender;
        }

        /// <summary>
        /// ����һ�� <see cref="MessageSender"/> ʵ����
        /// </summary>
        /// <param name="rpcId">RPC ID��</param>
        /// <param name="routeId">·�� ID��</param>
        /// <param name="request">·����Ϣ����</param>
        /// <param name="tcs">����</param>
        /// <returns>������ <see cref="MessageSender"/> ʵ����</returns>
        public static MessageSender Create(uint rpcId, long routeId, IRouteMessage request, FTask<IResponse> tcs)
        {
            var routeMessageSender = Pool<MessageSender>.Rent();
            routeMessageSender.Tcs = tcs;
            routeMessageSender.RpcId = rpcId;
            routeMessageSender.RouteId = routeId;
            routeMessageSender.Request = request;
            routeMessageSender.CreateTime = TimeHelper.Now;
            return routeMessageSender;
        }
    }
}