using System;

namespace Fantasy
{
    /// <summary>
    /// ����ʵ�����ϵͳ�Ľӿڡ�ʵ�����ϵͳ���ڹ����ض����͵�ʵ�壬��ÿ�θ���ʱִ���������ʵ����ص��߼���
    /// </summary>
    public interface IUpdateSystem : IEntitiesSystem { }

    /// <summary>
    /// ��ʾʵ�����ϵͳ�ĳ�����ࡣ�̳д�����Զ����ض�����ʵ��ĸ����߼���
    /// </summary>
    /// <typeparam name="T">ʵ�����ͣ�����̳���Entity��</typeparam>
    public abstract class UpdateSystem<T> : IUpdateSystem where T : Entity
    {
        /// <summary>
        /// ��ȡʵ�����ϵͳ�������ʵ�����͡�
        /// </summary>
        /// <returns>ʵ�����͡�</returns>
        public Type EntitiesType() => typeof(T);

        /// <summary>
        /// ��ʵ�����ϵͳ��ִ���ض�ʵ��ĸ����߼��������ʵ��Ӧ��������ʵ�֡�
        /// </summary>
        /// <param name="self">��Ҫִ�и����߼���ʵ�塣</param>
        protected abstract void Update(T self);

        /// <summary>
        /// ��ʵ�����ϵͳ�е��ø����߼���
        /// </summary>
        /// <param name="self">��Ҫִ�и����߼���ʵ�塣</param>
        public void Invoke(Entity self)
        {
            Update((T) self);
        }
    }
}