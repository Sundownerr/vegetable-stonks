using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class PlayerMover : Mover
    {
        [SerializeField] private float _moveForce;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private JoystickInput _joystickInput;
        private Vector3 _smoothDirection;

        private void Start()
        {
            _joystickInput.Pressed += JoystickInputOnPressed;
            _joystickInput.Released += JoystickInputOnReleased;
        }

        private void Update()
        {
            if (!_joystickInput.IsHolding)
            {
                return;
            }

            switch (CurrentState.Value)
            {
                case State.Idle:
                    break;

                case State.Moving:
                    UpdateSmoothDirection();
                    Move();
                    Rotate(_agent.transform.position + _smoothDirection);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnDestroy()
        {
            _joystickInput.Pressed -= JoystickInputOnPressed;
            _joystickInput.Released -= JoystickInputOnReleased;
        }

        private void Move()
        {
            var moveForce = _joystickInput.HoldDirectionNormalized.sqrMagnitude * _moveForce;
            _agent.Move(_agent.transform.forward * (Time.deltaTime * moveForce));
        }

        private void UpdateSmoothDirection()
        {
            var direction = Vector3.zero;

            direction.x = _joystickInput.Delta.x;
            direction.z = _joystickInput.Delta.y;

            _smoothDirection = Vector3.Lerp(_smoothDirection, direction, Time.deltaTime * 5f);
        }

        private void JoystickInputOnReleased()
        {
            CurrentState.ChangeTo(State.Idle);
        }

        private void JoystickInputOnPressed()
        {
            CurrentState.ChangeTo(State.Moving);
        }

        private void Rotate(Vector3 nextPosition)
        {
            var forward = nextPosition - _agent.transform.position;

            if (forward == Vector3.zero)
            {
                return;
            }

            _agent.transform.rotation = Quaternion.Slerp(
                _agent.transform.rotation,
                Quaternion.LookRotation(forward),
                Time.deltaTime * 14f);
        }
    }
}