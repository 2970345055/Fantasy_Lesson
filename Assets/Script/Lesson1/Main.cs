using System;
using System.Collections;
using System.Collections.Generic;
using Fantasy.Helper;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Fantasy.Entry.Initialize();
        //单例管理系统初始化
        SingletonSystem.Initialize();
        
        AssemblyManager.Load(1,GetType().Assembly);
    }

    private void Update()
    {
       
    }
}
