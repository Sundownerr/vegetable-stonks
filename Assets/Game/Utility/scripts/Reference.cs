using UnityEngine;

namespace Game
{
    public class Reference<T> : MonoBehaviour
    {
        [SerializeField] public T Value;
    }
}