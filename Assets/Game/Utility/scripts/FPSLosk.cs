using UnityEngine;

namespace Game
{
    public class FPSLosk : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}