using UnityEngine;

namespace _Project.Develop.Runtime.Data.Configs
{
    [System.Serializable]
    public class BusinessUpgradeConfig
    {
        [SerializeField] private string _upgradeKey;

        [SerializeField] private float _cost;
        [SerializeField] private float _incomeMultiplier;

        public string UpgradeKey => _upgradeKey;
        public float Cost => _cost;
        public float IncomeMultiplier => _incomeMultiplier;
    }
}