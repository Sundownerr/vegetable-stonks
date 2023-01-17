using System;
using UnityEngine;

namespace Game
{
    
    public class CashRegisterPOI : MonoBehaviour
    {
        [SerializeField] public Sprite Sprite;
        [SerializeField] public CashRegister CashRegister;
        [SerializeField] public ClientQueue Queue;
    }
}