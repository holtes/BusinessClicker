using UnityEngine;
using System.Collections.Generic;

namespace _Project.Develop.Runtime.Data.Configs
{
    [CreateAssetMenu(fileName = "New BusinessNamesDB", menuName = "Configs/Business Names Database")]
    public class BusinessNamesDB : ScriptableObject
    {
        [System.Serializable]
        public struct BusinessNameEntry
        {
            public string Key; 
            public string DisplayName;
        }

        [System.Serializable]
        public struct UpgradeNameEntry
        {
            public string Key;
            public string DisplayName;
        }

        [Header("Названия бизнесов")]
        [SerializeField] private List<BusinessNameEntry> _businessNames;

        [Header("Названия улучшений")]
        [SerializeField] private List<UpgradeNameEntry> _upgradeNames;

        public List<BusinessNameEntry> BusinessNames => _businessNames;
        public List<UpgradeNameEntry> UpgradeNames => _upgradeNames;

        public string GetBusinessNameByKey(string key)
        {
            foreach (var entry in BusinessNames)
            {
                if (entry.Key == key)
                    return entry.DisplayName;
            }
            return key;
        }

        public string GetUpgradeNameByKey(string key)
        {
            foreach (var entry in UpgradeNames)
            {
                if (entry.Key == key)
                    return entry.DisplayName;
            }
            return key;
        }
    }
}
