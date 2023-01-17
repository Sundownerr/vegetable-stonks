using UnityEngine;

namespace Game
{
    public class FactoryStorageListener : MonoBehaviour
    {
        [SerializeField] private Storage _storage;
        [SerializeField] private ItemCreationProcess[] _itemCreationProcesses;

        private void Start()
        {
            _storage.ItemTaken += OnStorageItemTaken;
        }

        private void OnDestroy()
        {
            _storage.ItemTaken -= OnStorageItemTaken;
        }

        private void OnStorageItemTaken(Item item)
        {
            foreach (var itemCreationProcess in _itemCreationProcesses)
            {
                if (item == itemCreationProcess.Result)
                {
                    itemCreationProcess.ResetProgress();
                }
            }
        }
    }
}