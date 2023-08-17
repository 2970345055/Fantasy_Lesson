using System.Threading.Tasks;
#pragma warning disable CS8601
#pragma warning disable CS8618

namespace Fantasy.Helper
{
    /// <summary>
    /// ����ĵ������࣬�̳��� <see cref="ISingleton"/> �ӿڡ�
    /// </summary>
    /// <typeparam name="T">�������͡�</typeparam>
    public abstract class Singleton<T> : ISingleton where T : ISingleton, new()
    {
        /// <summary>
        /// ��ȡ�����õ����Ƿ��ѱ����١�
        /// </summary>
        public bool IsDisposed { get; set; }

        /// <summary>
        /// ��ȡ������ʵ����
        /// </summary>
        public static T Instance { get; private set; }

        /// <summary>
        /// ע�ᵥ���ķ�����
        /// </summary>
        /// <param name="singleton">��������</param>
        private void RegisterSingleton(ISingleton singleton)
        {
            Instance = (T) singleton;
            AssemblyManager.OnLoadAssemblyEvent += OnLoad;
            AssemblyManager.OnUnLoadAssemblyEvent += OnUnLoad;
        }

        /// <summary>
        /// ��ʼ�������ķ�����
        /// </summary>
        /// <returns>��ʾ�첽����������</returns>
        public virtual Task Initialize()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// �ڳ��򼯼���ʱִ�еķ�����
        /// </summary>
        /// <param name="assemblyName">�������ơ�</param>
        protected virtual void OnLoad(int assemblyName) { }

        /// <summary>
        /// �ڳ���ж��ʱִ�еķ�����
        /// </summary>
        /// <param name="assemblyName">�������ơ�</param>
        protected virtual void OnUnLoad(int assemblyName) { }

        /// <summary>
        /// �ͷŵ����ķ�����
        /// </summary>
        public virtual void Dispose()
        {
            IsDisposed = true;
            Instance = default;
            AssemblyManager.OnLoadAssemblyEvent -= OnLoad;
            AssemblyManager.OnUnLoadAssemblyEvent -= OnUnLoad;
        }
    }
}