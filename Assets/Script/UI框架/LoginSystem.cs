using Fantasy;
using UnityEngine;

namespace Script.UI框架
{   //挂载Login 当组件被创建时调用
    public class LoginSystem:AwakeSystem<Login>
    {   
        protected override void Awake(Login self)
        { 
            self.Awake();
            Debug.Log("Login UI 已经初始化");
           // LoginEvent v = new LoginEvent();
            //v.Awake(self);
           
        }
    }
    
    public static class LoginEvent
    {
        public static  void Awake(this Login self)
        {   
            self.LoginButton.onClick.RemoveAllListeners();
            self.LoginButton.onClick.AddListener(() =>
            {
                Debug.Log($"UserName{self.UserName.text}");
                
                Debug.Log($"PassWord{self.PassWord.text}");
            });
        }
    }
}