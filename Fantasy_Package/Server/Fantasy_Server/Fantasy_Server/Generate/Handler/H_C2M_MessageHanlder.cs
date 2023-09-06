
using Fantasy;
using Fantasy.Core.Network;
public class H_C2M_MessageHanlder:Message<H_C2G_Message>
{
    protected override async FTask Run(Session session, H_C2G_Message message)
    {
        Log.Debug($"{message.Message}");
        
        session.Send(new H_G2C_TestMessage()
        {
            Message = "你好客户端"
        });
        await FTask.CompletedTask;
    }
}