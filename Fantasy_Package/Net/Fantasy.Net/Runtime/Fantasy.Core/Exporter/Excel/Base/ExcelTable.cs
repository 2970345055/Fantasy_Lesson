#if FANTASY_NET
namespace Fantasy.Core;

/// <summary>
/// Excel����࣬���ڴ洢�������ƺ�����Ϣ��
/// </summary>
public sealed class ExcelTable
{
    /// <summary>
    /// �������ơ�
    /// </summary>
    public readonly string Name;
    /// <summary>
    /// �ͻ�������Ϣ��ʹ�������ֵ�洢�������������б�
    /// </summary>
    public readonly SortedDictionary<string, List<int>> ClientColInfos = new();
    /// <summary>
    /// ������������Ϣ��ʹ�������ֵ�洢�������������б�
    /// </summary>
    public readonly SortedDictionary<string, List<int>> ServerColInfos = new();

    /// <summary>
    /// ���캯������ʼ��Excel���������ñ�����ơ�
    /// </summary>
    /// <param name="name">������ơ�</param>
    public ExcelTable(string name)
    {
        Name = name;
    }
}
#endif
