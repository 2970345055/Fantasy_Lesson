#if FANTASY_NET
using System;
using Fantasy.Core.Network;
namespace Fantasy
{
    /// <summary>
    /// ��ʾ�������³���ʱ�������¼����ݽṹ��
    /// </summary>
    public struct OnCreateScene
    {
        /// <summary>
        /// ��ȡ���¼������ĳ���ʵ�塣
        /// </summary>
        public readonly Scene Scene;

        /// <summary>
        /// ��ʼ��һ���µ� OnCreateScene ʵ����
        /// </summary>
        /// <param name="scene">���¼������ĳ���ʵ�塣</param>
        public OnCreateScene(Scene scene)
        {
            Scene = scene;
        }
    }
}
#endif
