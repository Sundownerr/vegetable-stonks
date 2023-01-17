using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class ItemBox : Item
    {
        [SerializeField] private string _openParameter;
        [SerializeField] private string _closeParameter;
        [SerializeField] private Animator _animator;
        [SerializeField] private ItemMoverToPoints _itemMover;

        private void Awake()
        {
            transform.DOScale(1, 0.2f).From(0);
        }

        public void Open()
        {
            _animator.SetBool(_openParameter, true);
            _animator.SetBool(_closeParameter, false);
        }

        public void Add(Item item)
        {
            _itemMover.Move(item.transform);
        }

        public void Close()
        {
            _animator.SetBool(_openParameter, false);
            _animator.SetBool(_closeParameter, true);
        }
    }
}