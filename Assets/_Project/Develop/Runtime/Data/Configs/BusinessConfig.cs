using UnityEngine;

namespace _Project.Develop.Runtime.Data.Configs
{
    [System.Serializable]
    public class BusinessConfig
    {
        [SerializeField] private string _businessKey;

        [Header("Настройки")]
        [SerializeField] private float _baseIncome;
        [SerializeField] private float _baseCost; 
        [SerializeField] private float _incomeDelay;

        [Header("Улучшения")]
        [SerializeField] private BusinessUpgradeConfig _upgrade1;
        [SerializeField] private BusinessUpgradeConfig _upgrade2;

        public string BusinessKey => _businessKey;

        public float BaseIncome => _baseIncome;
        public float BaseCost => _baseCost;
        public float IncomeDelay => _incomeDelay;

        public BusinessUpgradeConfig Upgrade1 => _upgrade1;
        public BusinessUpgradeConfig Upgrade2 => _upgrade2;
    }
}

