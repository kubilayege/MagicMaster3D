using System;
using Controllers;
using Core;
using ScriptableObjects;
using ScriptableObjects.Level;
using UnityEngine;

namespace Managers
{
    public enum LevelLoadType
    {
        Previous = -1,
        Restart = 0,
        Next = 1
    }
    public class LevelManager : MonoSingleton<LevelManager> , IActionListener
    {
        [SerializeField] private LevelPool levelPool;
        private GameObject _currentLevelObject;
        
        public GameObject GetCurrentLevel()
        {
            return _currentLevelObject;
        }

        private LevelContainer currentLevelContainer;
        private LevelData currentLevelData;
        private int _currentLevelNumber;
        [HideInInspector]
        public GameObject currentLevel;

        private bool isLastLevelTutorial;
        public int CurrentLevelNumber
        {
            get
            {
                if (PlayerPrefs.GetInt("Level") <= 0)
                    PlayerPrefs.SetInt("Level", 1);
                return PlayerPrefs.GetInt("Level");
            }
            private set => PlayerPrefs.SetInt("Level", value);
        }
        public int CurrentLevelIndex => CurrentLevelNumber - 1;

        private void Awake()
        {
            SetActionMethods();
        }

        public void InitLevel(LevelLoadType levelPassType)
        {
            if (currentLevel != null)
            {
                Destroy(currentLevel);
            }

            if (PlayerPrefs.GetInt("Tutorial") != 0)
            {
                if(!isLastLevelTutorial)
                    CurrentLevelNumber += (int) levelPassType;
                currentLevelContainer = levelPool.levels[CurrentLevelIndex % levelPool.Length];
            }
            else
            {
                currentLevelContainer = levelPool.tutorialLevel;
            }


            currentLevel = Instantiate(currentLevelContainer.levelPrefab);
            currentLevelData = currentLevel.GetComponent<LevelData>();        
            
            
            UIController.Instance.SetLevelData(currentLevelContainer, CurrentLevelNumber);
            
            PlayerController.Instance.InitPlayer(currentLevelData);
            
        }


        public void SetActionMethods()
        {
            ActionManager.Instance.AddAction(ActionIDHolder.OnLevelCompletedID, OnLevelCompleted);
        }

        private void OnLevelCompleted()
        {
            if(PlayerPrefs.GetInt("Tutorial") == 0)
            {
                isLastLevelTutorial = true;
                PlayerPrefs.SetInt("Tutorial", 1);
            }
            else
            {
                isLastLevelTutorial = false;
            }
        }
    }
}