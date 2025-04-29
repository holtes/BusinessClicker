using Leopotam.Ecs;
using _Project.Develop.Runtime.Presentation.BalanceViews.Views;
using _Project.Develop.Runtime.Domain.BalanceFeature.Components;
using _Project.Develop.Runtime.Domain.Shared;

namespace _Project.Develop.Runtime.Presentation.BalanceViews.Systems
{
    public sealed class BalanceViewUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Balance, UpdateUIFlag> _balanceFilter;

        private readonly BalanceView _balanceView;

        public BalanceViewUpdateSystem(BalanceView balanceView)
        {
            _balanceView = balanceView;
        }

        public void Run()
        {
            foreach (var i in _balanceFilter)
            {
                ref var balance = ref _balanceFilter.Get1(i);

                _balanceView.SetBalance(balance.Value);
            }
        }
    }
}