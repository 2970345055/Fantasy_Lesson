namespace Fantasy.Helper
{
    /// <summary>
    /// ����һ���ɸ��µĵ����ӿڣ��̳��� <see cref="ISingleton"/>��
    /// </summary>
    public interface IUpdateSingleton : ISingleton
    {
        /// <summary>
        /// ���µ���ʵ���ķ�����
        /// </summary>
        public abstract void Update();
    }
}