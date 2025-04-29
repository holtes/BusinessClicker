namespace _Project.Develop.Runtime.Domain.BusinessFeature.Models
{
    public struct BusinessInitData
    {
        public string BusinessName;
        public int BusinessId;
        public float BaseIncome;
        public float BaseCost;
        public float IncomeDelay;
        public UpgradeInitData Upgrade1;
        public UpgradeInitData Upgrade2;
    }
}