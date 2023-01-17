using UnityEngine;

namespace Game
{
    public class PurchasableGameObject : Purchasable
    {
        [SerializeField] private GameObject _target;

        private void Start()
        {
            _target.SetActive(false);
        }

        protected override void OnPurchase()
        {
            _target.SetActive(true);
        }
    }
}