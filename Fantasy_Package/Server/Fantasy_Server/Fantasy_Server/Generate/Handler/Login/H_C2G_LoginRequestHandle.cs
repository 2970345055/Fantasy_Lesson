

using Fantasy;
using Fantasy_Server.Generate.RouteHandle;
using Fantasy.Core.Network;

public class H_C2G_LoginRequestHandle:MessageRPC<H_C2G_LoginRequest,H_G2C_LoginResponse>
{
    protected override async FTask Run(Session session, H_C2G_LoginRequest request, H_G2C_LoginResponse response, Action reply)
    {
        if (request.Password!="123456")
        {
            return;
        }
        Log.Debug($"用户明：{request.UserName}登录成功！！！");

        var g2CMessageResponse =(I_G2C_MessageResponse)await MessageHelper.CallInnerRoute(session.Scene, 86167781376, new I_G2C_MessageRequest()
        {  
            GateRouteId = session.RuntimeId,
            Name = request.UserName
        });
        response.Text = "登录成功";
      
        var routeComponent=session.AddComponent<RouteComponent>();
        //绑定ID 
        routeComponent.AddAddress((int)RouteType.ChatRoute,g2CMessageResponse.ChatRouteId);      

    }
}