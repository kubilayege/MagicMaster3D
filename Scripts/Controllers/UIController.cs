using System;
using Core;
using DG.Tweening;
using Managers;
using ScriptableObjects.Level;
using UnityEngine;
using Utils;

namespace Controllers
{
    public class UIController : MonoSingleton<UIController>, IActionListener
    {
        [Header("Coin Text Animation Settings")]
        [SerializeField] private Vector3 coinAnimationPunchScale;
        [SerializeField] private float coinScaleAnimationDuration;
        [SerializeField] private float coinColorAnimationDuration;
        [SerializeField] private int coinAnimationVibrato;
        [SerializeField] private int coinAnimationElasticity;
        [SerializeField] private Ease ease;
        [SerializeField] private Color animationCoinColor;
        
        private int CoinCount
        {
            get
            {
                if (PlayerPrefs.GetInt("Coin") < 0)
                    PlayerPrefs.SetInt("Coin", 0);
                return PlayerPrefs.GetInt("Coin");
            }
            set => PlayerPrefs.SetInt("Coin", value);
        }
        
        private void Awake()
        {
            SetActionMethods();
        }

        public void SetLevelData(LevelContainer currentLevelContainer, int currentLevelNumber)
        {
            var newRect = new Rect(UIManager.Instance.SpellHolder.rect);
            newRect.width = (currentLevelContainer.levelSpells.Length * 200) + 200f;
            UIManager.Instance.SpellHolder.sizeDelta = new Vector2(newRect.width, newRect.height);
            Debug.Log(newRect.width);
            int i = 0;
            foreach (var spellData in currentLevelContainer.levelSpells)
            {
                UIManager.Instance.spells[i].gameObject.SetActive(true);
                UIManager.Instance.spells[i].Init(spellData.SpellType, spellData.SpellEffect);
                i++;
            }
        }

        public void SetActionMethods()
        {
            ActionManager.Instance.AddAction(ActionIDHolder.OnLevelStartedID, OnLevelStart);
            ActionManager.Instance.AddAction(ActionIDHolder.OnLevelCompletedID, OnLevelComplete);
            ActionManager.Instance.AddAction(ActionIDHolder.OnLevelFailedID, OnLevelFailed);
            ActionManager.Instance.AddAction(ActionIDHolder.OnLevelPreparedID, OnLevelPrepared);
        }

        public void CoinPickup(int coinValue)
        {
            DOTween.Kill(UIManager.Instance.coinText.GetInstanceID(), true);
            DOTween.Kill(UIManager.Instance.coinText.GetInstanceID() + 1, false);
            
            CoinCount += coinValue;
            UIManager.Instance.coinText.text = (CoinCount).ToString();

            UIManager.Instance.coinText.transform
                .DOPunchScale(coinAnimationPunchScale, coinScaleAnimationDuration, coinAnimationVibrato, coinAnimationElasticity)
                .SetEase(ease)
                .SetId(UIManager.Instance.coinText.GetInstanceID());
            
            UIManager.Instance.coinImage.transform
                .DOPunchScale(coinAnimationPunchScale, coinScaleAnimationDuration, coinAnimationVibrato, coinAnimationElasticity)
                .SetEase(ease)
                .SetId(UIManager.Instance.coinText.GetInstanceID());

            UIManager.Instance.coinText
                .DOColor(animationCoinColor, coinColorAnimationDuration)
                .SetEase(ease)
                .SetId(UIManager.Instance.coinText.GetInstanceID() + 1).OnComplete(() =>
                {
                    UIManager.Instance.coinText.color = Color.white;
                });
        }

        private void OnLevelComplete()
        {
            UIManager.Instance.PlayScreen.SetActive(false);
            UIManager.Instance.WinScreen.SetActive(true);
        }

        private void OnLevelFailed()
        {
            UIManager.Instance.PlayScreen.SetActive(false);
            UIManager.Instance.LoseScreen.SetActive(true);
        }

        private void OnLevelStart()
        {
            UIManager.Instance.IdleScreen.SetActive(false);
            UIManager.Instance.PlayScreen.SetActive(true);
        }

        private void OnLevelPrepared()
        {
            UIManager.Instance.IdleScreen.SetActive(true);
            UIManager.Instance.LoseScreen.SetActive(false);
            UIManager.Instance.WinScreen.SetActive(false);
            UIManager.Instance.PlayScreen.SetActive(false);
            if(PlayerPrefs.GetInt("Tutorial") == 0)
                UIManager.Instance.LevelText.text = $"Tutorial";
            else
                UIManager.Instance.LevelText.text = $"Level {LevelManager.Instance.CurrentLevelNumber}";
        }
    }
}