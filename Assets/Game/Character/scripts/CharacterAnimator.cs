using System;
using UnityEngine;

namespace Game
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private string _moving;
        [SerializeField] private string _idle;
        [SerializeField] private Mover _mover;
        [SerializeField] private Animator _animator;

        private int _idleBool;
        private int _movingBool;

        private void Awake()
        {
            _idleBool = Animator.StringToHash(_idle);
            _movingBool = Animator.StringToHash(_moving);

            _mover.CurrentState.Changed += OnPlayerMoverStateChanged;
        }

        private void OnDestroy()
        {
            _mover.CurrentState.Changed -= OnPlayerMoverStateChanged;
        }

        private void OnPlayerMoverStateChanged(Mover.State state)
        {
            switch (state)
            {
                case Mover.State.Idle:
                    _animator.SetBool(_idleBool, true);
                    _animator.SetBool(_movingBool, false);
                    break;

                case Mover.State.Moving:
                    _animator.SetBool(_idleBool, false);
                    _animator.SetBool(_movingBool, true);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}