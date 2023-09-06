using System.Collections;
using System.Collections.Generic;
using Fantasy;
using Fantasy.Core.Network;
using Fantasy.Helper;
using UnityEngine;

public class AddressLesson : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var secen = Fantasy.Entry.Initialize();
        AssemblyManager.Load(1,GetType().Assembly);
        secen.CreateSession("127.0.0.1:20000", NetworkProtocolType.KCP, ()=>{Log.Debug("连接成功");}, null, null, 5000);
        LoginRequest(secen.Session);
    }
    public async FTask LoginRequest(Session session)
    {
        var messageResponse=(H_G2C_LoginAddressResponse)await session.Call(new H_C2G_LoginAddressRequest()
        {
           Message = "123456"
        });

        if (messageResponse.ErrorCode!=0)
        {   Log.Debug($"H_C2G_LoginAddressRequest error {messageResponse.ErrorCode}");
            return;
        }
        Log.Debug("注册成功");
        
        session.Send(new H_C2M_Message()
        {
            Message = "Address 连接成功了吗"
        });
    }
}
