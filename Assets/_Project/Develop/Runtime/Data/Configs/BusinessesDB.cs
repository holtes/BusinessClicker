using UnityEngine;

namespace _Project.Develop.Runtime.Data.Configs
{
    [CreateAssetMenu(fileName = "New BusinessesDB", menuName = "Configs/Businesses Database")]
    public class BusinessesDB : ScriptableObject
    {
        [SerializeField] private BusinessConfig[] _businesses;

        public BusinessConfig[] Businesses => _businesses;
    }
}
