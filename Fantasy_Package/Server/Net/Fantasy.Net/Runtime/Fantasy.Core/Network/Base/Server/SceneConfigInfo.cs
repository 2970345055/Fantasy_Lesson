namespace Fantasy
{
    /// <summary>
    /// ����������Ϣ���ࡣ
    /// </summary>
    public class SceneConfigInfo
    {
        /// <summary>
        /// ��ȡ�����ó�����Ψһ��ʶ��
        /// </summary>
        public uint Id;

        /// <summary>
        /// ��ȡ�����ó���ʵ���Ψһ��ʶ��
        /// </summary>
        public long EntityId;

        /// <summary>
        /// ��ȡ�����ó������͡�
        /// </summary>
        public int SceneType;

        /// <summary>
        /// ��ȡ�����ó��������͡�
        /// </summary>
        public int SceneSubType;

        /// <summary>
        /// ��ȡ�����ó������͵��ַ�����ʾ��
        /// </summary>
        public string SceneTypeStr;

        /// <summary>
        /// ��ȡ�����ó��������͵��ַ�����ʾ��
        /// </summary>
        public string SceneSubTypeStr;

        /// <summary>
        /// ��ȡ�����÷��������õ�Ψһ��ʶ��
        /// </summary>
        public uint ServerConfigId;

        /// <summary>
        /// ��ȡ�����������Ψһ��ʶ��
        /// </summary>
        public uint WorldId;

        /// <summary>
        /// ��ȡ�������ⲿ�˿ڡ�
        /// </summary>
        public int OuterPort;

        /// <summary>
        /// ��ȡ����������Э�顣
        /// </summary>
        public string NetworkProtocol;
    }
}



