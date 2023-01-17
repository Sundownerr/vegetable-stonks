using UnityEngine;

namespace Game
{
    public class Tag : ScriptableObject
    {
        [SerializeField] private string _value;

        [ContextMenu("Set Value As Asset Name")]
        private void SetValueAsAssetName()
        {
            _value = name;
        }

        public bool Is(Tag other)
        {
            return other == this || other._value == _value;
        }
    }
}