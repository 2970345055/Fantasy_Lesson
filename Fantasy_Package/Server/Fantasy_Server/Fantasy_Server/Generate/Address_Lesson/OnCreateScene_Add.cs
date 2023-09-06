using Fantasy;
using Fantasy.Core.Network;

namespace Fantasy_Server.Generate.Address_Lesson;

public class OnCreateScene_Add:AsyncEventSystem<OnCreateScene>
{
    public override async FTask Handler(OnCreateScene self)
    {
       var scene=self.Scene;
        //当服务器启动时 通过对应SceneType 为创建的Scene添加组件
       switch (scene.SceneType)
       {
           case SceneType.Map:
               scene.AddComponent<UnitManager>();
               break;
           case SceneType.Addressable:
               scene.AddComponent<AddressableManageComponent>();
               break;
       }
        
       await FTask.CompletedTask;
    }
}