using Leopotam.Ecs;
using _Project.Develop.Runtime.Domain.BalanceFeature.Components;
using _Project.Develop.Runtime.Domain.Shared;

namespace _Project.Develop.Runtime.Domain.BalanceFeature.Systems
{
    public sealed class BalanceUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BalanceChangeRequest> _requestFilter;
        private readonly EcsFilter<Balance> _balanceFilter;

        public void Run()
        {
            foreach (var i in _balanceFilter)
            {
                ref var balance = ref _balanceFilter.Get1(i);

                foreach (var j in _requestFilter)
                {
                    ref var request = ref _requestFilter.Get1(j);
                    balance.Value += request.Amount;

                    _requestFilter.GetEntity(j).Destroy();
                    _balanceFilter.GetEntity(i).Get<UpdateUIFlag>();
                }
            }
        }
    }

}