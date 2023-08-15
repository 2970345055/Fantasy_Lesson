#if FANTASY_NET

namespace Fantasy.Core.Network
{
    /// <summary>
    /// �ṩ������ַӳ��ĸ���������
    /// </summary>
    public static class AddressableHelper
    {
        // ����һ��˽�о�ֻ̬���б� AddressableScenes�����ڴ洢��ַӳ��ĳ���������Ϣ
        private static readonly List<SceneConfigInfo> AddressableScenes = new List<SceneConfigInfo>();

        static AddressableHelper()
        {
            // ��ȡ���г���������Ϣ
            var sceneConfigInfos = ConfigTableManage.AllSceneConfig();
            // ��������������Ϣ��ɸѡ����ַӳ�����͵ĳ���������ӵ� AddressableScenes �б���
            foreach (var sceneConfigInfo in sceneConfigInfos)
            {
                if (sceneConfigInfo.SceneTypeStr == "Addressable")
                {
                    AddressableScenes.Add(sceneConfigInfo);
                }
            }
        }

        /// <summary>
        /// ��ӵ�ַӳ�䲢���ز��������
        /// </summary>
        /// <param name="scene">����ʵ����</param>
        /// <param name="addressableId">��ַӳ���Ψһ��ʶ��</param>
        /// <param name="routeId">·�� ID��</param>
        /// <param name="isLock">�Ƿ�������</param>
        public static async FTask AddAddressable(Scene scene, long addressableId, long routeId, bool isLock = true)
        {
            // ��ȡָ�������ĵ�ַӳ�䳡��������Ϣ
            var addressableScene = AddressableScenes[(int)addressableId % AddressableScenes.Count];
            // �����ڲ�·�ɷ�����������ӵ�ַӳ������󲢵ȴ���Ӧ
            var response = await MessageHelper.CallInnerRoute(scene, addressableScene.EntityId,
                new I_AddressableAdd_Request
                {
                    AddressableId = addressableId, RouteId = routeId, IsLock = isLock
                });
            if (response.ErrorCode != 0)
            {
                Log.Error($"AddAddressable error is {response.ErrorCode}");
            }
        }

        /// <summary>
        /// ��ȡ��ַӳ���·�� ID��
        /// </summary>
        /// <param name="scene">����ʵ����</param>
        /// <param name="addressableId">��ַӳ���Ψһ��ʶ��</param>
        /// <returns>��ַӳ���·�� ID��</returns>
        public static async FTask<long> GetAddressableRouteId(Scene scene, long addressableId)
        {
            // ��ȡָ�������ĵ�ַӳ�䳡��������Ϣ
            var addressableScene = AddressableScenes[(int)addressableId % AddressableScenes.Count];
            // �����ڲ�·�ɷ��������ͻ�ȡ��ַӳ��·�� ID �����󲢵ȴ���Ӧ
            var response = (I_AddressableGet_Response) await MessageHelper.CallInnerRoute(scene, addressableScene.EntityId,
                new I_AddressableGet_Request
                {
                    AddressableId = addressableId
                });
            // �����Ӧ�����룬���Ϊ�㣬����·�� ID���������������Ϣ������ 0
            if (response.ErrorCode == 0)
            {
                return response.RouteId;
            }

            Log.Error($"GetAddressable error is {response.ErrorCode} addressableId:{addressableId}");
            return 0;
        }

        /// <summary>
        /// �Ƴ�ָ����ַӳ�䡣
        /// </summary>
        /// <param name="scene">����ʵ����</param>
        /// <param name="addressableId">��ַӳ���Ψһ��ʶ��</param>
        public static async FTask RemoveAddressable(Scene scene, long addressableId)
        {
            var addressableScene = AddressableScenes[(int)addressableId % AddressableScenes.Count];
            var response = await MessageHelper.CallInnerRoute(scene, addressableScene.EntityId,
                new I_AddressableRemove_Request
                {
                    AddressableId = addressableId
                });
            
            if (response.ErrorCode != 0)
            {
                Log.Error($"RemoveAddressable error is {response.ErrorCode}");
            }
        }

        /// <summary>
        /// ����ָ����ַӳ�䡣
        /// </summary>
        /// <param name="scene">����ʵ����</param>
        /// <param name="addressableId">��ַӳ���Ψһ��ʶ��</param>
        public static async FTask LockAddressable(Scene scene, long addressableId)
        {
            var addressableScene = AddressableScenes[(int)addressableId % AddressableScenes.Count];
            var response = await MessageHelper.CallInnerRoute(scene, addressableScene.EntityId,
                new I_AddressableLock_Request
                {
                    AddressableId = addressableId
                });
            
            if (response.ErrorCode != 0)
            {
                Log.Error($"LockAddressable error is {response.ErrorCode}");
            }
        }

        /// <summary>
        /// ����ָ����ַӳ�䡣
        /// </summary>
        /// <param name="scene">����ʵ����</param>
        /// <param name="addressableId">��ַӳ���Ψһ��ʶ��</param>
        /// <param name="routeId">·�� ID��</param>
        /// <param name="source">������Դ��</param>
        public static async FTask UnLockAddressable(Scene scene, long addressableId, long routeId, string source)
        {
            var addressableScene = AddressableScenes[(int)addressableId % AddressableScenes.Count];
            var response = await MessageHelper.CallInnerRoute(scene, addressableScene.EntityId,
                new I_AddressableUnLock_Request
                {
                    AddressableId = addressableId,
                    RouteId = routeId,
                    Source = source
                });

            if (response.ErrorCode != 0)
            {
                Log.Error($"UnLockAddressable error is {response.ErrorCode}");
            }
        }
    }
}
#endif