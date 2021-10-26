using System;
using Controllers;
using Managers;
using UnityEngine;

namespace Objects
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private CoinMover coinMover;
        [SerializeField] private int coinValue;

        public void Move(Transform to)
        {
            coinMover.StartMoving(to);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                UIController.Instance.CoinPickup(coinValue);
                Destroy(gameObject);
            }
        }
    }
}