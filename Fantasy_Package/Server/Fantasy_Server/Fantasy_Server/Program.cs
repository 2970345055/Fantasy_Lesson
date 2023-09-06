// See https://aka.ms/new-console-template for more information

using Fantasy;
using Fantasy.Core;
using Fantasy.Core.DataBase;
using Fantasy.Helper;

Console.WriteLine("Hello, World!");
//初始化框架
Fantasy.Application.Initialize();

//I_G2M_LoginAddressRequest a=new I_G2M_LoginAddressRequest();

//装载程序集
AssemblyManager.Load(1,typeof(Program).Assembly);


#region 绑定配置文件


ConfigTableManage.ServerConfig+= ServerId =>
{   
    //使用前需要确认  配置表二进制文件存放目录（TODO可以直接查看）
    var serverConfig = ServerConfigData.Instance.Get(ServerId);
    return new ServerConfigInfo()
    {
        Id = serverConfig.Id,
        MachineId = serverConfig.MachineId,
        InnerPort = serverConfig.InnerPort
    };
};
  ConfigTableManage.MachineConfig = machineId =>
        {
            if (!MachineConfigData.Instance.TryGet(machineId, out var machineConfig))
            {
                return null;
            }
        
            return new MachineConfigInfo()
            {
                Id = machineConfig.Id,
                OuterIP = machineConfig.OuterIP,
                OuterBindIP = machineConfig.OuterBindIP,
                InnerBindIP = machineConfig.InnerBindIP,
                ManagementPort = machineConfig.ManagementPort
            };
        };
        ConfigTableManage.WorldConfigInfo = worldId =>
        {
            if (!WorldConfigData.Instance.TryGet(worldId, out var worldConfig))
            {
                return null;
            }
        
            return new WorldConfigInfo()
            {
                Id = worldConfig.Id,
                WorldName = worldConfig.WorldName,
                DbConnection = worldConfig.DbConnection,
                DbName = worldConfig.DbName,
                DbType = worldConfig.DbType
            };
        };
        ConfigTableManage.SceneConfig = sceneId =>
        {
            if (!SceneConfigData.Instance.TryGet(sceneId, out var sceneConfig))
            {
                return null;
            }
        
            return new SceneConfigInfo()
            {
                Id = sceneConfig.Id,
                SceneType = SceneType.SceneTypeDic[sceneConfig.SceneType],
                SceneSubType = SceneSubType.SceneSubTypeDic[sceneConfig.SceneSubType],
                SceneTypeStr = sceneConfig.SceneType,
                SceneSubTypeStr = sceneConfig.SceneSubType,
                NetworkProtocol = sceneConfig.NetworkProtocol,
                ServerConfigId = sceneConfig.ServerConfigId,
                WorldId = sceneConfig.WorldId,
                OuterPort = sceneConfig.OuterPort
            };
        };
        ConfigTableManage.AllServerConfig = () =>
        {
            var list = new List<ServerConfigInfo>();
        
            foreach (var serverConfig in ServerConfigData.Instance.List)
            {
                list.Add(new ServerConfigInfo()
                {
                    Id = serverConfig.Id,
                    InnerPort = serverConfig.InnerPort,
                    MachineId = serverConfig.MachineId
                });
            }
        
            return list;
        };
        ConfigTableManage.AllMachineConfig = () =>
        {
            var list = new List<MachineConfigInfo>();
        
            foreach (var machineConfig in MachineConfigData.Instance.List)
            {
                list.Add(new MachineConfigInfo()
                {
                    Id = machineConfig.Id,
                    OuterIP = machineConfig.OuterIP,
                    OuterBindIP = machineConfig.OuterBindIP,
                    InnerBindIP = machineConfig.InnerBindIP,
                    ManagementPort = machineConfig.ManagementPort
                });
            }
        
            return list;
        };
        ConfigTableManage.AllSceneConfig = () =>
        {
            var list = new List<SceneConfigInfo>();
        
            foreach (var sceneConfig in SceneConfigData.Instance.List)
            {
                list.Add(new SceneConfigInfo()
                {
                    Id = sceneConfig.Id,
                    EntityId = sceneConfig.EntityId,
                    SceneType = SceneType.SceneTypeDic[sceneConfig.SceneType],
                    SceneSubType = SceneSubType.SceneSubTypeDic[sceneConfig.SceneSubType],
                    SceneTypeStr = sceneConfig.SceneType,
                    SceneSubTypeStr = sceneConfig.SceneSubType,
                    NetworkProtocol = sceneConfig.NetworkProtocol,
                    ServerConfigId = sceneConfig.ServerConfigId,
                    WorldId = sceneConfig.WorldId,
                    OuterPort = sceneConfig.OuterPort
                });
            }
        
            return list;
        };
    

#endregion

// 启动框架
// 启动框架会加载Demo下Config/Excel/Server里四个文件配置
// 因为上面ConfigTableHelper.Bind已经绑定好了、所以框架可以直接读取这4个配置文件进行启动
Application.Start().Coroutine();


for (;;)
{   
    Thread.Sleep(1);//让出CPU 时间片给其他进程使用 （什么是 CPU时间片 ）
    ThreadSynchronizationContext.Main.Update();// 执行其他线程同步到主线程的逻辑 
    SingletonSystem.Update();// 执行所有继承IUpdateSingleton的类的Updata方法
}