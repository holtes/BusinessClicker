using Leopotam.Ecs;
using _Project.Develop.Runtime.Domain.BusinessFeature.Components;
using _Project.Develop.Runtime.Domain.Shared;

namespace _Project.Develop.Runtime.Domain.BusinessFeature.Systems
{
    public sealed class IncomeProgressSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Business, IncomeProgress> _businessFilter;
        private readonly TimeService _timeService;

        public IncomeProgressSystem(TimeService timeService)
        {
            _timeService = timeService;
        }

        public void Run()
        {
            float deltaTime = _timeService.DeltaTime;

            foreach (var i in _businessFilter)
            {
                ref var business = ref _businessFilter.Get1(i);
                ref var progress = ref _businessFilter.Get2(i);

                if (business.Level <= 0) continue;

                progress.CurrentProgress += deltaTime / business.IncomeDelay;

                if (progress.CurrentProgress > 1f) progress.CurrentProgress = 1f;
            }
        }
    }
}