using UnityEngine;
using _Project.Develop.Runtime.Presentation.BusinessViews.Views;
using _Project.Develop.Runtime.Presentation.BalanceViews.Views;

namespace _Project.Develop.Runtime.Startup.Data
{
    public class SceneData : MonoBehaviour
    {
        [Header("Префабы")]
        [SerializeField] private BusinessView _businessPrefab;

        [Header("UI")]
        [SerializeField] private BusinessListView _businessListView;
        [SerializeField] private BalanceView _balanceView;

        public BusinessView BusinessPrefab => _businessPrefab;
        public BusinessListView BusinessListView => _businessListView;
        public BalanceView BalanceView => _balanceView;
    }

}