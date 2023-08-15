#if FANTASY_NET
namespace Fantasy.Core.Network;

/// <summary>
/// ����һ�� sealed �� I_AddressableAddHandler���̳��� RouteRPC �࣬��ָ�����Ͳ���
/// </summary>
public sealed class I_AddressableAddHandler : RouteRPC<Scene, I_AddressableAdd_Request, I_AddressableAdd_Response>
{
    /// <summary>
    /// ���յ���ַӳ���������ʱִ�е��߼���
    /// </summary>
    /// <param name="scene">��ǰ����ʵ����</param>
    /// <param name="request">����������Ϣ�� I_AddressableAdd_Request ʵ����</param>
    /// <param name="response">���ڹ�����Ӧ�� I_AddressableAdd_Response ʵ����</param>
    /// <param name="reply">ִ����Ӧ�Ļص�������</param>
    protected override async FTask Run(Scene scene, I_AddressableAdd_Request request, I_AddressableAdd_Response response, Action reply)
    {
        await scene.GetComponent<AddressableManageComponent>().Add(request.AddressableId, request.RouteId, request.IsLock);
    }
}
#endif