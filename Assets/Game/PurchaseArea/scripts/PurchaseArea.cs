using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Game
{
    public class PurchaseArea : MonoBehaviour
    {
        [SerializeField] private int _price;
        [SerializeField] private float _cashTakeInterval;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private TriggerObject _playerTrigger;
        private int _remainingPrice;
        private Coroutine _takeCashRoutine;

        private void Start()
        {
            _playerTrigger.Entered += PlayerTriggerOnEntered;
            _playerTrigger.Exit += PlayerTriggerOnExit;
            _priceText.SetText(_price.ToString());
            _remainingPrice = _price;
        }

        private void OnDestroy()
        {
            _playerTrigger.Entered -= PlayerTriggerOnEntered;
            _playerTrigger.Exit -= PlayerTriggerOnExit;
        }

        public event Action Completed;

        private void PlayerTriggerOnEntered(Collider obj)
        {
            var cashVfx = obj.GetComponent<CashVFXReference>().Value;

            if (_takeCashRoutine != null)
            {
                StopCoroutine(_takeCashRoutine);
            }

            _takeCashRoutine = StartCoroutine(TakeCash(cashVfx));
        }

        private IEnumerator TakeCash(ParticleSystem cashVfx)
        {
            while (_remainingPrice > 0)
            {
                while (_playerData.Cash <= 0)
                {
                    yield return null;
                }

                _playerData.RemoveCash(1);
                cashVfx.Emit(1);
                _remainingPrice--;

                _priceText.SetText(_remainingPrice.ToString());

                yield return new WaitForSeconds(_cashTakeInterval);
            }

            Debug.Log("Completed");
            Completed?.Invoke();
            
            gameObject.SetActive(false);
        }

        private void PlayerTriggerOnExit(Collider obj)
        {
            if (_takeCashRoutine != null)
            {
                StopCoroutine(_takeCashRoutine);
            }
        }
    }
}