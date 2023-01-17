using System;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class ItemMoverToPoints : MonoBehaviour
    {
        [SerializeField] private float _moveDuration;
        [SerializeField] private Points _points;

        public void Move(Transform target)
        {
            target.DOKill();
            var itemPoint = _points.Next();
            target.SetParent(itemPoint);

            target.DOLocalJump(Vector3.zero, 2, 1, _moveDuration);
            target.DOLocalRotateQuaternion(Quaternion.identity, _moveDuration);
        }

        public void Move(Transform target, Action onComplete)
        {
            Move(target);
            DOVirtual.DelayedCall(_moveDuration, () => onComplete?.Invoke());
        }
    }
}