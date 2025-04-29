using Leopotam.Ecs;
using _Project.Develop.Runtime.Domain.BusinessFeature.Models;
using _Project.Develop.Runtime.Domain.BusinessFeature.Components;
using _Project.Develop.Runtime.Domain.UpgradeFeature.Components;
using _Project.Develop.Runtime.Domain.Shared;

namespace _Project.Develop.Runtime.Domain.BusinessFeature.Systems
{
    public sealed class BusinessInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;

        private readonly BusinessInitData[] _initData;

        public BusinessInitSystem(BusinessInitData[] initData)
        {
            _initData = initData;
        }

        public void Init()
        {
            for (int i = 0; i < _initData.Length; i++)
            {
                var model = _initData[i];
                var businessEntity = _world.NewEntity();

                ref var business = ref businessEntity.Get<Business>();
                ref var incomeProgress = ref businessEntity.Get<IncomeProgress>();

                business.BusinessName = model.BusinessName;
                business.BusinessId = model.BusinessId;
                business.Level = (i == 0) ? 1 : 0;
                business.BaseIncome = model.BaseIncome;
                business.BaseCost = model.BaseCost;
                business.IncomeDelay = model.IncomeDelay;

                var upgrade1Entity = _world.NewEntity();
                ref var upgrade1 = ref upgrade1Entity.Get<Upgrade>();
                upgrade1.UpgradeName = model.Upgrade1.UpgradeName;
                upgrade1.Cost = model.Upgrade1.Cost;
                upgrade1.IncomeMultiplier = model.Upgrade1.IncomeMultiplier;
                business.Upgrade1 = upgrade1Entity;

                var upgrade2Entity = _world.NewEntity();
                ref var upgrade2 = ref upgrade2Entity.Get<Upgrade>();
                upgrade2.UpgradeName = model.Upgrade2.UpgradeName;
                upgrade2.Cost = model.Upgrade2.Cost;
                upgrade2.IncomeMultiplier = model.Upgrade2.IncomeMultiplier;
                business.Upgrade2 = upgrade2Entity;

                businessEntity.Get<ViewInitRequest>();
            }
        }
    }
}