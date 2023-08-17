#if FANTASY_NET
namespace Fantasy.Core.Network;

/// <summary>
/// ����һ�� sealed �� I_AddressableLockHandler���̳��� RouteRPC �࣬��ָ�����Ͳ���
/// </summary>
public sealed class I_AddressableLockHandler : RouteRPC<Scene, I_AddressableLock_Request, I_AddressableLock_Response>
{
    /// <summary>
    /// ���յ���ַӳ����������ʱִ�е��߼���
    /// </summary>
    /// <param name="scene">��ǰ����ʵ����</param>
    /// <param name="request">����������Ϣ�� I_AddressableLock_Request ʵ����</param>
    /// <param name="response">���ڹ�����Ӧ�� I_AddressableLock_Response ʵ����</param>
    /// <param name="reply">ִ����Ӧ�Ļص�������</param>
    protected override async FTask Run(Scene scene, I_AddressableLock_Request request, I_AddressableLock_Response response, Action reply)
    {
        await scene.GetComponent<AddressableManageComponent>().Lock(request.AddressableId);
    }
}
#endif
