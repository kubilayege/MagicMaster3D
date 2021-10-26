using System;
using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Utils;

namespace Controllers
{
    public class EventController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        #region Properties
        [System.Serializable]
        public class DragStarted : UnityEvent<Vector2, PointerEventData> { }
        [SerializeField]
        public DragStarted dragStarted;

        [System.Serializable]
        public class Dragged : UnityEvent<Vector2, PointerEventData> { }
        [SerializeField] public Dragged dragged;

        [System.Serializable]
        public class DragEnded : UnityEvent<Vector2, PointerEventData> { }
        [SerializeField]
        public DragEnded dragEnded;

        private Vector2 _delta;

        public Vector2 Delta => _delta;

        private bool canInput;
        
        [SerializeField] private float defaultInputDelay;
        private float activeDelay;
        
        #endregion

        #region Functions

        private void OnEnable()
        {
            activeDelay = defaultInputDelay;
            StartCoroutine(nameof(InputDelay));
        }

        private IEnumerator InputDelay()
        {
            canInput = false;
            yield return Wait.ForSeconds(activeDelay);
            canInput = true;
        }
        
        public void OnPointerDown(PointerEventData data)
        {
            if (!canInput)
                return;
            if ((Application.isMobilePlatform && data.pointerId != 0))
                return;
            dragStarted.Invoke(data.position * (1536f / Screen.width), data);
        }

        public void OnDrag(PointerEventData data)
        {
            if (!canInput)
                return;
            if ((Application.isMobilePlatform && data.pointerId != 0))
                return;
            _delta = data.delta * (1536f / Screen.width);
            dragged.Invoke(_delta, data);
        }

        public void OnPointerUp(PointerEventData data)
        {
            if (!canInput)
                return;
            if (Application.isMobilePlatform && data.pointerId != 0)
                return;
            dragEnded.Invoke(_delta, data);
        }
    


        #endregion


    }
}