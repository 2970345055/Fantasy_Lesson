using System;
using System.Collections.Generic;

namespace Fantasy.Core.Network
{
    /// <summary>
    /// ��ַӳ�������������ڹ����ַӳ���������
    /// </summary>
    public sealed class AddressableManageComponent : Entity
    {
        private readonly Dictionary<long, long> _addressable = new();
        private readonly Dictionary<long, WaitCoroutineLock> _locks = new();
        private readonly CoroutineLockQueueType _addressableLock = new CoroutineLockQueueType("AddressableLock");

        /// <summary>
        /// ��ӵ�ַӳ�䡣
        /// </summary>
        /// <param name="addressableId">��ַӳ���Ψһ��ʶ��</param>
        /// <param name="routeId">·�� ID��</param>
        /// <param name="isLock">�Ƿ����������</param>
        public async FTask Add(long addressableId, long routeId, bool isLock)
        {
            WaitCoroutineLock waitCoroutineLock = null;
            try
            {
                if (isLock)
                {
                    waitCoroutineLock = await _addressableLock.Lock(addressableId);
                }
                
                _addressable[addressableId] = routeId;
                Log.Debug($"AddressableManageComponent Add addressableId:{addressableId} routeId:{routeId}");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            finally
            {
                waitCoroutineLock?.Dispose();
            }
        }

        /// <summary>
        /// ��ȡ��ַӳ���·�� ID��
        /// </summary>
        /// <param name="addressableId">��ַӳ���Ψһ��ʶ��</param>
        /// <returns>��ַӳ���·�� ID��</returns>
        public async FTask<long> Get(long addressableId)
        {
            using (await _addressableLock.Lock(addressableId))
            {
                _addressable.TryGetValue(addressableId, out var routeId);
                return routeId;
            }
        }

        /// <summary>
        /// �Ƴ���ַӳ�䡣
        /// </summary>
        /// <param name="addressableId">��ַӳ���Ψһ��ʶ��</param>
        public async FTask Remove(long addressableId)
        {
            using (await _addressableLock.Lock(addressableId))
            {
                _addressable.Remove(addressableId);
                Log.Debug($"Addressable Remove addressableId: {addressableId} _addressable:{_addressable.Count}");
            }
        }

        /// <summary>
        /// ������ַӳ�䡣
        /// </summary>
        /// <param name="addressableId">��ַӳ���Ψһ��ʶ��</param>
        public async FTask Lock(long addressableId)
        {
            var waitCoroutineLock = await _addressableLock.Lock(addressableId);
            _locks.Add(addressableId, waitCoroutineLock);
        }

        /// <summary>
        /// ������ַӳ�䡣
        /// </summary>
        /// <param name="addressableId">��ַӳ���Ψһ��ʶ��</param>
        /// <param name="routeId">�µ�·�� ID��</param>
        /// <param name="source">������Դ��</param>
        public void UnLock(long addressableId, long routeId, string source)
        {
            if (!_locks.Remove(addressableId, out var coroutineLock))
            {
                Log.Error($"Addressable unlock not found addressableId: {addressableId} Source:{source}");
                return;
            }

            _addressable.TryGetValue(addressableId, out var oldAddressableId);

            if (routeId != 0)
            {
                _addressable[addressableId] = routeId;
            }

            coroutineLock.Dispose();
            Log.Debug($"Addressable UnLock key: {addressableId} oldAddressableId : {oldAddressableId} routeId: {routeId}  Source:{source}");
        }
    }
}