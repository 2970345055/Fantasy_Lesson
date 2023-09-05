

using Fantasy;
using Fantasy_Server.Generate.RouteHandle;
using Fantasy.Core.Network;

public class I_G2C_MessageRequestLoginHandler:Route<ChatUnit,H_C2Chat_TestMessage>
{
    protected override async FTask Run(ChatUnit entity, H_C2Chat_TestMessage message)
    {
        Log.Debug($"{message.Message}");

        await FTask.CompletedTask;
    }
}