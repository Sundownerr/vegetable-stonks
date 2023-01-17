using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class ClientTaskViewData
    {
        [SerializeField] private GameObject _wrapper;
        [SerializeField] private SpriteRenderer _icon;

        public void SetActive(bool value)
        {
            _wrapper.SetActive(value);
        }

        public void SetSprite(Sprite sprite)
        {
            _icon.sprite = sprite;
        }
    }
}