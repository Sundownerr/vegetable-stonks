using System;
using UnityEngine;

namespace Game
{
    public class TriggerObject : MonoBehaviour
    {
        [SerializeField] private TriggerTag _tag;
        [SerializeField] private TriggerTag[] _collideTags;

#if UNITY_EDITOR
        [SerializeField] private bool _debug;
#endif

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<TriggerObject>(out var colliderObject))
            {
                return;
            }

            if (!CollidingWith(colliderObject))
            {
                return;
            }

#if UNITY_EDITOR
            if (_debug)
            {
                Debug.Log($"{name} enter {other.name}", other);
            }
#endif
            Entered?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<TriggerObject>(out var colliderObject))
            {
                return;
            }

            if (!CollidingWith(colliderObject))
            {
                return;
            }
#if UNITY_EDITOR
            if (_debug)
            {
                Debug.Log($"{name} exit {other.name}", other);
            }
#endif
            Exit?.Invoke(other);
        }

        private bool CollidingWith(TriggerObject other)
        {
            for (var i = 0; i < _collideTags.Length; i++)
            {
                if (other._tag.Is(_collideTags[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public event Action<Collider> Entered;
        public event Action<Collider> Exit;
    }
}