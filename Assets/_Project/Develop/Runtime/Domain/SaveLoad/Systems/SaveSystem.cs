using Leopotam.Ecs;
using System.Collections.Generic;
using _Project.Develop.Runtime.Domain.SaveLoadFeature.Interfaces;
using _Project.Develop.Runtime.Domain.SaveLoadFeature.Models;
using _Project.Develop.Runtime.Domain.BusinessFeature.Components;
using _Project.Develop.Runtime.Domain.UpgradeFeature.Components;
using _Project.Develop.Runtime.Domain.BalanceFeature.Components;

namespace _Project.Develop.Runtime.Domain.SaveLoadFeature.Systems
{
    public sealed class SaveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly ISaveLoadService _saveLoadService;
        private readonly EcsFilter<Business, IncomeProgress> _businessFilter;
        private EcsComponentPool<Balance> _balancePool;

        public SaveSystem(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public void Init()
        {
            _balancePool = _world.GetPool<Balance>();
        }

        public void Run()
        {
            var saveData = new GameSaveData();

            ref var balance = ref _balancePool.GetItem(0);
            saveData.Balance = balance.Value;

            saveData.Businesses = new List<BusinessSaveData>();

            foreach (var i in _businessFilter)
            {
                ref var business = ref _businessFilter.Get1(i);
                ref var progress = ref _businessFilter.Get2(i);

                var businessSave = new BusinessSaveData
                {
                    Level = business.Level,
                    Upgrade1Purchased = business.Upgrade1.Get<Upgrade>().IsPurchased,
                    Upgrade2Purchased = business.Upgrade2.Get<Upgrade>().IsPurchased,
                    IncomeProgress = progress.CurrentProgress
                };

                saveData.Businesses.Add(businessSave);
            }

            _saveLoadService.Save(saveData);
        }
    }
}