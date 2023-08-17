using System;

namespace Fantasy
{
    /// <summary>
    /// ����ʵ������ϵͳ�ӿڡ�����Ҫ��ʵ������ʱִ���ض����߼�ʱ��Ӧʵ�ִ˽ӿڡ�
    /// </summary>
    public interface IDestroySystem : IEntitiesSystem { }

    /// <summary>
    /// ��ʾ����ʵ��ʵ�������߼��ĳ�����ࡣ�̳д�����������ڴ����ض����͵�ʵ������ٲ�����
    /// </summary>
    /// <typeparam name="T">��Ҫ���������߼���ʵ�����͡�</typeparam>
    public abstract class DestroySystem<T> : IDestroySystem where T : Entity
    {
        /// <summary>
        /// ��ȡ��Ҫ���������߼���ʵ�����͡�
        /// </summary>
        /// <returns>ʵ�����͡�</returns>
        public Type EntitiesType() => typeof(T);

        /// <summary>
        /// ��ʵ������ʱִ�е��߼�������Ӧʵ�ִ˷����Դ����ض�ʵ�����͵����ٲ�����
        /// </summary>
        /// <param name="self">�������ٵ�ʵ�塣</param>
        protected abstract void Destroy(T self);

        /// <summary>
        /// ����ʵ��������߼�����ʵ������ʱ������ô˷�����ִ����Ӧ�����ٲ�����
        /// </summary>
        /// <param name="self">�������ٵ�ʵ�塣</param>
        public void Invoke(Entity self)
        {
            Destroy((T) self);
        }
    }
}