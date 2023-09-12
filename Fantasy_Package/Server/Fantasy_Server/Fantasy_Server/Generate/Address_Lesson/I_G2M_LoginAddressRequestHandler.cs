using Fantasy;
using Fantasy.Core.Network;

namespace Fantasy_Server.Generate.Address_Lesson;

public class I_G2M_LoginAddressRequestHandler:RouteRPC<Scene,I_G2M_LoginAddressRequest,I_M2G_LoginAddressResponse>
{
    protected override async FTask Run(Scene entity, I_G2M_LoginAddressRequest request, I_M2G_LoginAddressResponse response, Action reply)
    {
        //Map进程  一般都是玩家首次进入 消息会到这里，之后通过Address 协议绑定对应的Map 下的实体来进行直接通讯
        //一般情况下都是 通过数据库或者其他方式， 拿到这个玩家的数据
        //UnitManager.Units.TryGetValue();

        var unit=Entity.Create<Unit>(entity);
        unit.Name = "Fantasy Map 1";
        
        
        //添加AddressableMessageComponent 消息协议  并且注册 
        //该组件一定要挂载到 要注册Address 的实体下  这样才能和客户端通信（本质还是要经过Gate 中转）
        unit.AddComponent<AddressableMessageComponent>().Register();
        //entity.RuntimeId  会在更换服务器时修改， 所以使用Entity.Id  固定的ID
        response.AddressableId = unit.Id;

        await FTask.CompletedTask;
        
    }
}


public class UnitManager:Entity
{
    public static Dictionary<long, Unit> Units = new Dictionary<long, Unit>();
}

public class Unit : Entity
{
    public string Name;
    
}


