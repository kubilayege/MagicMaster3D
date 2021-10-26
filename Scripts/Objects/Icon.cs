using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Objects
{
    public class Icon : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        public RectTransform RectTransform => rectTransform;
        [SerializeField] private Image iconImage;
        [SerializeField] private Image backGroundImage;
        [SerializeField] private float followSpeed = 10f;
        [SerializeField] private Transform Parent;
        private Vector3 _originPoint;
        public Spell _spell;

        private void Awake()
        {
            _originPoint = transform.localPosition;
        }


        public void ResetIcon(Spell spell)
        {
            _spell = spell;
            backGroundImage.sprite = spell.effect.spellIcon;
            iconImage.sprite = spell.effect.spellIcon;
        }

        public void Hide(bool _do)
        {
            iconImage.enabled = !_do;
        }

        public void SpellCasted(float cooldown)
        {
            Parent.localPosition = _originPoint;
            _spell.CooldownUp(true);
            Hide(false);
            
            iconImage.fillAmount = 0f;
            iconImage.DOFillAmount(1f, cooldown).SetEase(Ease.Linear).OnComplete(() =>
            {
                _spell.CooldownUp(false);
                iconImage.raycastTarget = true;
            });
        }

        public void CancelCasting()
        {
            Hide(false);
            Parent.DOLocalMove(_originPoint,0.3f);
            iconImage.raycastTarget = true;
        }
        
        public void Move(Vector3 to)
        {
            Parent.LerpTo(to, followSpeed);
        }


        public void Hold()
        {
            iconImage.raycastTarget = false;
        }
    }
}