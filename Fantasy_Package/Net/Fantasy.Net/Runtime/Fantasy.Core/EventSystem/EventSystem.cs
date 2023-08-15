using System;
using System.Threading.Tasks;
using Fantasy.DataStructure;
using Fantasy.Helper;
using Type = System.Type;

// ReSharper disable MethodOverloadWithOptionalParameter

namespace Fantasy
{
    internal sealed class EventInfo
    {
        public readonly Type Type;
        public readonly object Obj;

        public EventInfo(Type type, object obj)
        {
            Type = type;
            Obj = obj;
        }
    }

    /// <summary>
    /// �¼�ϵͳ�࣬���ڼ�ж�س��򼯣������Ͷ����¼���
    /// </summary>
    public sealed class EventSystem : Singleton<EventSystem>
    {
        private readonly OneToManyList<Type, IEvent> _events = new();
        private readonly OneToManyList<Type, IAsyncEvent> _asyncEvents = new();
        
        private readonly OneToManyList<int, EventInfo> _assemblyEvents = new();
        private readonly OneToManyList<int, EventInfo> _assemblyAsyncEvents = new();

        /// <summary>
        /// �ڳ��򼯼���ʱ������Ѱ�Ҳ���ʼ���¼���
        /// </summary>
        /// <param name="assemblyName">������</param>
        protected override void OnLoad(int assemblyName)
        {
            foreach (var type in AssemblyManager.ForEach(assemblyName, typeof(IEvent)))
            {
                var obj = (IEvent) Activator.CreateInstance(type);
                
                if (obj != null)
                {
                    var eventType = obj.EventType();
                    _events.Add(eventType, obj);
                    _assemblyEvents.Add(assemblyName, new EventInfo(eventType, obj));
                }
            }
            
            foreach (var type in AssemblyManager.ForEach(assemblyName, typeof(IAsyncEvent)))
            {
                var obj = (IAsyncEvent) Activator.CreateInstance(type);

                if (obj != null)
                {
                    var eventType = obj.EventType();
                    _asyncEvents.Add(eventType, obj);
                    _assemblyAsyncEvents.Add(assemblyName, new EventInfo(eventType, obj));
                }
            }
        }

        /// <summary>
        /// �ڳ���ж��ʱ���Ƴ�����¼���
        /// </summary>
        /// <param name="assemblyName">������</param>
        protected override void OnUnLoad(int assemblyName)
        {
            if (_assemblyEvents.TryGetValue(assemblyName, out var events))
            {
                foreach (var @event in events)
                {
                    _events.RemoveValue(@event.Type, (IEvent)@event.Obj);
                }

                _assemblyEvents.RemoveByKey(assemblyName);
            }

            if (_assemblyAsyncEvents.TryGetValue(assemblyName, out var asyncEvents))
            {
                foreach (var @event in asyncEvents)
                {
                    _asyncEvents.RemoveValue(@event.Type, (IAsyncEvent)@event.Obj);
                }

                _assemblyAsyncEvents.RemoveByKey(assemblyName);
            }
        }

        /// <summary>
        /// ����һ��ֵ���͵��¼����ݡ�
        /// </summary>
        /// <typeparam name="TEventData">�¼��������ͣ�ֵ���ͣ���</typeparam>
        /// <param name="eventData">�¼�����ʵ����</param>
        public void Publish<TEventData>(TEventData eventData) where TEventData : struct
        {
            if (!_events.TryGetValue(eventData.GetType(), out var list))
            {
                return;
            }
            
            foreach (var @event in list)
            {
                try
                {
                    @event.Invoke(eventData);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        /// <summary>
        /// ����һ���̳��� Entity ���¼����ݡ�
        /// </summary>
        /// <typeparam name="TEventData">�¼��������ͣ��̳��� Entity����</typeparam>
        /// <param name="eventData">�¼�����ʵ����</param>
        /// <param name="isDisposed">�Ƿ��ͷ��¼����ݡ�</param>
        public void Publish<TEventData>(TEventData eventData, bool isDisposed = true) where TEventData : Entity
        {
            if (!_events.TryGetValue(typeof(TEventData), out var list))
            {
                return;
            }
            
            foreach (var @event in list)
            {
                try
                {
                    @event.Invoke(eventData);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }

            if (isDisposed)
            {
                eventData.Dispose();
            }
        }

        /// <summary>
        /// �첽����һ��ֵ���͵��¼����ݡ�
        /// </summary>
        /// <typeparam name="TEventData">�¼��������ͣ�ֵ���ͣ���</typeparam>
        /// <param name="eventData">�¼�����ʵ����</param>
        /// <returns>��ʾ�첽����������</returns>
        public async FTask PublishAsync<TEventData>(TEventData eventData) where TEventData : struct
        {
            if (!_asyncEvents.TryGetValue(eventData.GetType(), out var list))
            {
                return;
            }
            
            using var tasks = ListPool<FTask>.Create();

            foreach (var @event in list)
            {
                tasks.Add(@event.InvokeAsync(eventData));
            }

            await FTask.WhenAll(tasks);
        }

        /// <summary>
        /// �첽����һ���̳��� Entity ���¼����ݡ�
        /// </summary>
        /// <typeparam name="TEventData">�¼��������ͣ��̳��� Entity����</typeparam>
        /// <param name="eventData">�¼�����ʵ����</param>
        /// <param name="isDisposed">�Ƿ��ͷ��¼����ݡ�</param>
        /// <returns>��ʾ�첽����������</returns>
        public async FTask PublishAsync<TEventData>(TEventData eventData, bool isDisposed = true) where TEventData : Entity
        {
            if (!_asyncEvents.TryGetValue(eventData.GetType(), out var list))
            {
                return;
            }
            
            using var tasks = ListPool<FTask>.Create();

            foreach (var @event in list)
            {
                tasks.Add(@event.InvokeAsync(eventData));
            }

            await FTask.WhenAll(tasks);

            if (isDisposed)
            {
                eventData.Dispose();
            }
        }

        /// <summary>
        /// ������Դ���¼�����
        /// </summary>
        public override void Dispose()
        {
            _events.Clear();
            _asyncEvents.Clear();
            _assemblyEvents.Clear();
            _assemblyAsyncEvents.Clear();
            AssemblyManager.OnLoadAssemblyEvent -= OnLoad;
            AssemblyManager.OnUnLoadAssemblyEvent -= OnUnLoad;
            base.Dispose();
        }
    }
}