using System;
using Objects;
using UnityEngine;

namespace Controllers
{
    public class CoinPickupController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Coin"))
            {
                if(other.gameObject.TryGetComponent(out Coin coin))
                {
                    coin.Move(transform);
                }
            }
        }
    }
}