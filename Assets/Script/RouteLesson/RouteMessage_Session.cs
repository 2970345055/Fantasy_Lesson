<<<<<<< HEAD

    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Fantasy;
    using Fantasy.Core.Network;
    using Fantasy.Helper;
    using UnityEngine;
    public class RouteMessage_Session:MonoBehaviour
    {
        private void Start()
        {
            var secen = Fantasy.Entry.Initialize();
            AssemblyManager.Load(1,GetType().Assembly);
            secen.CreateSession("127.0.0.1:20000", NetworkProtocolType.KCP, ()=>{Log.Debug("连接成功");}, null, null, 5000);

          //  Request(secen.Session);
          LoginRequest(secen.Session);
        }
            
        public async FTask Request(Session session)
        {
            var messageResponse=(H_C2G_TestMessageResponse)await session.Call(new H_C2G_TestMessageRquest()
            {
                Message = "客户端请求Chat服务器"
            });
            
            Log.Debug($"Chat服务器回复消息{messageResponse.Message}");
            
            session.Send(new  H_C2Chat_TestMessage()
            {
                Message = "你好Chat服务器"
            });
        }

        public async FTask LoginRequest(Session session)
        {
            var messageResponse=(H_G2C_LoginResponse)await session.Call(new H_C2G_LoginRequest()
            {
              UserName = "LiZiNull",
              Password = "123456"
            });
            
            Log.Debug($"Chat服务器回复消息{messageResponse.Text}");
            
            session.Send(new  H_C2Chat_TestMessage()
            {
                Message = "你好Chat服务器!!!!"
            });
        }
    }
    
    
    public class ChatUnitManager : Entity
    {
        public readonly Dictionary<long, ChatUnit> Units = new Dictionary<long, ChatUnit>();
        
        
    }

    public class ChatUnit : Entity
    {
        public string ChatName;
        public long ChatId;
    }
=======
namespace Script.RouteLesson
{
    public class RouteMessage_Session
    {
        
    }
}
>>>>>>> main
