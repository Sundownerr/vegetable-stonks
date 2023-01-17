using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ClientSpawner : MonoBehaviour
    {
        [SerializeField] private int _maxClients = 3;
        [SerializeField] private float _spawnInterval = 1f;
        [SerializeField] private Transform _clientSpawnPoint;
        [SerializeField] private Transform _clientEndPoint;
        [SerializeField] private Client[] _clientPrefabs;
        [SerializeField] private CashRegisterPOI _cashRegisterPOI;
        [SerializeField] private List<StoragePOI> _storagePOI;

        private int _activeClients;

        private IEnumerator Start()
        {
            while (true)
            {
                while (_activeClients < _maxClients)
                {
                    var clientPrefab = _clientPrefabs[Random.Range(0, _clientPrefabs.Length)];
                    var client = Instantiate(clientPrefab, _clientSpawnPoint.position, Quaternion.identity);

                    SetUp(client);

                    client.Activate();

                    _activeClients++;

                    yield return new WaitForSeconds(_spawnInterval);
                }

                yield return null;
            }
        }

        public void Add(StoragePOI storagePoi)
        {
            _storagePOI.Add(storagePoi);
        }

        private void SetUp(Client client)
        {
            client.CashRegisterPOI = _cashRegisterPOI;
            client.EndPoint = _clientEndPoint;

            var storageTasksAmount = Random.Range(1, _storagePOI.Count);

            for (var i = 0; i < storageTasksAmount; i++)
            {
                var storagePOI = _storagePOI[Random.Range(0, _storagePOI.Count)];
                var amount = Random.Range(1, 4);

                client.AddStorageTask(new StorageTask {
                    ItemsAmount = amount,
                    POI = storagePOI,
                });
            }

            client.Completed += NewClientOnCompleted;

            void NewClientOnCompleted()
            {
                client.Completed -= NewClientOnCompleted;

                Destroy(client.gameObject);
                _activeClients--;
            }
        }
    }
}