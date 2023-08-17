using System.Collections;
using System.Collections.Generic;
using Fantasy.Helper;
using Script.Lesson1;
using UnityEngine;

public class SingletonSystemDemo : MonoBehaviour
{
    void Start()
    {
        //SingletonSystem是一个与例管理系统
        // 只要实现了SingletonSystem接口的类、会注册到SingletonSystem中并管理
        // 支持程序集加载和卸载的事件
        // 提供一个IUpdateSingleton接口、按赖执行的方法
            
        
        //SingletonSystem.Initialize();
       // AssemblyManager.Load(1,GetType().Assembly);
        SingletonSystemLesson.Instance.Show();
        //AssemblyManager.Load(1,null);
        
    }


}
