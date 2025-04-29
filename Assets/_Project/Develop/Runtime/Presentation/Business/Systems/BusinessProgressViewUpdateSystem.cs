using Leopotam.Ecs;
using System.Collections.Generic;
using _Project.Develop.Runtime.Presentation.BusinessViews.Views;
using _Project.Develop.Runtime.Domain.BusinessFeature.Components;


namespace _Project.Develop.Runtime.Presentation.BusinessViews.Systems
{
    public sealed class BusinessProgressViewUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Business, IncomeProgress> _businessFilter = null;

        private readonly Dictionary<int, BusinessView> _idViewMap;

        public BusinessProgressViewUpdateSystem(Dictionary<int, BusinessView> idViewMap)
        {
            _idViewMap = idViewMap;
        }

        public void Run()
        {
            foreach (var i in _businessFilter)
            {
                ref var business = ref _businessFilter.Get1(i);
                ref var progress = ref _businessFilter.Get2(i);

                if (!_idViewMap.TryGetValue(business.BusinessId, out var businessView)) continue;

                businessView.SetIncomeProgress(progress.CurrentProgress);
            }
        }
    }
}