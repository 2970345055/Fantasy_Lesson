namespace Fantasy.Core.Network
{
    /// <summary>
    /// ��ʾ����ͨ��Э�����͵�ö�١�
    /// </summary>
    public enum NetworkProtocolType
    {
        /// <summary>
        /// δָ��Э�����͡�
        /// </summary>
        None = 0,

        /// <summary>
        /// ʹ��KCP��KCPЭ�飩����ͨ�š�
        /// </summary>
        KCP = 1,

        /// <summary>
        /// ʹ��TCP���������Э�飩����ͨ�š�
        /// </summary>
        TCP = 2
    }
}