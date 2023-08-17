#if FANTASY_NET
namespace Fantasy.Core.DataBase;

/// <summary>
/// ��ʾ��Ϸ�����������Ϣ��
/// </summary>
public class WorldConfigInfo
{
    /// <summary>
    /// ��ȡ��������Ϸ�����Ψһ��ʶ��
    /// </summary>
    public uint Id { get; set; }

    /// <summary>
    /// ��ȡ��������Ϸ��������ơ�
    /// </summary>
    public string WorldName { get; set; }

    /// <summary>
    /// ��ȡ��������Ϸ��������ݿ������ַ�����
    /// </summary>
    public string DbConnection { get; set; }

    /// <summary>
    /// ��ȡ��������Ϸ��������ݿ����ơ�
    /// </summary>
    public string DbName { get; set; }

    /// <summary>
    /// ��ȡ��������Ϸ��������ݿ����͡�
    /// </summary>
    public string DbType { get; set; }
}
#endif