using System.Collections;
using System.Collections.Generic;
using Fantasy;
using Fantasy.Helper;
using UnityEngine;

public class EventSysytmLesson : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       Fantasy.Entry.Initialize();
        
        AssemblyManager.Load(1,GetType().Assembly);
        EventSystem.Instance.Publish(new AgeEvent()
        {
            age = 10
        });
        
        EventSystem.Instance.PublishAsync(new AgeEvent()
        {
            age = 10
        });
        /*//使用实体挂载
        var entity = Entity.Create<AgeEntity>(null);
        entity.age = 10;
         EventSystem.Instance.Publish(entity);//同步
        EventSystem.Instance.PublishAsync(entity).Coroutine();//异步*/
        
       //onAgeChange.Handler();
    }
    
}

public class OnAgeChange : EventSystem<AgeEvent>
{
    public override void Handler(AgeEvent self)
    {
        Debug.Log($"Age{self.age}");
    }
}
public class OnAgeChangeAsync : AsyncEventSystem<AgeEvent>
{
    public override async FTask Handler(AgeEvent self)
    {
        await FTask.CompletedTask;
        Debug.Log($"Age{self.age}");
    }
}

public struct AgeEvent
{
    public int age;
}

public class AgeEntity:Entity
{
    public int age;
}
