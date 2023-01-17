using UnityEngine;

namespace Game
{
    public abstract class Purchasable : MonoBehaviour
    {
        [SerializeField] private PurchaseArea _purchaseArea;

        private void Awake()
        {
            _purchaseArea.Completed += PurchaseAreaOnCompleted;
        }

        private void OnDestroy()
        {
            _purchaseArea.Completed -= PurchaseAreaOnCompleted;
        }

        protected abstract void OnPurchase();

        private void PurchaseAreaOnCompleted()
        {
            OnPurchase();
            _purchaseArea.Completed -= PurchaseAreaOnCompleted;
        }
    }
}