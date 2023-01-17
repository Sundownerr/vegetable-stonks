using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class TriggerAreaVFX : MonoBehaviour
    {
        [SerializeField] private Vector3 _enterScale;
        [SerializeField] private Vector3 _exitScale;
        [SerializeField] private float _scaleDuration;
        [SerializeField] private Transform _vfx;
        [SerializeField] private TriggerObject _trigger;

        private void Start()
        {
            _trigger.Entered += TriggerOnEntered;
            _trigger.Exit += TriggerOnExit;
        }

        private void TriggerOnExit(Collider obj)
        {
            _vfx.DOKill(true);
            _vfx.DOScale(_exitScale, _scaleDuration);
        }

        private void TriggerOnEntered(Collider obj)
        {
            _vfx.DOKill(true);
            _vfx.DOScale(_enterScale, _scaleDuration);
        }
    }
}