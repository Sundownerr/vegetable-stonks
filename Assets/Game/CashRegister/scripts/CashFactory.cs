using UnityEngine;

namespace Game
{
    public class CashFactory : MonoBehaviour
    {
        [SerializeField] private Cash _prefab;
        [SerializeField] private Storage _storage;
        [SerializeField] private Transform _cashSpawnPoint;
        [SerializeField] private ItemMoverToPoints _cashMover;

        public void Create(int cashValue)
        {
            var cash = Instantiate(_prefab, _cashSpawnPoint.position, Quaternion.identity, _cashSpawnPoint);

            cash.Value = cashValue;

            _cashMover.Move(cash.transform);
            _storage.Add(cash.GetComponent<Item>());
        }
    }
}