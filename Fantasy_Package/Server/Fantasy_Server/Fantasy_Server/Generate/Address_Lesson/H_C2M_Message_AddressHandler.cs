

using Fantasy;
using Fantasy_Server.Generate.Address_Lesson;
using Fantasy.Core.Network;

public class H_C2M_Message_AddressHandler:Addressable<Unit,H_C2M_Message>
{
    protected override async FTask Run(Unit unit, H_C2M_Message message)
    {
        Log.Debug($"unit 对象是{unit.Name}发来消息： {message.Message}");
    }
}