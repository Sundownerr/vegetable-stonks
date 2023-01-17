using System.Collections;
using UnityEngine;

namespace Game
{
    public class CashCollector : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private float _collectInterval = 0.15f;
        [SerializeField] private TriggerObject _cashRegisterTrigger;
        [SerializeField] private ItemMoverToPoints _itemMover;
        private Coroutine _collectRoutine;

        private void Start()
        {
            _cashRegisterTrigger.Entered += CashRegisterTriggerOnEntered;
            _cashRegisterTrigger.Exit += CashRegisterTriggerOnExit;
        }

        private void OnDestroy()
        {
            _cashRegisterTrigger.Entered -= CashRegisterTriggerOnEntered;
            _cashRegisterTrigger.Exit -= CashRegisterTriggerOnExit;
        }

        private void CashRegisterTriggerOnExit(Collider obj)
        {
            if (_collectRoutine != null)
            {
                StopCoroutine(_collectRoutine);
            }
        }

        private void CashRegisterTriggerOnEntered(Collider obj)
        {
            var cashStorage = obj.GetComponent<StorageReference>().Value;
            _collectRoutine = StartCoroutine(TakeItems(cashStorage));
        }

        private IEnumerator TakeItems(Storage cashStorage)
        {
            while (true)
            {
                while (!cashStorage.IsEmpty)
                {
                    var cash = cashStorage.GiveItem().GetComponent<Cash>();

                    _playerData.AddCash(cash.Value);
                    _itemMover.Move(cash.transform);

                    yield return new WaitForSeconds(_collectInterval);
                }

                yield return null;
            }
        }
    }
}