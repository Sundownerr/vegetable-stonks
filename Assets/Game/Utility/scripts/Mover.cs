using UnityEngine;

namespace Game
{
    public class Mover : MonoBehaviour
    {
        public enum State { Idle, Moving, }

        public State<State> CurrentState { get; } = new(State.Idle);
    }
}