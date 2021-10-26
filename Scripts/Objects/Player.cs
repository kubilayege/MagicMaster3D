using System;
using Controllers;
using Core;
using Managers;
using ScriptableObjects;
using UnityEngine;

namespace Objects
{
    public class Player : Agent, IActionListener
    {
        public static Player Instance;
        
        public PlayerData playerData;

        private void Awake()
        {
            Instance = this;
            SetActionMethods();
        }


        public void SetActionMethods()
        {
            ActionManager.Instance.AddAction(ActionIDHolder.OnLevelPreparedID, PreparePlayer);
        }

        private void PreparePlayer()
        {
            _currentHealth = playerData.Health;
            
            HpBarController.Init(_currentHealth);
        }

        public override void StopMoving()
        {
            PlayerController.Instance.StopMoving();
        }
        
        public override void StartMoving()
        {
            PlayerController.Instance.StartMoving();
        }

        public override void Damage(float value)
        {
            if (_currentHealth > 0 && value > 0)
            {
                GetHitEffect();
                
                HpBarController.Decrease(value);
            }

            _currentHealth -= value;
            
            if (_currentHealth < 0)
                _currentHealth = 0;

            // PlayAnimation(AgentGetHit, true);
            
            if (_currentHealth == 0)
            {
                GameManager.Instance.Lose();
            }
        }
    }
}