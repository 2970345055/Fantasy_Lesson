#if FANTASY_NET
using Fantasy.Core.DataBase;
using Fantasy.DataStructure;
using Fantasy.Helper;
// ReSharper disable SuspiciousTypeConversion.Global

namespace Fantasy.Hotfix;

/// <summary>
/// �������Ϲ������࣬�̳��� <see cref="Singleton{T}"/>��
/// </summary>
public class SingleCollection : Singleton<SingleCollection>
{
    private readonly OneToManyHashSet<Type, string> _collection = new OneToManyHashSet<Type, string>();
    private readonly OneToManyList<int, SingleCollectionInfo> _assemblyCollections = new OneToManyList<int, SingleCollectionInfo>();

    /// <summary>
    /// ��ʾ�������ϵ���Ϣ�ࡣ
    /// </summary>
    private sealed class SingleCollectionInfo
    {
        public readonly Type RootType;
        public readonly string CollectionName;

        public SingleCollectionInfo(Type rootType, string collectionName)
        {
            RootType = rootType;
            CollectionName = collectionName;
        }
    }

    /// <summary>
    /// �ڳ��򼯼���ʱִ�еķ�����
    /// </summary>
    /// <param name="assemblyName">�������ơ�</param>
    protected override void OnLoad(int assemblyName)
    {
        foreach (var type in AssemblyManager.ForEach(assemblyName, typeof(ISupportedSingleCollection)))
        {
            var customAttributes = type.GetCustomAttributes(typeof(SingleCollectionAttribute), false);

            if (customAttributes.Length == 0)
            {
                Log.Error($"type {type.FullName} Implemented the interface of ISingleCollection, requiring the implementation of SingleCollectionAttribute");
                continue;
            }

            var singleCollectionAttribute = (SingleCollectionAttribute)customAttributes[0];
            var rootType = singleCollectionAttribute.RootType;
            var collectionName = singleCollectionAttribute.CollectionName;
            _collection.Add(rootType, collectionName);
            _assemblyCollections.Add(assemblyName, new SingleCollectionInfo(rootType, collectionName));
        }
    }

    /// <summary>
    /// �ڳ���ж��ʱִ�еķ�����
    /// </summary>
    /// <param name="assemblyName">�������ơ�</param>
    protected override void OnUnLoad(int assemblyName)
    {
        if (!_assemblyCollections.TryGetValue(assemblyName, out var types))
        {
            return;
        }
        
        foreach (var singleCollectionInfo in types)
        {
            _collection.RemoveValue(singleCollectionInfo.RootType, singleCollectionInfo.CollectionName);
        }
            
        _assemblyCollections.RemoveByKey(assemblyName);
    }

    /// <summary>
    /// �첽��ȡʵ��ļ������ݡ�
    /// </summary>
    /// <param name="entity">ʵ�����</param>
    /// <returns>��ʾ�첽����������</returns>
    public async FTask GetCollections(Entity entity)
    {
        if (entity is not ISingleCollectionRoot)
        {
            return;
        }
        
        if (!_collection.TryGetValue(entity.GetType(), out var collections))
        {
            return;
        }

        var scene = entity.Scene;
        var worldDateBase = scene.World.DateBase;

        using (await IDateBase.DataBaseLock.Lock(entity.Id))
        {
            foreach (var collectionName in collections)
            {
                var singleCollection = await worldDateBase.QueryNotLock<Entity>(entity.Id, collectionName);
                singleCollection.Deserialize(scene);
                entity.AddComponent(singleCollection);
            }
        }
    }

    /// <summary>
    /// �첽����ʵ��ļ������ݡ�
    /// </summary>
    /// <param name="entity">ʵ�����</param>
    /// <returns>��ʾ�첽����������</returns>
    public async FTask SaveCollections(Entity entity)
    {
        if (entity is not ISingleCollectionRoot)
        {
            return;
        }

        using var collections = ListPool<Entity>.Create();
        
        foreach (var treeEntity in entity.ForEachSingleCollection)
        {
            if (treeEntity is not ISupportedSingleCollection)
            {
                continue;
            }
            
            collections.Add(treeEntity);
        }

        await entity.Scene.World.DateBase.Save(entity.Id, collections);
    }
}
#endif