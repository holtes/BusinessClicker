using System.Collections.Generic;
using Leopotam.Ecs;
using _Project.Develop.Runtime.Domain.UpgradeFeature.Components;
using _Project.Develop.Runtime.Domain.BusinessFeature.Components;
using _Project.Develop.Runtime.Domain.BalanceFeature.Components;
using _Project.Develop.Runtime.Domain.Shared;

namespace _Project.Develop.Runtime.Domain.UpgradeFeature.Systems
{
    public sealed class UpgradePurchaseSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<UpgradePurchaseRequest> _upgradeRequests;
        private EcsComponentPool<Balance> _balancePool;

        private readonly Dictionary<int, EcsEntity> _IdEntityMap;

        public UpgradePurchaseSystem(Dictionary<int, EcsEntity> IdEntityMap)
        {
            _IdEntityMap = IdEntityMap;
        }

        public void Init()
        {
            _balancePool = _world.GetPool<Balance>();
        }

        public void Run()
        {
            foreach (var i in _upgradeRequests)
            {
                ref var request = ref _upgradeRequests.Get1(i);

                int targetEntityId = request.BusinessEntityId;
                int upgradeIndex = request.UpgradeIndex;

                EcsEntity businessEntity;

                if (!_IdEntityMap.TryGetValue(targetEntityId, out businessEntity)) continue;

                ref var business = ref businessEntity.Get<Business>();

                EcsEntity upgradeEntity = upgradeIndex == 0 ? business.Upgrade1 : business.Upgrade2;

                ref var upgrade = ref upgradeEntity.Get<Upgrade>();
                ref var balance = ref _balancePool.GetItem(0);

                if (balance.Value >= upgrade.Cost)
                {
                    ref var balanceChangeRequest = ref _world.NewEntity().Get<BalanceChangeRequest>();
                    balanceChangeRequest.Amount = -upgrade.Cost;

                    upgrade.IsPurchased = true;

                    upgradeEntity.Get<UpdateUIFlag>();
                }

                _upgradeRequests.GetEntity(i).Destroy();
            }
        }
    }

}