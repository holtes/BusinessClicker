using Leopotam.Ecs;

namespace _Project.Develop.Runtime.Domain.BusinessFeature.Components
{
    public struct Business
    {
        public string BusinessName;

        public int BusinessId;
        public int Level;

        public float BaseIncome;
        public float BaseCost;
        public float IncomeDelay;

        public EcsEntity Upgrade1;
        public EcsEntity Upgrade2;
    }
}