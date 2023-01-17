using UnityEngine;

namespace Game
{
    public class PointReleaser : MonoBehaviour
    {
        [SerializeField] private Points _points;
        [SerializeField] private Storage _storage;

        private void Start()
        {
            _storage.ItemTaken += StorageOnItemTaken;
        }

        private void OnDestroy()
        {
            _storage.ItemTaken -= StorageOnItemTaken;
        }

        private void StorageOnItemTaken(Item obj)
        {
            _points.Release();
        }
    }
}