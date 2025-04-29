using UnityEngine;

namespace _Project.Develop.Runtime.Presentation.BusinessViews.Views
{
    public class BusinessListView : MonoBehaviour
    {
        [SerializeField] private Transform _container;

        public BusinessView CreateBusiness(BusinessView businessPrefab)
        {
            var view = Instantiate(businessPrefab, _container);
            return view;
        }
    }
}
