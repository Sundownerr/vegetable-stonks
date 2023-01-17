using System;
using UnityEngine;

namespace Game
{
    public class JoystickInput : MonoBehaviour
    {
        [SerializeField] private float _maxDistance;

        public Vector2 StartPosition { get; set; }
        public Vector2 EndPosition { get; set; }
        public bool Enabled { get; set; }

        public bool IsHolding { get; private set; }
        public Vector2 HoldDirection => EndPosition - StartPosition;
        public Vector2 Delta { get; set; }
        public Vector2 HoldDirectionNormalized => (EndPosition - StartPosition).normalized;

        public event Action Pressed;
        public event Action Released;

        public void Press()
        {
            IsHolding = true;
            Pressed?.Invoke();
        }

        public void Release()
        {
            IsHolding = false;
            Released?.Invoke();
        }
    }
}