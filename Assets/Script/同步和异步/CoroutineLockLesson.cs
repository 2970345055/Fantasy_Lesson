using System.Collections;
using System.Collections.Generic;
using Fantasy;
using Fantasy.Core;
using UnityEngine;
using UnityEngine.Timeline;

public class CoroutineLockLesson : MonoBehaviour
{
    // Start is called before the first frame update
    

    void Start()
    {
        Fantasy.Entry.Initialize();
        Show1();    
        Show2();
    }

    public string str = "";
    private readonly CoroutineLockQueueType _lockQueue = new CoroutineLockQueueType("LockShow");
    
    public async FTask Show1()
    {  
        Debug.Log("show1");
        using (await _lockQueue.Lock(str.GetHashCode(),"show",5000))
        {
            await FTask.Run(async () =>
            {  
                Debug.Log("show1修改");
                await TimerScheduler.Instance.Core.WaitAsync(1000);
                str = "a";
            });
            Debug.Log($"show1修改完成{str}");
        }    
    }
    
    public async FTask Show2()
    {   
        Debug.Log("show2");
        using (await _lockQueue.Lock(str.GetHashCode(),"show",5000))
        {
            await FTask.Run(async () =>
            {  
                Debug.Log("show2修改");
                //await TimerScheduler.Instance.Core.WaitAsync(1000);
                str = "b";
            });
        }  
        Debug.Log($"show2修改完成{str}");
    }
}
