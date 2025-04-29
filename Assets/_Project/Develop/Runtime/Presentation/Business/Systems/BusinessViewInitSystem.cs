using System.Collections.Generic;
using Leopotam.Ecs;
using _Project.Develop.Runtime.Presentation.BusinessViews.Views;
using _Project.Develop.Runtime.Domain.BusinessFeature.Components;
using _Project.Develop.Runtime.Domain.UpgradeFeature.Components;
using _Project.Develop.Runtime.Domain.Shared;
using _Project.Develop.Runtime.UtilTools;

namespace _Project.Develop.Runtime.Presentation.BusinessViews.Systems
{
    public sealed class BusinessViewInitSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<Business, IncomeProgress, ViewInitRequest> _businessFilter;

        private readonly BusinessListView _businessListView;
        private readonly BusinessView _businessViewPrefab;
        private readonly Dictionary<int, EcsEntity> _idEnityMap;
        private readonly Dictionary<int, BusinessView> _idViewMap;

        public BusinessViewInitSystem(
            BusinessListView businessListView,
            BusinessView businessViewPrefab,
            Dictionary<int, EcsEntity> idEntityMap,
            Dictionary<int, BusinessView> idViewMap)
        {
            _businessListView = businessListView;
            _businessViewPrefab = businessViewPrefab;
            _idEnityMap = idEntityMap;
            _idViewMap = idViewMap;
        }

        public void Run()
        {
            foreach (var i in _businessFilter)
            {
                ref var business = ref _businessFilter.Get1(i);
                ref var progress = ref _businessFilter.Get2(i);

                var businessView = _businessListView.CreateBusiness(_businessViewPrefab);
                businessView.Init(_world, i);

                SetupBusinessView(businessView, business, progress);

                _idEnityMap.Add(business.BusinessId, _businessFilter.GetEntity(i));
                _idViewMap.Add(business.BusinessId, businessView);
            }
        }

        private void SetupBusinessView(BusinessView businessView, Business business, IncomeProgress progress)
        {
            ref var upgrade1 = ref business.Upgrade1.Get<Upgrade>();
            ref var upgrade2 = ref business.Upgrade2.Get<Upgrade>();

            var multiplier1 = upgrade1.IsPurchased ? upgrade1.IncomeMultiplier : 0;
            var multiplier2 = upgrade2.IsPurchased ? upgrade2.IncomeMultiplier : 0;

            businessView.Setup(
                business.BusinessName,
                business.Level,
                Utils.GetCurrentIncome(business.Level, business.BaseIncome, multiplier1, multiplier2),
                Utils.GetNextLevelPrice(business.Level, business.BaseCost),
                progress.CurrentProgress
            );



            businessView.SetupUpgrades(
                upgrade1.UpgradeName,
                upgrade1.Cost,
                upgrade1.IncomeMultiplier,
                upgrade1.IsPurchased,
                upgrade2.UpgradeName,
                upgrade2.Cost,
                upgrade2.IncomeMultiplier,
                upgrade2.IsPurchased
            );
        }
    }
}