#if FANTASY_NET
namespace Fantasy.Core.Network;

/// <summary>
/// ����һ�� sealed �� I_AddressableUnLockHandler���̳��� RouteRPC �࣬��ָ�����Ͳ���
/// </summary>
public sealed class I_AddressableUnLockHandler : RouteRPC<Scene, I_AddressableUnLock_Request, I_AddressableUnLock_Response>
{
    /// <summary>
    /// ���յ���ַӳ���������ʱִ�е��߼���
    /// </summary>
    /// <param name="scene">��ǰ����ʵ����</param>
    /// <param name="request">����������Ϣ�� I_AddressableUnLock_Request ʵ����</param>
    /// <param name="response">���ڹ�����Ӧ�� I_AddressableUnLock_Response ʵ����</param>
    /// <param name="reply">ִ����Ӧ�Ļص�������</param>
    protected override async FTask Run(Scene scene, I_AddressableUnLock_Request request, I_AddressableUnLock_Response response, Action reply)
    {
        scene.GetComponent<AddressableManageComponent>().UnLock(request.AddressableId, request.RouteId, request.Source);
        await FTask.CompletedTask;
    }
}
#endif