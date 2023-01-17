using TMPro;
using UnityEngine;

namespace Game
{
    public class ClientTaskView : MonoBehaviour
    {
        [SerializeField] private ClientTaskViewData _cashRegisterTaskData;
        [SerializeField] private ClientTaskViewData _storageTaskData;

        [SerializeField] private TMP_Text _amountText;
        private ClientTaskViewData _activeTask;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (gameObject.activeSelf)
            {
                transform.rotation = Quaternion.LookRotation(_camera.transform.forward);
            }
        }

        public void ShowStorageTask(Sprite sprite, int amount)
        {
            SetStorageTaskAmount(amount);

            _storageTaskData.SetSprite(sprite);
            ChangeActiveTask(_storageTaskData);
        }

        public void SetStorageTaskAmount(int amount)
        {
            _amountText.SetText(amount.ToString());
        }

        private void ChangeActiveTask(ClientTaskViewData next)
        {
            if (_activeTask != null)
            {
                _activeTask.SetActive(false);
            }

            _activeTask = next;
            _activeTask.SetActive(true);
        }

        public void ShowCashRegisterTask(Sprite sprite)
        {
            _cashRegisterTaskData.SetSprite(sprite);
            ChangeActiveTask(_cashRegisterTaskData);
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}