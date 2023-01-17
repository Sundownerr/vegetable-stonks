using System;
using UnityEngine;

namespace Game
{
    public class PlayerJoystickInput : MonoBehaviour
    {
        [SerializeField] private JoystickInput _joysticInput;

        private void Update()
        {
            if (Input.touchCount == 0)
            {
                return;
            }

            var touch = Input.GetTouch(0);

            _joysticInput.Delta = touch.deltaPosition;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _joysticInput.StartPosition = touch.position;
                    _joysticInput.Press();
                    break;

                case TouchPhase.Moved:
                    _joysticInput.EndPosition = touch.position;
                    break;

                case TouchPhase.Stationary:
                    _joysticInput.EndPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    _joysticInput.Release();
                    break;

                case TouchPhase.Canceled:
                    _joysticInput.Release();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}