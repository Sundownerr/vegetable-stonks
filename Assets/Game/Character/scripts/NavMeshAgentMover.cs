using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class NavMeshAgentMover : Mover
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _stopDistance = 0.8f;

        private Transform _destinationPoint;
        private bool _isMoving;
        public bool ReachedDestination { get; private set; }

        private void Update()
        {
            if (!_isMoving)
            {
                if (ReachedDestination)
                {
                    ReachedDestination = false;
                }

                return;
            }

            ReachedDestination =
                Vector3.Distance(_agent.transform.position, _destinationPoint.position) <= _stopDistance;

            if (ReachedDestination)
            {
                _isMoving = false;
                CurrentState.ChangeTo(State.Idle);
            }
        }

        public void StopMoving()
        {
            _agent.SetDestination(_agent.transform.position);
            _isMoving = false;
            ReachedDestination = false;
            CurrentState.ChangeTo(State.Idle);
        }

        public void MoveTo(Transform point)
        {
            CurrentState.ChangeTo(State.Moving);
            _agent.SetDestination(point.position);
            _destinationPoint = point;
            _isMoving = true;
        }
    }
}