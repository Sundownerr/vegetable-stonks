using System;
using UnityEngine;

namespace Game
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] private int _startCash;
        [SerializeField] private int _cash;

        public int Cash => _cash;

        private void Start()
        {
            AddCash(_startCash);
        }

        public event Action<int> CashChanged;

        public void AddCash(int value)
        {
            _cash += value;
            CashChanged?.Invoke(_cash);
        }

        public void RemoveCash(int value)
        {
            _cash -= value;
            CashChanged?.Invoke(_cash);
        }
    }
}