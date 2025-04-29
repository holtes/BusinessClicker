using System.Collections.Generic;

namespace _Project.Develop.Runtime.Domain.SaveLoadFeature.Models
{
    [System.Serializable]
    public class GameSaveData
    {
        public float Balance;
        public List<BusinessSaveData> Businesses = new List<BusinessSaveData>();
    }
}
