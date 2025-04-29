using Leopotam.Ecs;
using _Project.Develop.Runtime.Domain.BalanceFeature.Components;
using _Project.Develop.Runtime.Domain.Shared;

namespace _Project.Develop.Runtime.Domain.BalanceFeature.Systems
{
    public sealed class BalanceInitSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world;

        public void Init()
        {
            var balanceEntity = _world.NewEntity();
            ref var balance = ref balanceEntity.Get<Balance>();
            balance.Value = 0f;

            balanceEntity.Get<ViewInitRequest>();
        }
    }
}

