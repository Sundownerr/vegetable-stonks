using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ItemTransferFromStorage : MonoBehaviour
    {
        [SerializeField] private float _transferInterval = 0.15f;
        [SerializeField] private TriggerObject _storageTrigger;
        [SerializeField] private ItemMoverToPoints _itemMover;
        [SerializeField] private Storage _toStorage;

        private readonly Dictionary<Collider, Coroutine> _collectingRoutines = new();
        private WaitForSeconds _collectIntervalWfs;

        private void Start()
        {
            _collectIntervalWfs = new WaitForSeconds(_transferInterval);

            _storageTrigger.Entered += OnEnteredStorage;
            _storageTrigger.Exit += OnExitStorage;
        }

        private void OnDestroy()
        {
            _storageTrigger.Entered -= OnEnteredStorage;
            _storageTrigger.Exit -= OnExitStorage;
        }

        private void OnEnteredStorage(Collider storageCollider)
        {
            if (_toStorage.IsFull)
            {
                return;
            }

            var fromStorage = storageCollider.GetComponent<StorageReference>().Value;

            if (fromStorage.IsEmpty)
            {
                return;
            }

            _collectingRoutines.Add(storageCollider, StartCoroutine(TakeItems(fromStorage)));
        }

        private void OnExitStorage(Collider storageCollider)
        {
            if (_collectingRoutines.TryGetValue(storageCollider, out var coroutine))
            {
                StopCoroutine(coroutine);
            }

            _collectingRoutines.Remove(storageCollider);
        }

        private IEnumerator TakeItems(Storage fromStorage)
        {
            while (true)
            {
                while (!_toStorage.IsFull && !fromStorage.IsEmpty)
                {
                    var item = fromStorage.GiveItem();

                    _itemMover.Move(item.transform);
                    _toStorage.Add(item);

                    yield return _collectIntervalWfs;
                }

                yield return null;
            }
        }
    }
}