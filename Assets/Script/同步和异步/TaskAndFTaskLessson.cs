using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fantasy;
using UnityEngine;

public class TaskAndFTaskLessson : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   
        Debug.Log($"主线程:{Thread.CurrentThread.ManagedThreadId}开始");
        ShowFTask();
        ShowFTask3();
       // ShowFTask2();
        Debug.Log($"主线程{Thread.CurrentThread.ManagedThreadId}结束");
    }
    
    //Task  await/async
    public async FTask ShowFTask3()
    {   Debug.Log("CompletedTask Start");
        await FTask.CompletedTask;
        Debug.Log("CompletedTask End");
    }

    public async FTask ShowFTask()
    {    
       
        /*await Task.Delay(3000);
        Debug.Log($"第一次异步结束{Thread.CurrentThread.ManagedThreadId}");*/
        await FTask.Run(async() =>
        {
            Task.Delay(3000);
            Debug.Log($"第一次异步结束{Thread.CurrentThread.ManagedThreadId}");
        });
        /*await Task.Run(async () =>
        {
            Task.Delay(10000);
            Debug.Log($"第二次异步结束{Thread.CurrentThread.ManagedThreadId}");
        });*/
        // ShowTask2();
    }
    public async FTask ShowFTask2()
    {    
       
        /*await Task.Delay(3000);
        Debug.Log($"第一次异步结束{Thread.CurrentThread.ManagedThreadId}");*/
        await FTask.Run(async() =>
        {
            Task.Delay(3000);
            Debug.Log($"第一次异步结束{Thread.CurrentThread.ManagedThreadId}");
        });
        /*await Task.Run(async () =>
        {
            Task.Delay(10000);
            Debug.Log($"第二次异步结束{Thread.CurrentThread.ManagedThreadId}");
        });*/
        // ShowTask2();
    }

    public async Task ShowTask()
    {    
       
        /*await Task.Delay(3000);
        Debug.Log($"第一次异步结束{Thread.CurrentThread.ManagedThreadId}");*/
        await Task.Run(async() =>
        {
            Task.Delay(3000);
            Debug.Log($"第一次异步结束{Thread.CurrentThread.ManagedThreadId}");
        });
        /*await Task.Run(async () =>
        {
            Task.Delay(10000);
            Debug.Log($"第二次异步结束{Thread.CurrentThread.ManagedThreadId}");
        });*/
       // ShowTask2();
        
        Debug.Log($"结束异步{Thread.CurrentThread.ManagedThreadId}");

       
    }

    public async Task ShowTask2()
    {  
        /*await Task.Delay(3000);
        Debug.Log($"第二次异步结束{Thread.CurrentThread.ManagedThreadId}");*/
        await Task.Run(async() =>
        {
            Task.Delay(3000);
            Debug.Log($"第二次异步结束{Thread.CurrentThread.ManagedThreadId}");
        });
    }
}
