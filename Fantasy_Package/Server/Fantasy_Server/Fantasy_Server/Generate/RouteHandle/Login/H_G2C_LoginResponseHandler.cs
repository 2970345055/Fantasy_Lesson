﻿

using Fantasy;
using Fantasy_Server.Generate.RouteHandle;
using Fantasy.Core.Network;

<<<<<<< HEAD
public class H_G2C_LoginResponseHandler:RouteRPC<Scene,I_G2C_MessageRequest,I_G2C_MessageResponse>
{
    protected override async FTask Run(Scene entity, I_G2C_MessageRequest request, I_G2C_MessageResponse response, Action reply)
    {
        Log.Debug($"{request.Name}");
        var chatUnit = Entity.Create<ChatUnit>(entity);
        
=======
public class H_G2C_LoginResponseHandler:MessageRPC<I_G2C_MessageRequest,I_G2C_MessageResponse>
{
    protected override async FTask Run(Session session, I_G2C_MessageRequest request, I_G2C_MessageResponse response, Action reply)
    {
        var chatUnit = Entity.Create<ChatUnit>(session.Scene);

>>>>>>> main
        chatUnit.ChatName = request.Name;
        chatUnit.ChatId = request.GateRouteId;
        
        response.ChatRouteId = chatUnit.RuntimeId;
        await FTask.CompletedTask;
    }
}