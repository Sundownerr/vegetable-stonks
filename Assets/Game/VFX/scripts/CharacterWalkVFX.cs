using System;
using UnityEngine;

namespace Game
{
    public class CharacterWalkVFX : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _moveParticle;
        [SerializeField] private Mover _playerMover;

        private void Awake()
        {
            _playerMover.CurrentState.Changed += OnMoverStateChanged;
        }

        private void OnDestroy()
        {
            _playerMover.CurrentState.Changed -= OnMoverStateChanged;
        }

        private void OnMoverStateChanged(Mover.State state)
        {
            switch (state)
            {
                case Mover.State.Idle:
                    _moveParticle.Stop(true);
                    break;

                case Mover.State.Moving:
                    _moveParticle.Play(true);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}