using System;

namespace Fantasy
{
    /// <summary>
    /// ��ʾһ�����ڷ����л���ϵͳ�ӿڣ���չ�� <see cref="IEntitiesSystem"/>��
    /// </summary>
    public interface IDeserializeSystem : IEntitiesSystem { }

    /// <summary>
    /// ��ʾһ�����ڷ����л��ض�����ʵ���ϵͳ�����࣬��չ�� <see cref="IDeserializeSystem"/>��
    /// </summary>
    /// <typeparam name="T">Ҫ�����л��� Entity ���͡�</typeparam>
    public abstract class DeserializeSystem<T> : IDeserializeSystem where T : Entity
    {
        /// <summary>
        /// ��ȡ��ϵͳ���ڴ����ʵ�����͡�
        /// </summary>
        /// <returns>ʵ�����͡�</returns>
        public Type EntitiesType() => typeof(T);

        /// <summary>
        /// ����������ʵ�֣����ڷ����л�ָ����ʵ�塣
        /// </summary>
        /// <param name="self">Ҫ�����л���ʵ�塣</param>
        protected abstract void Deserialize(T self);

        /// <summary>
        /// ����ʵ��ķ����л�������
        /// </summary>
        /// <param name="self">Ҫ�����л���ʵ�塣</param>
        public void Invoke(Entity self)
        {
            // �������ʵ��ת��Ϊ�������Ͳ����÷����л�������
            Deserialize((T) self);
        }
    }
}