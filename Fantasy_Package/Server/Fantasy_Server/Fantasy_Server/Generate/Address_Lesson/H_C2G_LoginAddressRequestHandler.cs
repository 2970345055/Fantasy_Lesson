using Fantasy;
using Fantasy.Core.Network;

namespace Fantasy_Server.Generate.Address_Lesson;

public class H_C2G_LoginAddressRequestHandler:MessageRPC<H_C2G_LoginAddressRequest,H_G2C_LoginAddressResponse>
{
    protected override async FTask Run(Session session, H_C2G_LoginAddressRequest request, H_G2C_LoginAddressResponse response, Action reply)
    {   
        //RouteComponent
        //AddressableRouteComponent
        Log.Debug(request.Message);
        //session.AddComponent<AddressableRouteComponent>().SetAddressableId();
        //1. 发送给Map 服务器
         var gloginAddressResponse=(I_M2G_LoginAddressResponse)await MessageHelper.CallInnerRoute(session.Scene,68920803328 
            , new I_G2M_LoginAddressRequest()
        {
                
        });
         if (gloginAddressResponse.ErrorCode!=0)
         {
             Log.Debug($"I_M2G_LoginAddressResponse error 注册到Map 错误{gloginAddressResponse.ErrorCode}");
             return;
         }
         
        
         //2.挂载AddressableRouteComponent组件 设置AddressableId ID
         session.AddComponent<AddressableRouteComponent>().SetAddressableId(gloginAddressResponse.AddressableId);
         Log.Debug("注册到Map服务器成功！！");
         
         
         //地图或者服务切换时
         //锁定AddressableId  防止修改发生变化
         //await AddressableHelper.LockAddressable(session.Scene, gloginAddressResponse.AddressableId);
         //解锁对应的Id
       //  await AddressableHelper.UnLockAddressable(session.Scene, gloginAddressResponse.AddressableId, 1024, "I_M2G_LoginAddressResponse");
    }
}