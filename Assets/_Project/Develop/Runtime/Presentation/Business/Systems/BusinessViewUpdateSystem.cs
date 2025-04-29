using System.Collections.Generic;
using Leopotam.Ecs;
using _Project.Develop.Runtime.Presentation.BusinessViews.Views;
using _Project.Develop.Runtime.Domain.BusinessFeature.Components;
using _Project.Develop.Runtime.Domain.UpgradeFeature.Components;
using _Project.Develop.Runtime.Domain.Shared;
using _Project.Develop.Runtime.UtilTools;

namespace _Project.Develop.Runtime.Presentation.BusinessViews.Systems
{
    public sealed class BusinessViewUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Business, UpdateUIFlag> _businessFilter;

        private readonly Dictionary<int, BusinessView> _idViewMap;

        public BusinessViewUpdateSystem(Dictionary<int, BusinessView> idViewMap)
        {
            _idViewMap = idViewMap;
        }

        public void Run()
        {
            foreach (var i in _businessFilter)
            {
                ref var business = ref _businessFilter.Get1(i);
                ref var upgrade1 = ref business.Upgrade1.Get<Upgrade>();
                ref var upgrade2 = ref business.Upgrade2.Get<Upgrade>();

                if (!_idViewMap.TryGetValue(business.BusinessId, out var businessView)) continue;

                businessView.SetLevel(business.Level);
                var multiplier1 = upgrade1.IsPurchased ? upgrade1.IncomeMultiplier : 0;
                var multiplier2 = upgrade2.IsPurchased ? upgrade2.IncomeMultiplier : 0;
                businessView.SetIncome(Utils.GetCurrentIncome(business.Level, business.BaseIncome, multiplier1, multiplier2));
                businessView.SetLevelUpPrice(Utils.GetNextLevelPrice(business.Level, business.BaseCost));
            }
        }
    }
}