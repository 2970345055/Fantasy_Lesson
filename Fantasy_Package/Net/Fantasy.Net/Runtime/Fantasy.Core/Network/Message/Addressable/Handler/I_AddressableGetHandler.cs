#if FANTASY_NET
namespace Fantasy.Core.Network;

/// <summary>
/// ����һ�� sealed �� I_AddressableGetHandler���̳��� RouteRPC �࣬��ָ�����Ͳ���
/// </summary>
public sealed class I_AddressableGetHandler : RouteRPC<Scene, I_AddressableGet_Request, I_AddressableGet_Response>
{
    /// <summary>
    /// ���յ���ַӳ���ȡ����ʱִ�е��߼���
    /// </summary>
    /// <param name="scene">��ǰ����ʵ����</param>
    /// <param name="request">����������Ϣ�� I_AddressableGet_Request ʵ����</param>
    /// <param name="response">���ڹ�����Ӧ�� I_AddressableGet_Response ʵ����</param>
    /// <param name="reply">ִ����Ӧ�Ļص�������</param>
    protected override async FTask Run(Scene scene, I_AddressableGet_Request request, I_AddressableGet_Response response, Action reply)
    {
        response.RouteId =  await scene.GetComponent<AddressableManageComponent>().Get(request.AddressableId);
    }
}
#endif
