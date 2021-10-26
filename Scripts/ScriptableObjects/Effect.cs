using System;
using System.Collections;
using Controllers;
using Managers;
using Objects;
using UnityEngine;

namespace ScriptableObjects
{
    [Serializable]
    public abstract class Effect : ScriptableObject
    {
        public Sprite spellIcon; 
        protected Spell Spell;
        protected SpellType Type;
        protected Agent SpellCasterAgent;
        protected Vector3 CastPosition;
        public float cooldown;
        public float castDuration;
        public float spellForce;
        
        public Alignment alignment;
        public Effect secondaryEffect;
        public GameObject perfectUIPrefab;
        private GameObject perfectUIInstance;
        public string perfectText;
        public Color PerfectTextColor;
        public int perfectCount;
        public abstract void Cast(Spell spell, SpellType type, Agent agent, Vector3 castPosition);
        public abstract IEnumerator SpellEffect();

        public virtual void DestroyEffect()
        {
            Spell.DestroyEffect(this);
        }

        public void OnPerfect(Vector3 position)
        {
            if(perfectUIInstance == null)
                perfectUIInstance = Instantiate(perfectUIPrefab, position, Quaternion.identity, LevelManager.Instance.currentLevel.transform);
            perfectUIInstance.GetComponent<PerfectUIController>().Init(perfectText, PerfectTextColor);
            perfectUIInstance.transform.position = position;
        }
    }
}