using UnityEngine;
using TMPro;

namespace _Project.Develop.Runtime.Presentation.BalanceViews.Views
{
    public class BalanceView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _balanceText;

        public void SetBalance(float balance)
        {
            _balanceText.text = $"Баланс: ${balance:F0}";
        }
    }
}
