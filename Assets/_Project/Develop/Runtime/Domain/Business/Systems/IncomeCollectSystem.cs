using Leopotam.Ecs;
using _Project.Develop.Runtime.Domain.BalanceFeature.Components;
using _Project.Develop.Runtime.Domain.BusinessFeature.Components;
using _Project.Develop.Runtime.Domain.UpgradeFeature.Components;
using _Project.Develop.Runtime.UtilTools;

namespace _Project.Develop.Runtime.Domain.BusinessFeature.Systems
{
    public sealed class IncomeCollectSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<Business, IncomeProgress> _businessFilter;

        public void Run()
        {
            foreach (var i in _businessFilter)
            {
                ref var business = ref _businessFilter.Get1(i);
                ref var progress = ref _businessFilter.Get2(i);

                if (progress.CurrentProgress < 1f) continue;

                ref var upgrade1 = ref business.Upgrade1.Get<Upgrade>();
                var multiplier1 = upgrade1.IsPurchased ? upgrade1.IncomeMultiplier : 0;

                ref var upgrade2 = ref business.Upgrade2.Get<Upgrade>();
                var multiplier2 = upgrade1.IsPurchased ? upgrade1.IncomeMultiplier : 0;

                ref var balanceChangeRequest = ref _world.NewEntity().Get<BalanceChangeRequest>();
                balanceChangeRequest.Amount = Utils.GetCurrentIncome(business.Level, business.BaseIncome, multiplier1, multiplier2);

                progress.CurrentProgress = 0f;
            }
        }
    }
}