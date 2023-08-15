using System;

namespace Fantasy
{
    /// <summary>
    /// �����¼��Ľӿڡ�
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// ��ȡ�¼������͡�
        /// </summary>
        /// <returns>�¼������͡�</returns>
        Type EventType();
        /// <summary>
        /// �����¼���������
        /// </summary>
        /// <param name="self">�¼���ʵ����</param>
        void Invoke(object self);
    }

    /// <summary>
    /// �����첽�¼��Ľӿڡ�
    /// </summary>
    public interface IAsyncEvent
    {
        /// <summary>
        /// ��ȡ�¼������͡�
        /// </summary>
        /// <returns>�¼������͡�</returns>
        Type EventType();
        /// <summary>
        /// �첽�����¼���������
        /// </summary>
        /// <param name="self">�¼���ʵ����</param>
        /// <returns>��ʾ�첽����������</returns>
        FTask InvokeAsync(object self);
    }

    /// <summary>
    /// �¼�ϵͳ�ĳ�����ࡣ
    /// </summary>
    /// <typeparam name="T">�¼������͡�</typeparam>
    public abstract class EventSystem<T> : IEvent
    {
        private readonly Type _selfType = typeof(T);

        /// <summary>
        /// ��ȡ�¼������͡�
        /// </summary>
        /// <returns>�¼������͡�</returns>
        public Type EventType()
        {
            return _selfType;
        }

        /// <summary>
        /// ͬ�������¼��ķ�����
        /// </summary>
        /// <param name="self">�¼���ʵ����</param>
        public abstract void Handler(T self);

        /// <summary>
        /// �����¼���������
        /// </summary>
        /// <param name="self">�¼���ʵ����</param>
        public void Invoke(object self)
        {
            try
            {
                Handler((T) self);
            }
            catch (Exception e)
            {
                Log.Error($"{_selfType.Name} Error {e}");
            }
        }
    }

    /// <summary>
    /// �첽�¼�ϵͳ�ĳ�����ࡣ
    /// </summary>
    /// <typeparam name="T">�¼������͡�</typeparam>
    public abstract class AsyncEventSystem<T> : IAsyncEvent
    {
        private readonly Type _selfType = typeof(T);

        /// <summary>
        /// ��ȡ�¼������͡�
        /// </summary>
        /// <returns>�¼������͡�</returns>
        public Type EventType()
        {
            return _selfType;
        }

        /// <summary>
        /// �첽�����¼��ķ�����
        /// </summary>
        /// <param name="self">�¼���ʵ����</param>
        /// <returns>��ʾ�첽����������</returns>
        public abstract FTask Handler(T self);

        /// <summary>
        /// �첽�����¼���������
        /// </summary>
        /// <param name="self">�¼���ʵ����</param>
        /// <returns>��ʾ�첽����������</returns>
        public async FTask InvokeAsync(object self)
        {
            try
            {
                await Handler((T) self);
            }
            catch (Exception e)
            {
                Log.Error($"{_selfType.Name} Error {e}");
            }
        }
    }
}