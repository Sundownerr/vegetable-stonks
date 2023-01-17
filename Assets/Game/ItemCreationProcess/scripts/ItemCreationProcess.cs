using System.Collections;
using UnityEngine;

namespace Game
{
    public class ItemCreationProcess : MonoBehaviour
    {
        public enum State
        {
            Idle, Progress, Completed,
        }

        [SerializeField] private string _creationParameter;
        [SerializeField] private Animator _animator;
        [SerializeField] private Item _prefab;
        [SerializeField] private Transform _createdItemWrapper;
        public bool Completed => CurrentState.Value == State.Completed;
        public Item Result { get; private set; }

        public State<State> CurrentState { get; } = new(State.Idle);

        public void ResetProgress()
        {
            Result = null;
            CurrentState.ChangeTo(State.Idle);
        }

        private void UpdateAnimator(float creationPercent)
        {
            _animator.SetFloat(_creationParameter, creationPercent);
        }
        
        public IEnumerator Launch(float duration)
        {
            UpdateAnimator(0);
            Result = Instantiate(_prefab, _createdItemWrapper);

            yield return null;

            CurrentState.ChangeTo(State.Progress);

            var timeElapsed = 0f;
            var percent = 0f;

            while (percent < 1)
            {
                percent = timeElapsed / duration;
                timeElapsed += Time.deltaTime;

                UpdateAnimator(percent);

                yield return null;
            }

            yield return null;

            CurrentState.ChangeTo(State.Completed);
        }
    }
}