using System;
using System.Collections;
using System.Collections.Generic;
using Fantasy;
using Fantasy.Helper;
using Script.实体;
using Unity.VisualScripting;
using UnityEngine;

public class EntityLesson : MonoBehaviour
{
    // Start is called before the first frame update
    private User user = null;
    void Start()
    {   
        //初始化会返回一个Scene
        var FirstScene=Fantasy.Entry.Initialize();
        AssemblyManager.Load(1,GetType().Assembly);
        //创建一个实体
        user = Entity.Create<User>(FirstScene);
        user.name = "ZKM";
        user.age = 10;
        
        //调用方法
      //  user.Add();
        
       // EventSystem.Instance.Publish(user);
        //在实体上挂载一个实体
        Bag bag1 = Entity.Create<Bag>(FirstScene, false);
        //1.
        user.AddComponent<Bag>();
        //2.
       // user.AddComponent(bag1);
        
        //获取到该物体的组件
        user.GetComponent<Bag>();
        //user.GetComponent(typeof(Bag));
        
        //删除组件
        user.RemoveComponent<Bag>();
        
        //销毁
     //   user.Dispose();
        //user.IsDisposed;
        
        //反序列号一个实体
       // user.Deserialize(FirstScene);
        
        //Clone 克隆实体
         //user.Clone();
         
         //ISupportedMultiEntity
         //添加相同组件时要设置ID 放置出现重复错误
      //  Bag bag2= user.AddComponent<Bag>(2);

      //  var bag3 = user.GetComponent<Bag>(bag2.Id);
        
       // user.RemoveComponent<Bag>(bag3.Id);
        
        
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
        }
    }
}
