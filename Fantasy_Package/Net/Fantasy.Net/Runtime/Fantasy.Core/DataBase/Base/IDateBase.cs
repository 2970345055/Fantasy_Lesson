#if FANTASY_NET
using System.Linq.Expressions;
#pragma warning disable CS8625

namespace Fantasy.Core.DataBase;

/// <summary>
/// ��ʾ����ִ�и������ݿ���������ݿ�ӿڡ�
/// </summary>
public interface IDateBase
{
    /// <summary>
    /// �������ݿ���ʲ����Ե����������͡�
    /// </summary>
    public static readonly CoroutineLockQueueType DataBaseLock = new CoroutineLockQueueType("DataBaseLock");

    /// <summary>
    /// ��ʼ�����ݿ����ӡ�
    /// </summary>
    IDateBase Initialize(string connectionString, string dbName);
    /// <summary>
    /// ��ָ���ļ����м������� <typeparamref name="T"/> ��ʵ��������
    /// </summary>
    FTask<long> Count<T>(string collection = null) where T : Entity;
    /// <summary>
    /// ��ָ���ļ����м����������ɸѡ���������� <typeparamref name="T"/> ��ʵ��������
    /// </summary>
    FTask<long> Count<T>(Expression<Func<T, bool>> filter, string collection = null) where T : Entity;
    /// <summary>
    /// ���ָ���������Ƿ�������� <typeparamref name="T"/> ��ʵ�塣
    /// </summary>
    FTask<bool> Exist<T>(string collection = null) where T : Entity;
    /// <summary>
    /// ���ָ���������Ƿ�����������ɸѡ���������� <typeparamref name="T"/> ��ʵ�塣
    /// </summary>
    FTask<bool> Exist<T>(Expression<Func<T, bool>> filter, string collection = null) where T : Entity;
    /// <summary>
    /// ��ָ�������м���ָ�� ID ������ <typeparamref name="T"/> ��ʵ�壬��������
    /// </summary>
    FTask<T> QueryNotLock<T>(long id, string collection = null) where T : Entity;
    /// <summary>
    /// ��ָ�������м���ָ�� ID ������ <typeparamref name="T"/> ��ʵ�塣
    /// </summary>
    FTask<T> Query<T>(long id, string collection = null) where T : Entity;
    /// <summary>
    /// ��ҳ��ѯ�������ɸѡ���������� <typeparamref name="T"/> ��ʵ�����������ڡ�
    /// </summary>
    FTask<(int count, List<T> dates)> QueryCountAndDatesByPage<T>(Expression<Func<T, bool>> filter, int pageIndex, int pageSize, string collection = null) where T : Entity;
    /// <summary>
    /// ��ҳ��ѯ�������ɸѡ���������� <typeparamref name="T"/> ��ʵ�����������ڡ�
    /// </summary>
    FTask<(int count, List<T> dates)> QueryCountAndDatesByPage<T>(Expression<Func<T, bool>> filter, int pageIndex, int pageSize, string[] cols, string collection = null) where T : Entity;
    /// <summary>
    /// ��ҳ��ѯָ���������������ɸѡ���������� <typeparamref name="T"/> ��ʵ���б�
    /// </summary>
    FTask<List<T>> QueryByPage<T>(Expression<Func<T, bool>> filter, int pageIndex, int pageSize, string collection = null) where T : Entity;
    /// <summary>
    /// ��ҳ��ѯָ���������������ɸѡ���������� <typeparamref name="T"/> ��ʵ���б�������ָ���е����ݡ�
    /// </summary>
    FTask<List<T>> QueryByPage<T>(Expression<Func<T, bool>> filter, int pageIndex, int pageSize, string[] cols, string collection = null) where T : Entity;
    /// <summary>
    /// ��ָ�������а�ҳ��ѯ�������ɸѡ���������� <typeparamref name="T"/> ��ʵ���б���ָ���ֶ�����
    /// </summary>
    FTask<List<T>> QueryByPageOrderBy<T>(Expression<Func<T, bool>> filter, int pageIndex, int pageSize, Expression<Func<T, object>> orderByExpression, bool isAsc = true, string collection = null) where T : Entity;
    /// <summary>
    /// �����������ɸѡ���������� <typeparamref name="T"/> �ĵ�һ��ʵ�壬��ָ�������С�
    /// </summary>
    FTask<T?> First<T>(Expression<Func<T, bool>> filter, string collection = null) where T : Entity;
    /// <summary>
    /// ��ѯָ��������������� JSON ��ѯ�ַ��������� <typeparamref name="T"/> �ĵ�һ��ʵ�壬������ָ���е����ݡ�
    /// </summary>
    FTask<T> First<T>(string json, string[] cols, string collection = null) where T : Entity;
    /// <summary>
    /// ��ָ�������а�ҳ��ѯ�������ɸѡ���������� <typeparamref name="T"/> ��ʵ���б���ָ���ֶ�����
    /// </summary>
    FTask<List<T>> QueryOrderBy<T>(Expression<Func<T, bool>> filter, Expression<Func<T, object>> orderByExpression, bool isAsc = true, string collection = null) where T : Entity;
    /// <summary>
    /// ��ָ�������а�ҳ��ѯ�������ɸѡ���������� <typeparamref name="T"/> ��ʵ���б�
    /// </summary>
    FTask<List<T>> Query<T>(Expression<Func<T, bool>> filter, string collection = null) where T : Entity;
    /// <summary>
    /// ��ѯָ�� ID �Ķ�����ϣ�������洢�ڸ�����ʵ���б��С�
    /// </summary>
    FTask Query(long id, List<string> collectionNames, List<Entity> result);
    /// <summary>
    /// ���ݸ����� JSON ��ѯ�ַ�����ѯָ�������е����� <typeparamref name="T"/> ʵ���б�
    /// </summary>
    FTask<List<T>> QueryJson<T>(string json, string collection = null) where T : Entity;
    /// <summary>
    /// ���ݸ����� JSON ��ѯ�ַ�����ѯָ�������е����� <typeparamref name="T"/> ʵ���б�������ָ���е����ݡ�
    /// </summary>
    FTask<List<T>> QueryJson<T>(string json, string[] cols, string collection = null) where T : Entity;
    /// <summary>
    /// ���ݸ����� JSON ��ѯ�ַ�����ѯָ�������е����� <typeparamref name="T"/> ʵ���б�ͨ��ָ�������� ID ���б�ʶ��
    /// </summary>
    FTask<List<T>> QueryJson<T>(long taskId, string json, string collection = null) where T : Entity;
    /// <summary>
    /// ��ѯָ���������������ɸѡ���������� <typeparamref name="T"/> ʵ���б�������ָ���е����ݡ�
    /// </summary>
    FTask<List<T>> Query<T>(Expression<Func<T, bool>> filter, string[] cols, string collection = null) where T : class;
    /// <summary>
    /// �������� <typeparamref name="T"/> ʵ�嵽ָ�������У�������ϲ����ڽ��Զ�������
    /// </summary>
    FTask Save<T>(T entity, string collection = null) where T : Entity, new();
    /// <summary>
    /// ����һ��ʵ�嵽���ݿ��У�����ʵ���б�� ID �������ֺʹ洢��
    /// </summary>
    FTask Save(long id, List<Entity> entities);
    /// <summary>
    /// ͨ������Ự������ <typeparamref name="T"/> ʵ�屣�浽ָ�������У�������ϲ����ڽ��Զ�������
    /// </summary>
    FTask Save<T>(object transactionSession, T entity, string collection = null) where T : Entity;
    /// <summary>
    /// ��ָ�������в���һ������ <typeparamref name="T"/> ʵ�壬������ϲ����ڽ��Զ�������
    /// </summary>
    FTask Insert<T>(T entity, string collection = null) where T : Entity, new();
    /// <summary>
    /// ��������һ������ <typeparamref name="T"/> ʵ�嵽ָ�������У�������ϲ����ڽ��Զ�������
    /// </summary>
    FTask InsertBatch<T>(IEnumerable<T> list, string collection = null) where T : Entity, new();
    /// <summary>
    /// ͨ������Ự����������һ������ <typeparamref name="T"/> ʵ�嵽ָ�������У�������ϲ����ڽ��Զ�������
    /// </summary>
    FTask InsertBatch<T>(object transactionSession, IEnumerable<T> list, string collection = null) where T : Entity, new();
    /// <summary>
    /// ͨ������Ự������ָ���� ID �����ݿ���ɾ��ָ������ <typeparamref name="T"/> ʵ�塣
    /// </summary>
    FTask<long> Remove<T>(object transactionSession, long id, string collection = null) where T : Entity, new();
    /// <summary>
    /// ����ָ���� ID �����ݿ���ɾ��ָ������ <typeparamref name="T"/> ʵ�塣
    /// </summary>
    FTask<long> Remove<T>(long id, string collection = null) where T : Entity, new();
    /// <summary>
    /// ͨ������Ự�����ݸ�����ɸѡ������ ID �����ݿ���ɾ��ָ������ <typeparamref name="T"/> ʵ�塣
    /// </summary>
    FTask<long> Remove<T>(long id,object transactionSession, Expression<Func<T, bool>> filter, string collection = null) where T : Entity, new();
    /// <summary>
    /// ���ݸ�����ɸѡ������ ID �����ݿ���ɾ��ָ������ <typeparamref name="T"/> ʵ�塣
    /// </summary>
    FTask<long> Remove<T>(long id, Expression<Func<T, bool>> filter, string collection = null) where T : Entity, new();
    /// <summary>
    /// ���ݸ�����ɸѡ��������ָ������������ <typeparamref name="T"/> ʵ��ĳ�����Ե��ܺ͡�
    /// </summary>
    FTask<long> Sum<T>(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sumExpression, string collection = null) where T : Entity;
    /// <summary>
    /// ��ָ���ļ����д������������������ <typeparamref name="T"/> ʵ��Ĳ�ѯ���ܡ�
    /// </summary>
    FTask CreateIndex<T>(string collection, params object[] keys) where T : Entity;
    /// <summary>
    /// ��Ĭ�ϼ����д������������������ <typeparamref name="T"/> ʵ��Ĳ�ѯ���ܡ�
    /// </summary>
    FTask CreateIndex<T>(params object[] keys) where T : Entity;
    /// <summary>
    /// ����ָ������ <typeparamref name="T"/> �����ݿ⣬���ڴ洢ʵ�塣
    /// </summary>
    FTask CreateDB<T>() where T : Entity;
    /// <summary>
    /// ����ָ�����ʹ������ݿ⣬���ڴ洢ʵ�塣
    /// </summary>
    FTask CreateDB(Type type);
}
#endif