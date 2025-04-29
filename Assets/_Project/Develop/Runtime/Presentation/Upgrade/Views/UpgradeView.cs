using UnityEngine;
using TMPro;
using UnityEngine.UI;
using R3;
using Leopotam.Ecs;
using _Project.Develop.Runtime.Domain.UpgradeFeature.Components;

namespace _Project.Develop.Runtime.Presentation.UpgradeViews.Views
{
    [RequireComponent(typeof(Button))]
    public class UpgradeView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _upgradeNameText;
        [SerializeField] private TMP_Text _upgradeIncomeText;
        [SerializeField] private TMP_Text _upgradeCostText;
        [SerializeField] private Button _upgradeButton;

        private int _businessEntityId;
        private int _upgradeIndex;
        private EcsWorld _world;

        public void Init(EcsWorld world, int entityId, int upgradeIndex)
        {
            _world = world;
            _businessEntityId = entityId;
            _upgradeIndex = upgradeIndex;
        }

        private void Awake()
        {
            _upgradeButton.OnClickAsObservable()
                .Subscribe(_ => OnUpgradeButtonClicked())
                .AddTo(this);
        }

        private void OnUpgradeButtonClicked()
        {
            ref var request = ref _world.NewEntity().Get<UpgradePurchaseRequest>();
            request.BusinessEntityId = _businessEntityId;
            request.UpgradeIndex = _upgradeIndex;
        }

        public void SetUpgradeName(string name)
        {
            _upgradeNameText.text = name;
        }

        public void SetUpgradeCost(float cost)
        {
            _upgradeCostText.text = $"Цена: {cost}$";
        }

        public void SetIncome(float income)
        {
            _upgradeIncomeText.text = $"+ {income * 100}%";
        }

        public void SetPurchased()
        {
            _upgradeCostText.text = "Куплено";
            _upgradeButton.interactable = false;
        }

        public void SetAvailable(bool isAvailable)
        {
            _upgradeButton.interactable = isAvailable;
        }
    }
}