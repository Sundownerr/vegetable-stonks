using System;
using UnityEngine;

namespace Game
{
    public class PlayerVFX : MonoBehaviour
    {
        [SerializeField] private GameObject _maxItemsIndicator;
        [SerializeField] private Storage _playerStorage;
        private Camera _camera;

        private void Start()
        {
            _playerStorage.CurrentState.Changed += OnStorageStateChanged;

            _camera = Camera.main;

            _maxItemsIndicator.SetActive(false);
        }

        private void Update()
        {
            if (_maxItemsIndicator.activeSelf)
            {
                _maxItemsIndicator.transform.rotation = Quaternion.LookRotation(_camera.transform.forward);
            }
        }

        private void OnDestroy()
        {
            _playerStorage.CurrentState.Changed -= OnStorageStateChanged;
        }

        private void OnStorageStateChanged(Storage.State state)
        {
            switch (state)
            {
                case Storage.State.Empty:
                    _maxItemsIndicator.SetActive(false);
                    break;

                case Storage.State.HaveItems:
                    _maxItemsIndicator.SetActive(false);
                    break;

                case Storage.State.Full:
                    _maxItemsIndicator.SetActive(true);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}