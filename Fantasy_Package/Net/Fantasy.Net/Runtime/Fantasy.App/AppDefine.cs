#pragma warning disable CS8618
namespace Fantasy;

/// <summary>
/// ����̨����������
/// </summary>
public static class AppDefine
{
    /// <summary>
    /// ������ѡ��
    /// </summary>
    public static CommandLineOptions Options;
    /// <summary>
    /// App����Id
    /// </summary>
    public static uint AppId => Options.AppId;
}