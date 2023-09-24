using System.Collections;
using System.Collections.Generic;
using Fantasy;
using UnityEngine;

public class Lesson19 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // TimerScheduler任务调度器// TimerScheduler是一个时间任务调度器、通过TimerScheduler可以开启定时、循环、的任多// 如:1分钟后指定某些任务、又或每隔多久执行一个任务等
        // TimerScheduler的核心包括
        // TimerScheduler.Instance.Core // 使用系统时间创建的计时器核心。
        // TimerScheduler.Instance.Unity // 使用 Unity 时间创建的计时器核心。支持Unity的时间缩放


        // TimerScheduler的API
        // WaitAsync(1000) // 等待一定时间后再执行下面的逻辑、这个时间是相对时间
        // WaitTillAsync(1000)// 等待一定时间后再执行下面的逻辑、这个时间是绝对时间
        
        // 0nceTimer(1000,委托) // 创建一个只执行一次的时间任务
        // 0nceTimer(1000,事件类型) // 创建一个只执行一次的时间任务
        // 0nceTillTimer 同上、一个是相对时间一个是绝对时间
        // RepeatedTimer(1000,委托) // 创建一个重复执行的时间任务,1000是重复间隔的时间
        // RepeatedTimer(1000,事件类型) // 创建一个重复执行的时间任务,1000是重复间隔的时间
        // NewFrameTimer(委托)// 创建一个在下一帧执行的时间任务。


        // Remove(时间任务Id)// 移除指定时间任务Id的时间任务
        // 0nceTime 0nceTillTimer RepeatedTimer创建成功后会返回-个时间任务Id、可以通过Remove传入这个Id来移除时间任务
    }

    private   async FTask Wait()
    {   
        //等待1s
        await TimerScheduler.Instance.Core.WaitAsync(1000);
        
        await TimerScheduler.Instance.Unity.WaitTillAsync(1000);
        
        //返回一个时间ID
        var timeId= TimerScheduler.Instance.Core.OnceTimer(5000, () => { Debug.Log("5s 后执行的委托"); });
        
        //间隔多久执行
        var repeatedTimerId = TimerScheduler.Instance.Core.RepeatedTimer(6000, () => { Debug.Log("每隔6s执行一次的方法");});
        //
        TimerScheduler.Instance.Core.RepeatedTimer(3000,new Ages.TimeEnd());
        TimerScheduler.Instance.Core.RepeatedTimer(3000,new Ages().Speed);
        //删除任务代表着取消任务
        TimerScheduler.Instance.Core.Remove(repeatedTimerId);
    }
}

public class Ages
{
    public struct  TimeEnd
    {
        
    }

    public void Speed()
    {
        Debug.Log("122321");
    }
}

public class OnTime:EventSystem<Ages.TimeEnd>
{
    public override void Handler(Ages.TimeEnd self)
    {
        throw new System.NotImplementedException();
    }
}

public class OnTime2:EventSystem<Ages>
{

    public override void Handler(Ages self)
    {
        throw new System.NotImplementedException();
    }
}