using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Fantasy.Helper;
using UnityEngine;


public interface test
{
    void name();
}


public class AssemblYDemo2:test
{
    public void name()
    {
        Debug.Log(23333);
    }
}

public class AssemblyManagerDemo : MonoBehaviour,test
{
  
    void Start()
    {
        //AssemblyManager介绍：
       // 框架里的的如: 网络消息协议、事件等、都使用了反射来实现的
       //框架启动的时候会把不同程序集通过反射拿到继承接口或特性的类、放入缓存中
       //在调用的时候、在缓存中获取、从而提高性能、而不是每次调用都通过反射执行
       //可以同时加载多个程序集、这个是框架的核心工具了
       
       
        // Load的参数
        // assemblyName:自定义的程序集名称、用于区别不同的程序集
        // assembly:Assembly程序集对象
        // 比如加载Lesson03所在的程序集
        
        //如果加载两次那么就会 先加载 在卸载 在加载
        AssemblyManager.Load(1,typeof(AssemblyManagerDemo).Assembly);
        AssemblyManager.Load(1,typeof(AssemblyManagerDemo).Assembly);
        // ForEachAssemblyName:返回当前框架已经加载的程序集列表
        // 这里返回是就是上面自定义的assemblyName
        foreach (var assembly in AssemblyManager.ForEachAssemblyName())
        {
            Debug.Log($"assembly{assembly}");
        }
        
        Debug.Log("-------------------------------------");
        // ForEach:根据类型或指定的assemblyName返回类型列表// 三个重载方法:
        
        // 1、返回某一个assemblyName下的所有类型
        foreach (var type in   AssemblyManager.ForEach(1))
        {
            Debug.Log($"type{type}");
        }
        // 2、在所有程序集中查找实现某一个接口的所有类型
        Debug.Log("-------------------------------------");
        foreach (var type in   AssemblyManager.ForEach(1,typeof(test)))
        {
            Debug.Log($"type{type}");
        }
        // 3、返回assemblyName下的某一个接口的所有类型
        Debug.Log("-------------------------------------");
        foreach (var type in  AssemblyManager.ForEach(typeof(test)))
        {
            Debug.Log($"type{type}");
            if (type==typeof(AssemblyManagerDemo))
            {   
                //使用方法
                MethodInfo method = type.GetMethod("name");
                // 创建实例
                object instance = Activator.CreateInstance(type);
                // 调用方法
                method.Invoke(instance, null);
            }
            Debug.Log("231232131");
        }
        
        
        //卸载
      //  AssemblyManager.Load(1,null);
    }
    
    public void name()
    {
       print(2222);
    }
}
