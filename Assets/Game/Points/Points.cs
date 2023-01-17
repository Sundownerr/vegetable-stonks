using UnityEngine;

namespace Game
{
    public class Points : MonoBehaviour
    {
        [SerializeField] private Transform[] _all;

        private int _pointIndex;

        public Transform Next()
        {
            var point = _all[_pointIndex];
            _pointIndex = (_pointIndex + 1) % _all.Length;
            // _pointIndex++;
            return point;
        }

        public void Release()
        {
            if (_pointIndex == 0)
            {
                return;
            }

            _pointIndex--;
        }
    }
}