using System;
using System.Collections;
using System.Collections.Generic;
using Fantasy;
using Fantasy.Core;
using Fantasy.Helper;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.UI;

public class Lesson20 : MonoBehaviour
{
    private void Awake()
    {
        var scene=Fantasy.Entry.Initialize();
        AssemblyManager.Load(1,GetType().Assembly);
        
        //添加一个UI组件
        var uiComponent=scene.AddComponent<UIComponent>();
        //初始化 设置画布大小
        uiComponent.Initialize(1080,1920,CanvasScaler.ScaleMode.ScaleWithScreenSize);
        
        //显示一个UI
        uiComponent.AddComponent<Login>();
    }
    // Update is called once per frame
    void Update()
    {
        ThreadSynchronizationContext.Main.Update();
        SingletonSystem.Update();
    }

    private void OnApplicationQuit()
    {
        EventSystem.Instance?.Publish(new OnAppClosed());
        Fantasy.Application.Close();
    }
}
