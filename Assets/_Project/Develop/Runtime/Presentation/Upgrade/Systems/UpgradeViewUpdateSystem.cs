using System.Collections.Generic;
using Leopotam.Ecs;
using _Project.Develop.Runtime.Presentation.BusinessViews.Views;
using _Project.Develop.Runtime.Domain.BusinessFeature.Components;
using _Project.Develop.Runtime.Domain.Shared;

namespace _Project.Develop.Runtime.Presentation.UpgradeViews.Systems
{
    public class UpgradeViewUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Business> _businessFilter;

        private readonly Dictionary<int, BusinessView> _idViewMap;

        public UpgradeViewUpdateSystem(Dictionary<int, BusinessView> idViewMap)
        {
            _idViewMap = idViewMap;
        }

        public void Run()
        {
            foreach (var i in _businessFilter)
            {
                ref var business = ref _businessFilter.Get1(i);

                if (!_idViewMap.TryGetValue(business.BusinessId, out var businessView)) continue;

                if (business.Upgrade1.Has<UpdateUIFlag>())
                {
                    var upgrade1 = businessView.GetUpgradeView(0);
                    upgrade1.SetPurchased();
                }

                if (business.Upgrade2.Has<UpdateUIFlag>())
                {
                    var upgrade2 = businessView.GetUpgradeView(1);
                    upgrade2.SetPurchased();
                }
            }
        }
    }
}