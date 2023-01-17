using System;
using UnityEngine;

namespace Game
{
    public class ItemFactoryAnimator : MonoBehaviour
    {
        [SerializeField] private string _creationParameter;
        [SerializeField] private string _idleParameter;
        [SerializeField] private string _completedParameter;
        [SerializeField] private ItemFactory _itemFactory;
        [SerializeField] private Animator _animator;

        private void Start()
        {
            _itemFactory.CurrentState.Changed += OnFactoryStateChanged;
        }

        private void OnDestroy()
        {
            _itemFactory.CurrentState.Changed -= OnFactoryStateChanged;
        }

        private void OnFactoryStateChanged(ItemFactory.State state)
        {
            switch (state)
            {
                case ItemFactory.State.Idle:
                    _animator.SetBool(_idleParameter, true);
                    _animator.SetBool(_creationParameter, false);
                    _animator.SetBool(_completedParameter, false);
                    break;

                case ItemFactory.State.CreatingItems:
                    _animator.SetBool(_idleParameter, false);
                    _animator.SetBool(_creationParameter, true);
                    _animator.SetBool(_completedParameter, false);
                    break;

                case ItemFactory.State.Completed:
                    _animator.SetBool(_idleParameter, false);
                    _animator.SetBool(_creationParameter, false);
                    _animator.SetBool(_completedParameter, true);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}