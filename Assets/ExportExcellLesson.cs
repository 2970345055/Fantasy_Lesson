using System.Collections;
using System.Collections.Generic;
using Fantasy;
using UnityEngine;

public class ExportExcellLesson : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {     
        Fantasy.Entry.Initialize();
        
        //ServerConfigData.cs
        
        //遍历所有表内数据
        foreach (var serverConfig in  ServerConfigData.Instance.List)
        {
          Debug.Log($"ConfigId{serverConfig.Id}");
        }
        //通过ID直接查找
        var config = ServerConfigData.Instance.Get(1024);
        Debug.Log($"configID{config}");
        
        //尝试获取ID 输入相似ID
        if (!ServerConfigData.Instance.TryGet(102411, out var newConfig))
        {
            Debug.Log($"没有找到ServerId为：{102411}");
            return;
        }
        Debug.Log($"找到ServerId为：{newConfig}");
    }
}
