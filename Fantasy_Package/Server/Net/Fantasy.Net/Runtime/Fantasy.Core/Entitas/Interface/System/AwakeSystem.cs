using System;

namespace Fantasy
{
    /// <summary>
    /// ����ʵ��Ļ���ϵͳ�ӿڡ�����Ҫ��ʵ�廽��ʱִ���ض����߼�ʱ��Ӧʵ�ִ˽ӿڡ�
    /// </summary>
    public interface IAwakeSystem : IEntitiesSystem { }

    /// <summary>
    /// ��ʾ����ʵ��ʵ�廽���߼��ĳ�����ࡣ�̳д�����������ڴ����ض����͵�ʵ��Ļ��Ѳ�����
    /// </summary>
    /// <typeparam name="T">��Ҫ�������߼���ʵ�����͡�</typeparam>
    public abstract class AwakeSystem<T> : IAwakeSystem where T : Entity
    {
        /// <summary>
        /// ��ȡ��Ҫ�������߼���ʵ�����͡�
        /// </summary>
        /// <returns>ʵ�����͡�</returns>
        public Type EntitiesType() => typeof(T);

        /// <summary>
        /// ��ʵ�廽��ʱִ�е��߼�������Ӧʵ�ִ˷����Դ����ض�ʵ�����͵Ļ��Ѳ�����
        /// </summary>
        /// <param name="self">���ڻ��ѵ�ʵ�塣</param>
        protected abstract void Awake(T self);

        /// <summary>
        /// ����ʵ��Ļ����߼�����ʵ�廽��ʱ������ô˷�����ִ����Ӧ�Ļ��Ѳ�����
        /// </summary>
        /// <param name="self">���ڻ��ѵ�ʵ�塣</param>
        public void Invoke(Entity self)
        {
            Awake((T) self);
        }
    }
}