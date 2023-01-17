using UnityEngine;

namespace Game
{
    public class CurrencyUIPresenter : MonoBehaviour
    {
        [SerializeField] private CurrencyUIView _view;
        [SerializeField] private PlayerData _playerData;

        private void Awake()
        {
            _playerData.CashChanged += PlayerDataOnCashChanged;
        }

        private void PlayerDataOnCashChanged(int value)
        {
            _view.SetCash(value);
        }
    }
}