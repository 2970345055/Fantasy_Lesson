using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fantasy.Helper;
using UnityEngine;

namespace Script.Lesson1
{
    public class SingletonSystemLesson:Singleton<SingletonSystemLesson>,IUpdateSingleton
    {
        public int age;

        public List<Type> Types = new List<Type>();
        //初始化
        public override Task Initialize()
        {
            age = 20;
            return base.Initialize();
        }
    
        public void Update()
        {
            
        }


        /// <summary>
        /// 当前装载的程序集
        /// </summary>
        /// <param name="assemblyName"></param>
        protected override void OnLoad(int assemblyName)
        {   
            //把继承了test 的类存入
            foreach (var type in AssemblyManager.ForEach(assemblyName, typeof(ISingleton)))
            {
                Types.Add(type);
            }
            Debug.Log($"Onload assemblyName{assemblyName}");   
            
            base.OnLoad(assemblyName);
        }
        
        
        /// <summary>
        /// 当前卸载的程序集
        /// </summary>
        /// <param name="assemblyName"></param>
        protected override void OnUnLoad(int assemblyName)
        {   
            Types.Clear();
            Debug.Log($"OnUnLoad assemblyName{assemblyName}");   
            base.OnUnLoad(assemblyName);
        }

        public void Show()
        {
            Debug.Log($"Lesson4Demo age:{age}");
        }
    }
}