using System.Collections.Generic;
using Leopotam.Ecs;
using _Project.Develop.Runtime.Domain.LevelUpFeature.Components;
using _Project.Develop.Runtime.Domain.BalanceFeature.Components;
using _Project.Develop.Runtime.Domain.BusinessFeature.Components;
using _Project.Develop.Runtime.Domain.Shared;
using _Project.Develop.Runtime.UtilTools;


namespace _Project.Develop.Runtime.Domain.LevelUpFeature.Systems
{
    public sealed class LevelUpSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<LevelUpRequest> _levelUpRequests;
        private EcsComponentPool<Balance> _balancePool;

        private readonly Dictionary<int, EcsEntity> _IdEntityMap;

        public LevelUpSystem(Dictionary<int, EcsEntity> IdEntityMap)
        {
            _IdEntityMap = IdEntityMap;
        }

        public void Init()
        {
            _balancePool = _world.GetPool<Balance>();
        }

        public void Run()
        {
            foreach (var i in _levelUpRequests)
            {
                ref var request = ref _levelUpRequests.Get1(i);

                int targetEntityId = request.BusinessEntityId;

                if (!_IdEntityMap.TryGetValue(targetEntityId, out var businessEntity)) continue;

                ref var business = ref businessEntity.Get<Business>();
                ref var balance = ref _balancePool.GetItem(0);

                float levelUpCost = Utils.GetNextLevelPrice(business.Level, business.BaseCost);

                if (balance.Value >= levelUpCost)
                {
                    ref var balanceChangeRequest = ref _world.NewEntity().Get<BalanceChangeRequest>();
                    balanceChangeRequest.Amount = -levelUpCost;

                    business.Level++;

                    businessEntity.Get<UpdateUIFlag>();
                }

                _levelUpRequests.GetEntity(i).Destroy();
            }
        }
    }
}