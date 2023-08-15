// ����ָ�����ڽ�ֹ ReSharper ���棬�Դ����Ϊ���������͵������
// �ڴ�����£����԰�ȫ�ؽ��þ��棬��Ϊ���Ǵ�����Ƿǿ�Ϊ�����͡�
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
namespace Fantasy
{
    /// <summary>
    /// ʵ������ֻ���ṹ�������� Entity ʵ�������á�
    /// </summary>
    /// <typeparam name="T">Entity �����͡�</typeparam>
    public readonly struct EntityReference<T> where T : Entity
    {
        private readonly T _entity;
        private readonly long _runTimeId;

        // �� Entity ʵ������ EntityReference ��˽�й��캯����
        private EntityReference(T t)
        {
            _entity = t;
            _runTimeId = t.RuntimeId;
        }

        /// <summary>
        /// ��ʽ�ؽ� Entity ʵ��ת��Ϊ EntityReference��
        /// </summary>
        /// <param name="t">Ҫת���� Entity ʵ����</param>
        /// <returns>����ͬһ Entity �� EntityReference ʵ����</returns>
        public static implicit operator EntityReference<T>(T t)
        {
            return new EntityReference<T>(t);
        }

        /// <summary>
        /// ��ʽ�ؽ� EntityReference ת����ԭʼ�� Entity ���͡�
        /// </summary>
        /// <param name="v">Ҫת���� EntityReference��</param>
        /// <returns>
        /// �������ʱ ID ƥ�䣬�򷵻�ԭʼ�� Entity ʵ���������ƥ���򷵻� null��
        /// �����������Ϊ null���򷵻� null��
        /// </returns>
        public static implicit operator T(EntityReference<T> v)
        {
            if (v._entity == null)
            {
                return null;
            }

            return v._entity.RuntimeId != v._runTimeId ? null : v._entity;
        }
    }
}