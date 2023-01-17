using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CashRegister : MonoBehaviour
    {
        [SerializeField] private float _packItemsInterval;
        [SerializeField] private float _packItemsDelay;
        [SerializeField] private float _boxOpenDelay;
        [SerializeField] private float _boxCloseDelay;
        [SerializeField] private Transform _boxSpawnPoint;
        [SerializeField] private Transform _clientPoint;
        [SerializeField] private ItemBox _itemBoxPrefab;
        [SerializeField] private TriggerObject _operatorTrigger;
        [SerializeField] private CashFactory _cashFactory;

        private bool _active;
        private WaitForSeconds _boxCloseDelayWfs;
        private WaitForSeconds _boxOpenDelayWfs;

        private readonly List<int> _cashValues = new();
        private WaitForSeconds _packItemsDelayWfs;
        private WaitForSeconds _packItemsIntervalWfs;

        public Transform ClientPoint => _clientPoint;

        private void Start()
        {
            _packItemsIntervalWfs = new WaitForSeconds(_packItemsInterval);
            _packItemsDelayWfs = new WaitForSeconds(_packItemsDelay);
            _boxOpenDelayWfs = new WaitForSeconds(_boxOpenDelay);
            _boxCloseDelayWfs = new WaitForSeconds(_boxCloseDelay);

            _operatorTrigger.Entered += OnOperatorEntered;
            _operatorTrigger.Exit += OnOperatorExit;
        }

        private void OnDestroy()
        {
            _operatorTrigger.Entered -= OnOperatorEntered;
            _operatorTrigger.Exit -= OnOperatorExit;
        }

        public IEnumerator Serve(Client client)
        {
            while (!_active)
            {
                yield return null;
            }

            var box = Instantiate(_itemBoxPrefab, _boxSpawnPoint.position, Quaternion.identity, _boxSpawnPoint);

            yield return _boxOpenDelayWfs;

            box.Open();

            yield return _packItemsDelayWfs;

            _cashValues.Clear();

            while (!client.Storage.IsEmpty)
            {
                var clientItem = client.Storage.GiveItem();

                box.Add(clientItem);
                _cashValues.Add(clientItem.Cost);

                yield return _packItemsIntervalWfs;
            }

            box.Close();

            foreach (var cashValue in _cashValues)
            {
                _cashFactory.Create(cashValue);
            }

            yield return _boxCloseDelayWfs;

            client.Takes(box);

            yield return _boxCloseDelayWfs;
        }

        private void OnOperatorExit(Collider obj)
        {
            _active = false;
        }

        private void OnOperatorEntered(Collider obj)
        {
            _active = true;
        }
    }
}