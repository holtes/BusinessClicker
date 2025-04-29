using Leopotam.Ecs;
using _Project.Develop.Runtime.Domain.SaveLoadFeature.Interfaces;
using _Project.Develop.Runtime.Domain.BusinessFeature.Components;
using _Project.Develop.Runtime.Domain.UpgradeFeature.Components;
using _Project.Develop.Runtime.Domain.BalanceFeature.Components;

namespace _Project.Develop.Runtime.Domain.SaveLoadFeature.Systems
{
    public sealed class LoadSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<Business, IncomeProgress> _businessFilter;
        private EcsComponentPool<Balance> _balancePool;

        private readonly ISaveLoadService _saveLoadService;

        public LoadSystem(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public void Init()
        {
            var loadedData = _saveLoadService.Load();
            if (loadedData == null) return;

            _balancePool = _world.GetPool<Balance>();
            ref var balance = ref _balancePool.GetItem(0);
            balance.Value = loadedData.Balance;

            foreach (var i in _businessFilter)
            {
                ref var business = ref _businessFilter.Get1(i);
                ref var incomeProgress = ref _businessFilter.Get2(i);

                var savedBusiness = loadedData.Businesses[business.BusinessId];
                business.Level = savedBusiness.Level;
                ref var upgrade1 = ref business.Upgrade1.Get<Upgrade>();
                upgrade1.IsPurchased = savedBusiness.Upgrade1Purchased;
                ref var upgrade2 = ref business.Upgrade2.Get<Upgrade>();
                upgrade2.IsPurchased = savedBusiness.Upgrade2Purchased;
                incomeProgress.CurrentProgress = savedBusiness.IncomeProgress;
            }
        }
    }
}