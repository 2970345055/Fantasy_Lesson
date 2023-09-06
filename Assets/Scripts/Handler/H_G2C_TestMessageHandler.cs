

using Fantasy;
using Fantasy.Core.Network;

public class H_G2C_TestMessageHandler :Message<H_G2C_TestMessage>
{
    protected override async FTask Run(Session session, H_G2C_TestMessage message)
    {
        Log.Debug($"服务端发来消息{message.Message}");
        
        await FTask.CompletedTask;
        
    }
}
