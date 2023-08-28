using Fantasy;

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
}
