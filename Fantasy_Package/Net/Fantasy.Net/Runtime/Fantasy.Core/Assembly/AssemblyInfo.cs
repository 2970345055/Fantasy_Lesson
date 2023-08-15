using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fantasy.DataStructure;

namespace Fantasy.Helper
{
    /// <summary>
    /// AssemblyInfo�ṩ�йس��򼯺����͵���Ϣ
    /// </summary>
    public sealed class AssemblyInfo
    {
        /// <summary>
        /// ��ȡ��������˳���������� <see cref="Assembly"/> ʵ����
        /// </summary>
        public Assembly Assembly { get; private set; }
        /// <summary>
        /// �������ͼ��ϣ���ȡһ���б������ӳ��򼯼��ص��������͡�
        /// </summary>
        public readonly List<Type> AssemblyTypeList = new List<Type>();
        /// <summary>
        /// �������ͷ��鼯�ϣ���ȡһ�������б����ӿ�����ӳ�䵽ʵ����Щ�ӿڵ����͡�
        /// </summary>
        public readonly OneToManyList<Type, Type> AssemblyTypeGroupList = new OneToManyList<Type, Type>();

        /// <summary>
        /// ��ָ���ĳ��򼯼���������Ϣ�����з��ࡣ
        /// </summary>
        /// <param name="assembly">Ҫ������Ϣ�ĳ��򼯡�</param>
        public void Load(Assembly assembly)
        {
            Assembly = assembly;
            var assemblyTypes = assembly.GetTypes().ToList();

            foreach (var type in assemblyTypes)
            {
                if (type.IsAbstract || type.IsInterface)
                {
                    continue;
                }

                var interfaces = type.GetInterfaces();

                foreach (var interfaceType in interfaces)
                {
                    AssemblyTypeGroupList.Add(interfaceType, type);
                }
            }

            AssemblyTypeList.AddRange(assemblyTypes);
        }

        /// <summary>
        /// ж�س��򼯵�������Ϣ��
        /// </summary>
        public void Unload()
        {
            AssemblyTypeList.Clear();
            AssemblyTypeGroupList.Clear();
        }
    }
}