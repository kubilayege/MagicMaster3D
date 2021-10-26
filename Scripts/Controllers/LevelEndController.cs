using System;
using Core;
using Managers;
using Objects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class LevelEndController : MonoBehaviour, IActionListener
    {
        public GameObject[] playerParticles;
        public GameObject endPlatformFlare;
        
        private void Awake()
        {
            SetActionMethods();
        }

        public void SetActionMethods()
        {
            ActionManager.Instance.AddAction(ActionIDHolder.OnLevelCompletedID, PlayEndEffects);
            ActionManager.Instance.AddAction(ActionIDHolder.OnLevelFailedID, RemoveLevelEnd);
        }

        private void PlayEndEffects()
        {
            Instantiate(endPlatformFlare, transform.position, endPlatformFlare.transform.rotation, LevelManager.Instance.currentLevel.transform);
            foreach (var particle in playerParticles)
            {
                Instantiate(particle, Player.Instance.Parent.position, particle.transform.rotation, LevelManager.Instance.currentLevel.transform);
            }
            ActionManager.Instance.RemoveListener(ActionIDHolder.OnLevelCompletedID, PlayEndEffects);
            ActionManager.Instance.RemoveListener(ActionIDHolder.OnLevelFailedID, RemoveLevelEnd);
        }

        private void RemoveLevelEnd()
        {
            ActionManager.Instance.RemoveListener(ActionIDHolder.OnLevelCompletedID, PlayEndEffects);
            ActionManager.Instance.RemoveListener(ActionIDHolder.OnLevelFailedID, RemoveLevelEnd);
        }
    }
}