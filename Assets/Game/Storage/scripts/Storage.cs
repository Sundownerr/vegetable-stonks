using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Storage : MonoBehaviour
    {
        public enum State
        {
            Empty, HaveItems, Full,
        }

        [SerializeField] private int _maxItems;

        private readonly Stack<Item> _items = new();

        public State<State> CurrentState { get; } = new(State.Empty);

        public int TotalItems { get; private set; }

        public bool IsEmpty => CurrentState.Value == State.Empty;
        public bool IsFull => CurrentState.Value == State.Full;
        public event Action<Item> Added;
        public event Action<Item> ItemTaken;

        public void Add(Item item)
        {
            _items.Push(item);

            TotalItems++;
            UpdateState();

            Added?.Invoke(item);
        }

        private void UpdateState()
        {
            if (TotalItems >= _maxItems)
            {
                CurrentState.ChangeTo(State.Full);
                return;
            }

            if (TotalItems <= 0)
            {
                CurrentState.ChangeTo(State.Empty);
                return;
            }

            if (CurrentState.Value != State.HaveItems)
            {
                CurrentState.ChangeTo(State.HaveItems);
            }
        }

        public Item GiveItem()
        {
            var item = _items.Pop();

            TotalItems--;
            UpdateState();

            ItemTaken?.Invoke(item);

            return item;
        }
    }
}