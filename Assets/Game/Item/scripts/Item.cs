using System;
using UnityEngine;

namespace Game
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private int _cost;
        [SerializeField] public Vector3 Size;

        public int Cost => _cost;
        private void OnDrawGizmosSelected()
        {
            var color = Color.cyan;
            color.a -= 0.7f;

            Gizmos.color = color;
            Gizmos.DrawCube(transform.position, Size);
        }
    }
}