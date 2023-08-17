#if FANTASY_NET
namespace Fantasy.Core.DataBase;

/// <summary>
/// ��ʾһ����Ϸ���硣
/// </summary>
public sealed class World
{
    /// <summary>
    /// ��ȡ��Ϸ�����Ψһ��ʶ��
    /// </summary>
    public uint Id { get; private init; }
    /// <summary>
    /// ��ȡ��Ϸ��������ݿ�ӿڡ�
    /// </summary>
    public IDateBase DateBase { get; private init; }
    /// <summary>
    /// ��ȡ��Ϸ�����������Ϣ��
    /// </summary>
    public WorldConfigInfo Config => ConfigTableManage.WorldConfigInfo(Id);
    /// <summary>
    /// ���ڴ洢�Ѵ�������Ϸ����ʵ��
    /// </summary>
    private static readonly Dictionary<uint, World> Worlds = new();

    /// <summary>
    /// ʹ��ָ����������Ϣ����һ����Ϸ����ʵ����
    /// </summary>
    /// <param name="worldConfigInfo">��Ϸ�����������Ϣ��</param>
    public World(WorldConfigInfo worldConfigInfo)
    {
        Id = worldConfigInfo.Id;
        var dbType = worldConfigInfo.DbType.ToLower();
        
        switch (dbType)
        {
            case "mongodb":
            {
                DateBase = new MongoDataBase();
                DateBase.Initialize(worldConfigInfo.DbConnection, worldConfigInfo.DbName);
                break;
            }
            default:
                throw new Exception("No supported database");
        }
    }

    /// <summary>
    /// ����һ��ָ��Ψһ��ʶ����Ϸ����ʵ����
    /// </summary>
    /// <param name="id">��Ϸ�����Ψһ��ʶ��</param>
    /// <returns>��Ϸ����ʵ����</returns>
    public static World Create(uint id)
    {
        if (Worlds.TryGetValue(id, out var world))
        {
            return world;
        }

        var worldConfigInfo = ConfigTableManage.WorldConfigInfo(id);
        world = new World(worldConfigInfo);
        Worlds.Add(id, world);
        return world;
    }
}
#endif