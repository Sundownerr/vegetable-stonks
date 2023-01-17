using System;

namespace Game
{
    public class State<T>
    {
        public State()
        {
            
        }
        
        public State(T initialState)
        {
            Value = initialState;
        }

        public T Value { get; private set; }
        public event Action<T> Changed;

        public void ChangeTo(T next)
        {
            Value = next;
            Changed?.Invoke(Value);
        }
    }
}