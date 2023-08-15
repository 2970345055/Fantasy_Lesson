using System;
using System.IO;
#pragma warning disable CS8625
#pragma warning disable CS8618

namespace Fantasy.Core.Network
{
    /// <summary>
    /// ��ͬ���͵����������
    /// </summary>
    public enum NetActionType
    {
        /// <summary>
        /// �޲�����
        /// </summary>
        None = 0,
        /// <summary>
        /// �������ݡ�
        /// </summary>
        Send = 1,
        /// <summary>
        /// �����ڴ������ݡ�
        /// </summary>
        SendMemoryStream = 2,
        /// <summary>
        /// �Ƴ�ͨ����
        /// </summary>
        RemoveChannel = 3,
    }

    /// <summary>
    /// ��ʾһ����������������Ƿ�����Ϣ���Ƴ�ͨ���Ȳ�����
    /// </summary>
    public struct NetAction : IDisposable
    {
        /// <summary>
        /// ���ڷ��͵Ķ��󣬿�������Ϣ������������ݡ�
        /// </summary>
        public object Obj;
        /// <summary>
        /// Ҫ���͵� RPC ID��
        /// </summary>
        public uint RpcId;
        /// <summary>
        /// ������ʵ�� ID��
        /// </summary>
        public long EntityId;
        /// <summary>
        /// ���������� ID��
        /// </summary>
        public long NetworkId;
        /// <summary>
        /// ������ͨ�� ID��
        /// </summary>
        public uint ChannelId;
        /// <summary>
        /// ������·������ Op Code��
        /// </summary>
        public long RouteTypeOpCode;
        /// <summary>
        /// ���ڷ��͵��ڴ�����
        /// </summary>
        public MemoryStream MemoryStream;
        /// <summary>
        /// ������������͡�
        /// </summary>
        public NetActionType NetActionType;

        /// <summary>
        /// ��ʼ��һ���µ� NetAction �ṹ��ʵ�������ڷ����ڴ�����
        /// </summary>
        /// <param name="networkId">���������� ID��</param>
        /// <param name="channelId">������ͨ�� ID��</param>
        /// <param name="rpcId">Ҫ���͵� RPC ID��</param>
        /// <param name="routeTypeOpCode">������·������ Op Code��</param>
        /// <param name="entityId">������ʵ�� ID��</param>
        /// <param name="netActionType">������������͡�</param>
        /// <param name="memoryStream">Ҫ���͵��ڴ�����</param>
        public NetAction(long networkId, uint channelId, uint rpcId, long routeTypeOpCode, long entityId, NetActionType netActionType, MemoryStream memoryStream)
        {
            Obj = null;
            RpcId = rpcId;
            EntityId = entityId;
            NetworkId = networkId;
            ChannelId = channelId;
            RouteTypeOpCode = routeTypeOpCode;
            MemoryStream = memoryStream;
            NetActionType = netActionType;
        }

        /// <summary>
        /// ��ʼ��һ���µ� NetAction �ṹ��ʵ�������ڷ��Ͷ���
        /// </summary>
        /// <param name="networkId">���������� ID��</param>
        /// <param name="channelId">������ͨ�� ID��</param>
        /// <param name="rpcId">Ҫ���͵� RPC ID��</param>
        /// <param name="routeTypeOpCode">������·������ Op Code��</param>
        /// <param name="entityId">������ʵ�� ID��</param>
        /// <param name="netActionType">������������͡�</param>
        /// <param name="obj">Ҫ���͵Ķ���</param>
        public NetAction(long networkId, uint channelId, uint rpcId, long routeTypeOpCode, long entityId, NetActionType netActionType, object obj)
        {
            Obj = obj;
            RpcId = rpcId;
            EntityId = entityId;
            NetworkId = networkId;
            ChannelId = channelId;
            MemoryStream = null;
            RouteTypeOpCode = routeTypeOpCode;
            NetActionType = netActionType;
        }

        /// <summary>
        /// �ͷ���Դ������ǰʵ����״̬��
        /// </summary>
        public void Dispose()
        {
            Obj = null;
            MemoryStream = null;
            RpcId = 0;
            EntityId = 0;
            NetworkId = 0;
            ChannelId = 0;
            RouteTypeOpCode = 0;
            NetActionType = NetActionType.None;
        }
    }
}