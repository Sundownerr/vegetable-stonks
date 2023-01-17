using TMPro;
using UnityEngine;

namespace Game
{
    public class CurrencyUIView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _cashText;

        public void SetCash(int value)
        {
            _cashText.SetText(value.ToString());
        }
    }
}