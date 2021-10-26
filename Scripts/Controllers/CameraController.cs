using System;
using System.Collections;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Controllers;
using Managers;
using ScriptableObjects;
using ScriptableObjects.Level;
using UnityEngine;
using Utils;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private SplineController splineController;
        [SerializeField] private float startPosOnSpline;
        private LevelData _currentLevel;
        private PlayerData _playerData;

        private void Awake()
        {
            SetActionMethods();
        }

        public void InitCamera(LevelData currentLevelData, PlayerData playerData)
        {
            _currentLevel = currentLevelData;
            _playerData = playerData;
            splineController.Spline = _currentLevel.Spline;
            splineController.Speed = 0;
            StartCoroutine(nameof(InitPos));
        }

        private IEnumerator InitPos()
        {
            yield return Wait.FixedUpdate;
            splineController.PositionMode = CurvyPositionMode.WorldUnits;
            splineController.AbsolutePosition = startPosOnSpline;
        }
        
        public void SetActionMethods()
        {
            ActionManager.Instance.AddAction(ActionIDHolder.OnLevelStartedID, StartMoving);
            ActionManager.Instance.AddAction(ActionIDHolder.OnLevelCompletedID, StopMoving);
            ActionManager.Instance.AddAction(ActionIDHolder.OnLevelFailedID, StopMoving);
        }

        public void StopMoving()
        {
            splineController.Speed = 0f;
        }

        public void StartMoving()
        {
            splineController.Speed = _playerData.playerForwardSpeed;
            
        }
    }
}