using System.Collections;
using System.Collections.Generic;
using Fantasy.Helper;
using UnityEngine;

public class IdFactoryDemo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //IdFactory
        //数据库自++  
        //如果合区 容易出现相同ID  数据量太大容易出错
        
        //使用雪花算法、时间戳、+自增的变量+订单号、自定义的值
        //IdFactory 包含：时间戳、进程ID、世界ID(World)  
        //可以通过这个ID 随时可以得到 进程，在哪个世界  保证不重复
        
        //using Fantasy.Helper;
        //生成一个NextEntityId
        var nextEntityId=IdFactory.NextEntityId(1024);//给一个ServerID （该ID会在服务器启动是自动生成）
       
         //将一个ID 转换成结构体   
        var a=(EntityIdStruct)nextEntityId;
        
        //获得 进程ID  时间ID  世界ID
        //a.AppId
        //a.Time
        //a.WordId
        
        //即使在同一服务器中 nextEntityId 也永远不会重复
        
        //一般用于数据库保存使用 或者一些数据类使用  方便查找信息
        
        //EntityId最多每毫秒生成65000个
        
        //NextRunTimeId   运行时的ID 不包括任何位置信息（server,world） 只在当前运行时唯一
        //RuntimeIdStruct

        var nextRunTimeId=IdFactory.NextRunTimeId();
        // var b = (RuntimeIdStruct)nextRunTimeId;
    }   

}
