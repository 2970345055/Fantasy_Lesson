#if FANTASY_NET
namespace Fantasy;

/// <summary>
/// ����������Ϣ���ࡣ
/// </summary>
public class MachineConfigInfo
{
    /// <summary>
    /// ��ȡ�����û�����Ψһ��ʶ��
    /// </summary>
    public uint Id;
    /// <summary>
    /// ��ȡ�������ⲿIP��ַ��
    /// </summary>
    public string OuterIP;
    /// <summary>
    /// ��ȡ�������ⲿ��IP��ַ��
    /// </summary>
    public string OuterBindIP;
    /// <summary>
    /// ��ȡ�������ڲ���IP��ַ��
    /// </summary>
    public string InnerBindIP;
    /// <summary>
    /// ��ȡ�����ù���˿ڡ�
    /// </summary>
    public int ManagementPort;
}
#endif
