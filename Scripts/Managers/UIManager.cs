using Core;
using Objects;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField] private TextMeshProUGUI levelText;
        public TextMeshProUGUI coinText;
        public Transform coinImage;
        
        public TextMeshProUGUI LevelText => levelText;
        
        [SerializeField] private GameObject idleScreen;
        public GameObject IdleScreen => idleScreen;
        [SerializeField] private GameObject playScreen;
        public GameObject PlayScreen => playScreen;
        [SerializeField] private GameObject winScreen;
        public GameObject WinScreen => winScreen;
        
        [SerializeField] private GameObject loseScreen;
        public GameObject LoseScreen => loseScreen;

        public Spell[] spells;
        public RectTransform SpellHolder;

    }
}