using System;
using DG.Tweening;
using System.Collections;
using Core;
using FluffyUnderware.Curvy.Controllers;
using Managers;
using Objects;
using ScriptableObjects;
using ScriptableObjects.Level;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Controllers
{
    public class PlayerController : MonoSingleton<PlayerController>, IActionListener
    {
        [SerializeField] private Player player;
        [SerializeField] public LayerMask ground;
        [SerializeField] private SplineController splineController;

        public UnityAction<CurvySplineMoveEventArgs> splineEndReached;
        [SerializeField] private LevelData currentLevel;
        [SerializeField] private CameraController cameraController;
        private void Awake()
        {
            SetActionMethods();
            
            splineEndReached += OnEndReached;
        }

        public void InitPlayer(LevelData currentLevelData)
        {
            currentLevel = currentLevelData;
            
            splineController.Spline = currentLevel.Spline;
            splineController.Speed = 0;
            splineController.Position = 0;
            player.PlayAnimation(player.PlayerAnimIdle, true);
            
            cameraController.InitCamera(currentLevelData, player.playerData);
        }

        public void SetActionMethods()
        {
            ActionManager.Instance.AddAction(ActionIDHolder.OnLevelStartedID, StartMoving);
            ActionManager.Instance.AddAction(ActionIDHolder.OnLevelCompletedID, StopMoving);
            ActionManager.Instance.AddAction(ActionIDHolder.OnLevelFailedID, StopMoving);
        }

        private void OnEndReached(CurvySplineMoveEventArgs args)
        {
            splineController.OnEndReached.RemoveListener(splineEndReached);
            GameManager.Instance.Win();
        }


        public void StopMoving()
        {
            splineController.Speed = 0;
            
            player.PlayAnimation(player.PlayerAnimIdle, true);
            cameraController.StopMoving();
        }

        public void StartMoving()
        {
            cameraController.StartMoving();
            player.PlayAnimation(player.PlayerAnimRun, true);
            splineController.Speed = player.playerData.playerForwardSpeed;
            splineController.OnEndReached.AddListener(splineEndReached);
        }

        public Vector3 GetNextSplinePos(Vector3 position)
        {
            var tf = splineController.Spline.GetNearestPointTF(position, Space.World);
            var distance = splineController.Spline.TFToDistance(tf);
            return splineController.Spline.InterpolateByDistance(distance, Space.World);
        }
    }
}
