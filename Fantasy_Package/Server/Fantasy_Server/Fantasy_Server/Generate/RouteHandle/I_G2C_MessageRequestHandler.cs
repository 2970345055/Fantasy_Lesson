<<<<<<< HEAD
﻿using Fantasy;
using Fantasy.Core.Network;

namespace Fantasy_Server.Generate.RouteHandle;

/*
public class I_G2C_MessageRequestHandler:RouteRPC<Scene,I_G2C_MessageRequest,I_G2C_MessageResponse>
{
    protected override async FTask Run(Scene entity, I_G2C_MessageRequest request, I_G2C_MessageResponse response, Action reply)
    {
        Log.Debug($"接收到Gate服务器发送来的请求{request.GateRouteId}");


        var chatUnit=Entity.Create<ChatUnit>(entity);
        chatUnit.ChatName = request.Name;
        chatUnit.ChatId = request.GateRouteId;
        
        response.ChatRouteId = chatUnit.RuntimeId;
        
        await FTask.CompletedTask;
    }
}
*/





public class ChatUnit : Entity
{
    public string ChatName;
    public long ChatId;
=======
﻿namespace Fantasy_Server.Generate.RouteHandle;

public class I_G2C_MessageRequestHandler
{
    
>>>>>>> main
}