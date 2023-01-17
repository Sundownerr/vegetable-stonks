using UnityEngine;

namespace Game
{
    public class PurchasableItemStorage : Purchasable
    {
        [SerializeField] private StoragePOI _storagePoi;
        [SerializeField] private ClientSpawner _clientSpawner;

        private void Start()
        {
            _storagePoi.gameObject.SetActive(false);
        }

        protected override void OnPurchase()
        {
            _clientSpawner.Add(_storagePoi);
            _storagePoi.gameObject.SetActive(true);
        }
    }
}