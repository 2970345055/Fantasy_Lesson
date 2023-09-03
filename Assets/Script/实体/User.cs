using Fantasy;
using UnityEngine;

namespace Script.实体
{
    public class User : Entity,INotSupportedPool
    {

        public string name;

        public int age;
        
        public override void Dispose()
        {
            name = null;
            age = 0;
            base.Dispose();
        }
    }

    public static class UserSystem
    {
        public static void Add(this User self)
        {
            //具体逻辑
        }
    }

    public class UserAwakeSystem : AwakeSystem<User>
    {
        protected override void Awake(User self)
        { 
            Debug.Log("2131312");
            Debug.Log(self.name);
        }
    }

    public class UserDestroySysytem : DestroySystem<User>
    {
        protected override void Destroy(User self)
        {
            Debug.Log(self.name);
        }
    }
    
    public class UserUpdate:UpdateSystem<User>
    {
        protected override void Update(User self)
        {
            Debug.Log(self.age);
        }
    }
    public class UserDeserialize:DeserializeSystem<User>
    {
        protected override void Deserialize(User self)
        {
            Debug.Log(self.age);
            Debug.Log(self.name);
        }
    }
}
