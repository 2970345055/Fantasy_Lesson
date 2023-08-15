#if FANTASY_NET
namespace Fantasy.Core.Network;

/// <summary>
/// ����һ�� sealed �� I_AddressableRemoveHandler���̳��� RouteRPC �࣬��ָ�����Ͳ��� 
/// </summary>
public sealed class I_AddressableRemoveHandler : RouteRPC<Scene, I_AddressableRemove_Request, I_AddressableRemove_Response>
{
    /// <summary>
    /// ���յ���ַӳ���Ƴ�����ʱִ�е��߼���
    /// </summary>
    /// <param name="scene">��ǰ����ʵ����</param>
    /// <param name="request">����������Ϣ�� I_AddressableRemove_Request ʵ����</param>
    /// <param name="response">���ڹ�����Ӧ�� I_AddressableRemove_Response ʵ����</param>
    /// <param name="reply">ִ����Ӧ�Ļص�������</param>
    protected override async FTask Run(Scene scene, I_AddressableRemove_Request request, I_AddressableRemove_Response response, Action reply)
    {
        await scene.GetComponent<AddressableManageComponent>().Remove(request.AddressableId);
    }
}
#endif