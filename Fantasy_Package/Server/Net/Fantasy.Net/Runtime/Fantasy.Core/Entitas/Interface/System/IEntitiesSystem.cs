using System;

namespace Fantasy
{
    /// <summary>
    /// ����ʵ��ϵͳ�Ľӿڡ�ʵ��ϵͳ���ڹ����ض����͵�ʵ�壬ִ���������ʵ����ص��߼���
    /// </summary>
    public interface IEntitiesSystem
    {
        /// <summary>
        /// ��ȡʵ��ϵͳ�������ʵ�����͡�
        /// </summary>
        /// <returns>ʵ�����͡�</returns>
        public Type EntitiesType();

        /// <summary>
        /// ��ʵ��ϵͳ��ִ���ض�ʵ����߼��������ʵ��Ӧ��������ʵ�֡�
        /// </summary>
        /// <param name="entity">��Ҫִ���߼���ʵ�塣</param>
        void Invoke(Entity entity);
    }
}