using System;
using System.Collections;
using System.Collections.Generic;
using Fantasy;
using Fantasy.Core.Network;
using UnityEngine;

public class SessionLesson : MonoBehaviour
{
    
    
         /*// CreateSession有五个参数:

        remoteAddress:连接的服务器地址 地址在Demo下Config/Excel/Server里四个文件配置出的 192.168.0.1:20000

        networkProtocolType:网络协议类型、目前只有TCP和UDP(KCP)这两种

        onConnectComplete:当跟服务器建立连接后的回调

        onConnectFail:当网络无法连接或出错时的回调

        onConnectDisconnect;当服务器主动断开连接客户端收到的回调

        connectTimeout:连接超时时间、默认是5000毫秒*/

         private void Start()
         {
             var secen = Fantasy.Entry.Initialize();
             secen.CreateSession("127.0.0.1:20000",NetworkProtocolType.TCP, () =>//连接成功时
             {
                 Debug.Log("连接成功");
                 OnConnectComplete();
             },
                 () =>//连接失败时
                 {
                     OnConnectFail();
                 },
                 () =>//关闭连接时
                 {
                     OnOnnectDisConnect();
                 },2000);

             var session = secen.Session;
             
             session.Send(new H_C2M_Message()//Send 方法需要使用继承了 IAddressableRouteMessage接口的数据
             {
                 Message = "Hell World!!!"
             });

         }
        //异步Call方法
         public async FTask Respone(Session session)
         {
             var response =(H_G2C_MessageResponse) await session.Call(new H_C2G_MessageRequest()//Call方法需要使用继承了IRequest 接口的数据
             {
                 Message = "这是一个Call 方法！！！！"
             });
             
             if (response.ErrorCode==0)
             {
                 Debug.Log($"接受信息成功{response.Message}");
                 return;
             }
             else
             {
                 Debug.Log($"接受信息失败{response.ErrorCode}");
             }
         }

         public void OnConnectComplete()
         {
             
         }

         public void OnConnectFail()
         {
             
         }

         public void OnOnnectDisConnect()
         {
             
         }
}
