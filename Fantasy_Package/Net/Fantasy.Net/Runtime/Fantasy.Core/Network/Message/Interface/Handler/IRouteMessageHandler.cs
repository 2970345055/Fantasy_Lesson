#if FANTASY_NET
// ReSharper disable InconsistentNaming

namespace Fantasy.Core.Network
{
    /// <summary>
    /// ��ʾ·����Ϣ�������Ľӿڣ������ض����͵�·����Ϣ��
    /// </summary>
    public interface IRouteMessageHandler
    {
        /// <summary>
        /// ��ȡ�������Ϣ���͡�
        /// </summary>
        /// <returns>��Ϣ���͡�</returns>
        public Type Type();
        /// <summary>
        /// ����·����Ϣ�ķ�����
        /// </summary>
        /// <param name="session">�Ự����</param>
        /// <param name="entity">ʵ�����</param>
        /// <param name="rpcId">RPC��ʶ��</param>
        /// <param name="routeMessage">Ҫ�����·����Ϣ��</param>
        /// <returns>�첽����</returns>
        FTask Handle(Session session, Entity entity, uint rpcId, object routeMessage);
    }

    /// <summary>
    /// ����·�ɻ��࣬ʵ���� <see cref="IRouteMessageHandler"/> �ӿڣ����ڴ����ض�ʵ���·����Ϣ���͵�·�ɡ�
    /// </summary>
    /// <typeparam name="TEntity">ʵ�����͡�</typeparam>
    /// <typeparam name="TMessage">·����Ϣ���͡�</typeparam>
    public abstract class Route<TEntity, TMessage> : IRouteMessageHandler where TEntity : Entity where TMessage : IRouteMessage
    {
        /// <summary>
        /// ��ȡ�������Ϣ���͡�
        /// </summary>
        /// <returns>��Ϣ���͡�</returns>
        public Type Type()
        {
            return typeof(TMessage);
        }

        /// <summary>
        /// ����·����Ϣ�ķ�����
        /// </summary>
        /// <param name="session">�Ự����</param>
        /// <param name="entity">ʵ�����</param>
        /// <param name="rpcId">RPC��ʶ��</param>
        /// <param name="routeMessage">Ҫ�����·����Ϣ��</param>
        /// <returns>�첽����</returns>
        public async FTask Handle(Session session, Entity entity, uint rpcId, object routeMessage)
        {
            if (routeMessage is not TMessage ruteMessage)
            {
                Log.Error($"Message type conversion error: {routeMessage.GetType().FullName} to {typeof(TMessage).Name}");
                return;
            }

            if (entity is not TEntity tEntity)
            {
                Log.Error($"Route type conversion error: {entity.GetType().Name} to {nameof(TEntity)}");
                return;
            }

            try
            {
                await Run(tEntity, ruteMessage);
            }
            catch (Exception e)
            {
                if (entity is not Scene scene)
                {
                    scene = entity.Scene;
                }
                
                Log.Error($"SceneWorld:{session.Scene.World.Id} ServerConfigId:{scene.Server?.Id} SceneType:{scene.SceneType} EntityId {tEntity.Id} : Error {e}");
            }
        }

        /// <summary>
        /// ����·����Ϣ�����߼���
        /// </summary>
        /// <param name="entity">ʵ�����</param>
        /// <param name="message">Ҫ�����·����Ϣ��</param>
        /// <returns>�첽����</returns>
        protected abstract FTask Run(TEntity entity, TMessage message);
    }

    /// <summary>
    /// ����·��RPC���࣬ʵ���� <see cref="IRouteMessageHandler"/> �ӿڣ����ڴ����������Ӧ���͵�·�ɡ�
    /// </summary>
    /// <typeparam name="TEntity">ʵ�����͡�</typeparam>
    /// <typeparam name="TRouteRequest">·���������͡�</typeparam>
    /// <typeparam name="TRouteResponse">·����Ӧ���͡�</typeparam>
    public abstract class RouteRPC<TEntity, TRouteRequest, TRouteResponse> : IRouteMessageHandler where TEntity : Entity where TRouteRequest : IRouteRequest where TRouteResponse : IRouteResponse
    {
        /// <summary>
        /// ��ȡ�������Ϣ���͡�
        /// </summary>
        /// <returns>��Ϣ���͡�</returns>
        public Type Type()
        {
            return typeof(TRouteRequest);
        }

        /// <summary>
        /// ����·����Ϣ�ķ�����
        /// </summary>
        /// <param name="session">�Ự����</param>
        /// <param name="entity">ʵ�����</param>
        /// <param name="rpcId">RPC��ʶ��</param>
        /// <param name="routeMessage">Ҫ�����·����Ϣ��</param>
        /// <returns>�첽����</returns>
        public async FTask Handle(Session session, Entity entity, uint rpcId, object routeMessage)
        {
            if (routeMessage is not TRouteRequest tRouteRequest)
            {
                Log.Error($"Message type conversion error: {routeMessage.GetType().FullName} to {typeof(TRouteRequest).Name}");
                return;
            }

            if (entity is not TEntity tEntity)
            {
                Log.Error($"Route type conversion error: {entity.GetType().Name} to {nameof(TEntity)}");
                return;
            }
            
            var isReply = false;
            var response = Activator.CreateInstance<TRouteResponse>();
            
            void Reply()
            {
                if (isReply)
                {
                    return;
                }

                isReply = true;

                if (session.IsDisposed)
                {
                    return;
                }
                
                session.Send(response, rpcId);
            }
            
            try
            {
                await Run(tEntity, tRouteRequest, response, Reply);
            }
            catch (Exception e)
            {
                if (entity is not Scene scene)
                {
                    scene = entity.Scene;
                }
                
                Log.Error($"SceneWorld:{session.Scene.World?.Id} ServerConfigId:{scene.Server?.Id} SceneType:{scene.SceneType} EntityId {tEntity.Id} : Error {e}");
                response.ErrorCode = CoreErrorCode.ErrRpcFail;
            }
            finally
            {
                Reply();
            }
        }

        /// <summary>
        /// ����·����Ϣ�����߼���
        /// </summary>
        /// <param name="entity">ʵ�����</param>
        /// <param name="request">����·����Ϣ��</param>
        /// <param name="response">��Ӧ·����Ϣ��</param>
        /// <param name="reply">������Ӧ�ķ�����</param>
        /// <returns>�첽����</returns>
        protected abstract FTask Run(TEntity entity, TRouteRequest request, TRouteResponse response, Action reply);
    }

    /// <summary>
    /// ���Ϳ�Ѱַ·�ɻ��࣬ʵ���� <see cref="IRouteMessageHandler"/> �ӿڣ����ڴ����ض�ʵ��Ϳ�Ѱַ·����Ϣ���͵�·�ɡ�
    /// </summary>
    /// <typeparam name="TEntity">ʵ�����͡�</typeparam>
    /// <typeparam name="TMessage">��Ѱַ·����Ϣ���͡�</typeparam>
    public abstract class Addressable<TEntity, TMessage> : IRouteMessageHandler where TEntity : Entity where TMessage : IAddressableRouteMessage
    {
        /// <summary>
        /// ��ȡ��Ϣ���͡�
        /// </summary>
        /// <returns>��Ϣ���͡�</returns>
        public Type Type()
        {
            return typeof(TMessage);
        }

        /// <summary>
        /// �����Ѱַ·����Ϣ��
        /// </summary>
        /// <param name="session">�Ự��</param>
        /// <param name="entity">ʵ�塣</param>
        /// <param name="rpcId">RPC��ʶ��</param>
        /// <param name="routeMessage">��Ѱַ·����Ϣ��</param>
        public async FTask Handle(Session session, Entity entity, uint rpcId, object routeMessage)
        {
            if (routeMessage is not TMessage ruteMessage)
            {
                Log.Error($"Message type conversion error: {routeMessage.GetType().FullName} to {typeof(TMessage).Name}");
                return;
            }

            if (entity is not TEntity tEntity)
            {
                Log.Error($"Route type conversion error: {entity.GetType().Name} to {nameof(TEntity)}");
                return;
            }

            try
            {
                await Run(tEntity, ruteMessage);
            }
            catch (Exception e)
            {
                if (entity is not Scene scene)
                {
                    scene = entity.Scene;
                }
                
                Log.Error($"SceneWorld:{session.Scene.World.Id} ServerConfigId:{scene.Server?.Id} SceneType:{scene.SceneType} EntityId {tEntity.Id} : Error {e}");
            }
            finally
            {
                session.Send(MessageDispatcherSystem.Instance.CreateRouteResponse(), rpcId);
            }
        }

        /// <summary>
        /// ���д����Ѱַ·����Ϣ��
        /// </summary>
        /// <param name="entity">ʵ�塣</param>
        /// <param name="message">��Ѱַ·����Ϣ��</param>
        protected abstract FTask Run(TEntity entity, TMessage message);
    }

    /// <summary>
    /// ���Ϳ�ѰַRPC·�ɻ��࣬ʵ���� <see cref="IRouteMessageHandler"/> �ӿڣ����ڴ����ض�ʵ��Ϳ�ѰַRPC·���������͵�·�ɡ�
    /// </summary>
    /// <typeparam name="TEntity">ʵ�����͡�</typeparam>
    /// <typeparam name="TRouteRequest">��ѰַRPC·���������͡�</typeparam>
    /// <typeparam name="TRouteResponse">��ѰַRPC·����Ӧ���͡�</typeparam>
    public abstract class AddressableRPC<TEntity, TRouteRequest, TRouteResponse> : IRouteMessageHandler where TEntity : Entity where TRouteRequest : IAddressableRouteRequest where TRouteResponse : IAddressableRouteResponse
    {
        /// <summary>
        /// ��ȡ��Ϣ���͡�
        /// </summary>
        /// <returns>��Ϣ���͡�</returns>
        public Type Type()
        {
            return typeof(TRouteRequest);
        }

        /// <summary>
        /// �����ѰַRPC·������
        /// </summary>
        /// <param name="session">�Ự��</param>
        /// <param name="entity">ʵ�塣</param>
        /// <param name="rpcId">RPC��ʶ��</param>
        /// <param name="routeMessage">��ѰַRPC·������</param>
        public async FTask Handle(Session session, Entity entity, uint rpcId, object routeMessage)
        {
            if (routeMessage is not TRouteRequest tRouteRequest)
            {
                Log.Error($"Message type conversion error: {routeMessage.GetType().FullName} to {typeof(TRouteRequest).Name}");
                return;
            }

            if (entity is not TEntity tEntity)
            {
                Log.Error($"Route type conversion error: {entity.GetType().Name} to {nameof(TEntity)}");
                return;
            }
            
            var isReply = false;
            var response = Activator.CreateInstance<TRouteResponse>();
            
            void Reply()
            {
                if (isReply)
                {
                    return;
                }

                isReply = true;

                if (session.IsDisposed)
                {
                    return;
                }
                
                session.Send(response, rpcId);
            }
            
            try
            {
                await Run(tEntity, tRouteRequest, response, Reply);
            }
            catch (Exception e)
            {
                if (entity is not Scene scene)
                {
                    scene = entity.Scene;
                }
                
                Log.Error($"SceneWorld:{session.Scene.World?.Id} ServerConfigId:{scene.Server?.Id} SceneType:{scene.SceneType} EntityId {tEntity.Id} : Error {e}");
                response.ErrorCode = CoreErrorCode.ErrRpcFail;
            }
            finally
            {
                Reply();
            }
        }

        /// <summary>
        /// ���д����ѰַRPC·������
        /// </summary>
        /// <param name="entity">ʵ�塣</param>
        /// <param name="request">��ѰַRPC·������</param>
        /// <param name="response">��ѰַRPC·����Ӧ��</param>
        /// <param name="reply">�ظ�������</param>
        protected abstract FTask Run(TEntity entity, TRouteRequest request, TRouteResponse response, Action reply);
    }
}
#endif