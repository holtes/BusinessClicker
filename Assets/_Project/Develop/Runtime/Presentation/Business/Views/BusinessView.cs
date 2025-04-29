using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Leopotam.Ecs;
using R3;
using _Project.Develop.Runtime.Presentation.UpgradeViews.Views;
using _Project.Develop.Runtime.Domain.LevelUpFeature.Components;

namespace _Project.Develop.Runtime.Presentation.BusinessViews.Views
{
    public class BusinessView : MonoBehaviour
    {
        [Header("Элементы")]
        [SerializeField] private TMP_Text _businessNameText;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _incomeText;
        [SerializeField] private TMP_Text _levelUpPriceText;
        [SerializeField] private Button _levelUpButton;
        [SerializeField] private Slider _incomeProgressBar;

        [Header("Улучшения")]
        [SerializeField] private UpgradeView _upgradeView1;
        [SerializeField] private UpgradeView _upgradeView2;

        public int BusinessEntityId { get; private set; }
        private EcsWorld _world;

        public void Init(EcsWorld world, int entityId)
        {
            BusinessEntityId = entityId;
            _world = world;
            _upgradeView1.Init(world, entityId, 0);
            _upgradeView2.Init(world, entityId, 1);
        }

        private void Awake()
        {
            _levelUpButton.OnClickAsObservable()
                .Subscribe(_ => OnLevelUpButtonClicked())
                .AddTo(this);
        }

        private void OnLevelUpButtonClicked()
        {
            ref var request = ref _world.NewEntity().Get<LevelUpRequest>();
            request.BusinessEntityId = BusinessEntityId;
        }

        public void Setup(string businessName, int level, float income, float levelUpCost, float progress)
        {
            SetBusinessName(businessName);
            SetLevel(level);
            SetIncome(income);
            SetLevelUpPrice(levelUpCost);
            SetIncomeProgress(progress);
        }

        public void SetupUpgrades(string upgrade1Name, float upgrade1Cost, float upgrade1Income, bool isupgrade1Purchased,
            string upgrade2Name, float upgrade2Cost, float upgrade2Income, bool isupgrade2Purchased)
        {
            _upgradeView1.SetUpgradeName(upgrade1Name);
            _upgradeView1.SetUpgradeCost(upgrade1Cost);
            _upgradeView1.SetIncome(upgrade1Income);
            if (isupgrade1Purchased) _upgradeView1.SetPurchased();

            _upgradeView2.SetUpgradeName(upgrade2Name);
            _upgradeView2.SetUpgradeCost(upgrade2Cost);
            _upgradeView2.SetIncome(upgrade2Income);
            if (isupgrade2Purchased) _upgradeView2.SetPurchased();
        }

        public void SetBusinessName(string name)
        {
            _businessNameText.text = name;
        }

        public void SetLevel(int level)
        {
            _levelText.text = $"LVL {level}";
        }

        public void SetIncome(float income)
        {
            _incomeText.text = $"Доход: ${income}";
        }

        public void SetLevelUpPrice(float price)
        {
            _levelUpPriceText.text = $"Цена: ${price}";
        }

        public void SetIncomeProgress(float progress)
        {
            _incomeProgressBar.value = progress;
        }

        public UpgradeView GetUpgradeView(int index)
        {
            return index == 0 ? _upgradeView1 : _upgradeView2;
        }
    }
}