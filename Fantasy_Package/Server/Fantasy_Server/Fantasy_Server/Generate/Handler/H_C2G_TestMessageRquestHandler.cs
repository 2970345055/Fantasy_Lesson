

/*
using Fantasy;
using Fantasy.Core.Network;
public class H_C2G_TestMessageRquestHandler:MessageRPC<H_C2G_TestMessageRquest,H_C2G_TestMessageResponse>
{
    protected override async FTask Run(Session session, H_C2G_TestMessageRquest request, H_C2G_TestMessageResponse response, Action reply)
    {
        Log.Debug($"客户端请求{request.Message}");

        var g2cresponse=(I_G2C_MessageResponse)await MessageHelper.CallInnerRoute(session.Scene, 86167781376, 
            new I_G2C_MessageRequest()
        {   
                Name = "0",
             GateRouteId = session.RuntimeId
             
        });
        response.Message = g2cresponse.ChatRouteId.ToString();
        Log.Debug($"收到Chat的回复{g2cresponse.ChatRouteId}");
            
            //添加一个RouteComponent 组件
           // var routeComponent= session.AddComponent<RouteComponent>();
            //绑定相对应的 ChatRouteId 实体ID; 这样就可以通过ID 找到对应的chat 对象
           // routeComponent.AddAddress((int)RouteType.ChatRoute,g2cresponse.ChatRouteId);
    }
}*/