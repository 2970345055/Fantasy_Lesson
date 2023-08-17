using System;
using System.Collections.Generic;
using System.Reflection;
#if FANTASY_NET
using System.Runtime.Loader;
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
#endif
#pragma warning disable CS8603
#pragma warning disable CS8618
namespace Fantasy.Helper
{
    /// <summary>
    /// ������򼯼��غ�ж�صİ����ࡣ
    /// </summary>
    public static class AssemblyManager
    {
        /// <summary>
        /// �����򼯼���ʱ�������¼���
        /// </summary>
        public static event Action<int> OnLoadAssemblyEvent;
        /// <summary>
        /// ������ж��ʱ�������¼���
        /// </summary>
        public static event Action<int> OnUnLoadAssemblyEvent;
        /// <summary>
        /// �����¼��س���ʱ�������¼���
        /// </summary>
        public static event Action<int> OnReLoadAssemblyEvent;
        /// <summary>
        /// �洢�Ѽ��صĳ�����Ϣ���ֵ䡣
        /// </summary>
        private static readonly Dictionary<int, AssemblyInfo> AssemblyList = new Dictionary<int, AssemblyInfo>();

        /// <summary>
        /// ��ʼ�� AssemblyManager�����ص�ǰ���򼯡�
        /// </summary>
        public static void Initialize()
        {
            LoadAssembly(int.MaxValue, typeof(AssemblyManager).Assembly);
        }

        /// <summary>
        /// ����ָ���ĳ��򼯣���������Ӧ���¼���
        /// ��MaxValue�жϡ�
        /// </summary>
        /// <param name="assemblyName">�������ơ�</param>
        /// <param name="assembly">Ҫ���صĳ��򼯡�</param>
        public static void LoadAssembly(int assemblyName, Assembly assembly)
        {
            var isReLoad = false;

            // ����Ƿ��Ѿ�������ͬ���Ƶĳ���
            if (!AssemblyList.TryGetValue(assemblyName, out var assemblyInfo))
            {
                assemblyInfo = new AssemblyInfo();
                AssemblyList.Add(assemblyName, assemblyInfo);
            }
            else
            {
                // ���Ѵ��ڣ����ʾ���¼���
                isReLoad = true;
                // ж��֮ǰ�ĳ���
                assemblyInfo.Unload();

                // ���� OnUnLoadAssemblyEvent �¼���֪ͨ���򼯱�ж��
                if (OnUnLoadAssemblyEvent != null)
                {
                    OnUnLoadAssemblyEvent(assemblyName);
                }
            }

            // �����µĳ���
            assemblyInfo.Load(assembly);

            // ���� OnLoadAssemblyEvent �¼���֪ͨ�����Ѽ���
            if (OnLoadAssemblyEvent != null)
            {
                OnLoadAssemblyEvent(assemblyName);
            }

            // ��Ϊ���¼����Ҵ��� OnReLoadAssemblyEvent �¼����򴥷����¼���֪ͨ���������¼���
            if (isReLoad && OnReLoadAssemblyEvent != null)
            {
                OnReLoadAssemblyEvent(assemblyName);
            }
        }

        /// <summary>
        /// ����ָ���ĳ��򼯡���MaxValue�ж�
        /// </summary>
        /// <param name="assemblyName">�������ơ�</param>
        /// <param name="assembly">Ҫ���صĳ��򼯡�</param>
        /// <exception cref="NotSupportedException">����������Ϊ <see cref="int.MaxValue"/> ʱ���׳��쳣��</exception>
        public static void Load(int assemblyName, Assembly assembly)
        {
            if (int.MaxValue == assemblyName)
            {
                throw new NotSupportedException("AssemblyName cannot be 2147483647");
            }

            LoadAssembly(assemblyName, assembly);
        }

        /// <summary>
        /// ��ȡ�����Ѽ��س��򼯵����ơ�
        /// </summary>
        /// <returns>�����Ѽ��س��򼯵����ơ�</returns>
        public static IEnumerable<int> ForEachAssemblyName()
        {
            foreach (var (key, _) in AssemblyList)
            {
                yield return key;
            }
        }

        /// <summary>
        /// ��ȡ�����Ѽ��س����е��������͡�
        /// </summary>
        /// <returns>�����Ѽ��س����е����͡�</returns>
        public static IEnumerable<Type> ForEach()
        {
            foreach (var (_, assemblyInfo) in AssemblyList)
            {
                foreach (var type in assemblyInfo.AssemblyTypeList)
                {
                    yield return type;
                }
            }
        }

        /// <summary>
        /// ��ȡָ�������е��������͡�
        /// </summary>
        /// <param name="assemblyName">�������ơ�</param>
        /// <returns>ָ�������е����͡�</returns>
        public static IEnumerable<Type> ForEach(int assemblyName)
        {
            if (!AssemblyList.TryGetValue(assemblyName, out var assemblyInfo))
            {
                yield break;
            }

            foreach (var type in assemblyInfo.AssemblyTypeList)
            {
                yield return type;
            }
        }

        /// <summary>
        /// ��ȡ�����Ѽ��س�����ʵ��ָ�����͵��������͡�
        /// </summary>
        /// <param name="findType">Ҫ���ҵĻ����ӿ����͡�</param>
        /// <returns>�����Ѽ��س�����ʵ��ָ�����͵����͡�</returns>
        public static IEnumerable<Type> ForEach(Type findType)
        {
            foreach (var (_, assemblyInfo) in AssemblyList)
            {
                if (!assemblyInfo.AssemblyTypeGroupList.TryGetValue(findType, out var assemblyLoad))
                {
                    yield break;
                }
                
                foreach (var type in assemblyLoad)
                {
                    yield return type;
                }
            }
        }

        /// <summary>
        /// ��ȡָ��������ʵ��ָ�����͵��������͡�
        /// </summary>
        /// <param name="assemblyName">�������ơ�</param>
        /// <param name="findType">Ҫ���ҵĻ����ӿ����͡�</param>
        /// <returns>ָ��������ʵ��ָ�����͵����͡�</returns>
        public static IEnumerable<Type> ForEach(int assemblyName, Type findType)
        {
            if (!AssemblyList.TryGetValue(assemblyName, out var assemblyInfo))
            {
                yield break;
            }

            if (!assemblyInfo.AssemblyTypeGroupList.TryGetValue(findType, out var assemblyLoad))
            {
                yield break;
            }
            
            foreach (var type in assemblyLoad)
            {
                yield return type;
            }
        }

        /// <summary>
        /// ��ȡָ�����򼯵�ʵ����
        /// </summary>
        /// <param name="assemblyName">�������ơ�</param>
        /// <returns>ָ�����򼯵�ʵ�������δ�����򷵻� null��</returns>
        public static Assembly GetAssembly(int assemblyName)
        {
            return !AssemblyList.TryGetValue(assemblyName, out var assemblyInfo) ? null : assemblyInfo.Assembly;
        }

        /// <summary>
        /// �ͷ���Դ��ж�����м��صĳ��򼯡�
        /// </summary>
        public static void Dispose()
        {
            // ж�������Ѽ��صĳ���
            foreach (var (_, assemblyInfo) in AssemblyList)
            {
                assemblyInfo.Unload();
            }

            // ����Ѽ��صĳ����б�
            AssemblyList.Clear();

            // �Ƴ������¼���������Ա����¼�й©���ڴ�й©
            if (OnLoadAssemblyEvent != null)
            {
                foreach (var @delegate in OnLoadAssemblyEvent.GetInvocationList())
                {
                    OnLoadAssemblyEvent -= @delegate as Action<int>;
                }
            }
            
            if (OnUnLoadAssemblyEvent != null)
            {
                foreach (var @delegate in OnUnLoadAssemblyEvent.GetInvocationList())
                {
                    OnUnLoadAssemblyEvent -= @delegate as Action<int>;
                }
            }
            
            if (OnReLoadAssemblyEvent != null)
            {
                foreach (var @delegate in OnReLoadAssemblyEvent.GetInvocationList())
                {
                    OnReLoadAssemblyEvent -= @delegate as Action<int>;
                }
            }
        }
    }
}