#if FANTASY_NET
namespace Fantasy.Core;

/// <summary>
/// �Զ��嵼���ӿ�
/// </summary>
public interface ICustomExport
{
    /// <summary>
    /// ִ�е�������
    /// </summary>
    void Run();
}

/// <summary>
/// �����Զ��嵼������
/// </summary>
public abstract class ACustomExport : ICustomExport
{
    /// <summary>
    /// �Զ��嵼������ö�٣��ͻ��ˡ�������
    /// </summary>
    protected enum CustomExportType
    {
        /// <summary>
        /// �ͻ���
        /// </summary>
        Client,
        /// <summary>
        /// ������
        /// </summary>
        Server
    }

    /// <summary>
    /// ִ�е��������ĳ��󷽷�
    /// </summary>
    public abstract void Run();

    /// <summary>
    /// д���ļ����ݵ�ָ��λ��
    /// </summary>
    /// <param name="fileName">�ļ���</param>
    /// <param name="fileContent">�ļ�����</param>
    /// <param name="customExportType">�Զ��嵼������</param>
    protected void Write(string fileName, string fileContent, CustomExportType customExportType)
    {
        switch (customExportType)
        {
            case CustomExportType.Client:
            {
                if (!Directory.Exists(Define.ClientCustomExportDirectory))
                {
                    Directory.CreateDirectory(Define.ClientCustomExportDirectory);
                }
                
                File.WriteAllText($"{Define.ClientCustomExportDirectory}/{fileName}", fileContent);
                return;
            }
            case CustomExportType.Server:
            {
                if (!Directory.Exists(Define.ServerCustomExportDirectory))
                {
                    Directory.CreateDirectory(Define.ServerCustomExportDirectory);
                }
                
                File.WriteAllText($"{Define.ServerCustomExportDirectory}/{fileName}", fileContent);
                return;
            }
        }
    }
}
#endif
