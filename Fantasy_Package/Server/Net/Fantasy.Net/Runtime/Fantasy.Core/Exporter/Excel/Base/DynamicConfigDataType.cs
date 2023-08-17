#if FANTASY_NET
using System.Reflection;
using System.Text;

namespace Fantasy.Core;

/// <summary>
/// ��̬�������������࣬���ڴ洢��̬�������ݵ������Ϣ��
/// </summary>
public class DynamicConfigDataType
{
    /// <summary>
    /// �������ݶ��󣬼̳��� AProto ���ࡣ
    /// </summary>
    public AProto ConfigData;

    /// <summary>
    /// �����������͡�
    /// </summary>
    public Type ConfigDataType;

    /// <summary>
    /// �������͡�
    /// </summary>
    public Type ConfigType;

    /// <summary>
    /// ���䷽����Ϣ�����ڵ����ض�������
    /// </summary>
    public MethodInfo Method;

    /// <summary>
    /// �������ݶ���ʵ����
    /// </summary>
    public object Obj;

    /// <summary>
    /// �������� JSON ��ʽ���ݵ��ַ�����������
    /// </summary>
    public StringBuilder Json = new StringBuilder();
}
#endif
