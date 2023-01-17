using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Client : MonoBehaviour
    {
        [SerializeField] private float _takeInterval;
        [SerializeField] private Storage _storage;
        [SerializeField] private ClientTaskView _taskView;
        [SerializeField] private ItemMoverToPoints _itemMover;
        [SerializeField] private NavMeshAgentMover _navMeshAgentMover;
        [SerializeField] private TriggerObject _itemStorageTrigger;
        private readonly List<StorageTask> _storageTasks = new();
        public CashRegisterPOI CashRegisterPOI { get; set; }
        public Transform EndPoint { get; set; }
        public Storage Storage => _storage;
        public event Action Completed;

        public void AddStorageTask(StorageTask task)
        {
            _storageTasks.Add(task);
        }

        public void Activate()
        {
            StartCoroutine(CompleteTasks());
        }

        private IEnumerator CompleteTasks()
        {
            foreach (var storageTask in _storageTasks)
            {
                yield return Complete(storageTask);
            }

            yield return Complete(CashRegisterPOI);
            yield return TravelTo(EndPoint);

            Completed?.Invoke();
        }

        private IEnumerator Complete(CashRegisterPOI poi)
        {
            _taskView.ShowCashRegisterTask(poi.Sprite);

            yield return TravelTo(poi.CashRegister.ClientPoint);
            poi.Queue.Add(this);
            yield return WaitFor(poi.Queue);
            yield return CashRegisterPOI.CashRegister.Serve(this);

            _taskView.SetActive(false);

            yield return new WaitForSeconds(0.2f);

            poi.Queue.Remove();
        }

        private IEnumerator Complete(StorageTask task)
        {
            _taskView.ShowStorageTask(task.POI.Sprite, task.ItemsAmount);

            yield return TravelTo(task.POI.Storage);
            task.POI.Queue.Add(this);
            yield return WaitFor(task.POI.Queue);
            yield return TakeItems(task.POI.Storage, task.ItemsAmount);

            task.POI.Queue.Remove();
        }

        private IEnumerator TravelTo(Storage storage)
        {
            _navMeshAgentMover.MoveTo(storage.transform);
            _itemStorageTrigger.Entered += ItemStorageTriggerOnEntered;

            var reachedStorage = false;

            void ItemStorageTriggerOnEntered(Collider obj)
            {
                if (obj.GetComponent<StorageReference>().Value == storage)
                {
                    reachedStorage = true;
                    _navMeshAgentMover.StopMoving();
                    _itemStorageTrigger.Entered -= ItemStorageTriggerOnEntered;
                }
            }

            while (!reachedStorage)
            {
                yield return null;
            }
        }

        private IEnumerator TravelTo(Transform target)
        {
            _navMeshAgentMover.MoveTo(target);

            while (!_navMeshAgentMover.ReachedDestination)
            {
                yield return null;
            }
        }

        private IEnumerator WaitFor(ClientQueue queue)
        {
            while (queue.Next() != this)
            {
                yield return null;
            }
        }

        private IEnumerator TakeItems(Storage storage, int amount)
        {
            var takenAmount = 0;

            while (takenAmount < amount)
            {
                if (!storage.IsEmpty)
                {
                    var item = storage.GiveItem();

                    _itemMover.Move(item.transform);
                    _storage.Add(item);

                    takenAmount++;

                    _taskView.SetStorageTaskAmount(amount - takenAmount);
                }

                yield return new WaitForSeconds(_takeInterval);
            }
        }

        public void Takes(Item item)
        {
            _storage.Add(item);
            _itemMover.Move(item.transform);
        }
    }
}