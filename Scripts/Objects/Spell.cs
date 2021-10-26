using System;
using System.Collections.Generic;
using Managers;
using ScriptableObjects;
using UnityEngine;

namespace Objects
{
    public class Spell : MonoBehaviour
    {
        [SerializeField] public SpellType spellType;
        [SerializeField] private Icon spellIcon;
        [SerializeField] public Effect effect;

        private List<Effect> currentEffectInstances;
        private GameObject _ghostObject;
        private bool _isGhostActive = false;
        private bool _isOnCooldown = false;

        private void Awake()
        {
            ActionManager.Instance.AddAction(ActionIDHolder.OnLevelCompletedID, DeActivateSpell);
            ActionManager.Instance.AddAction(ActionIDHolder.OnLevelFailedID, DeActivateSpell);
        }

        private void DeActivateSpell()
        {
            gameObject.SetActive(false);
            Destroy(_ghostObject);
        }

        public void Init(SpellType _spellType, Effect _effect)
        {
            spellType = _spellType;
            effect = _effect;
            spellIcon.ResetIcon(this);
            currentEffectInstances = new List<Effect>();
        }
        
        public bool TryToCast()
        {
            if (_isOnCooldown)
                return false;

            spellIcon.Hold();
            return true;
        }

        public void ToggleSpellGhost(bool state)
        {
            spellIcon.Hide(state);

            _ghostObject = spellType.ToggleGhostIndicator(_ghostObject, state);
            _isGhostActive = state;
        }
        
        public void SpellMove(Vector3 position)
        {
            spellIcon.Move(position);
            
            if (!_isGhostActive)
                return;
            
            if(_ghostObject != null)
                spellType.MoveGhostIndicator(_ghostObject, position);
        }

        public void CancelCasting()
        {
            spellIcon.CancelCasting();
        }

        public void Cast(Agent agent, Vector3 castPosition)
        {
            var newEffect = Instantiate(effect, transform);
            currentEffectInstances.Add(newEffect);
            newEffect.Cast(this, spellType, agent, castPosition);
            //Check if it can be cast at that location
            //CancelCasting if it can't
            spellIcon.SpellCasted(effect.cooldown);
            ToggleSpellGhost(false);
        }


        public void CooldownUp(bool state)
        {
            _isOnCooldown = state;
        }

        public void DestroyEffect(Effect _effect)
        {
            currentEffectInstances.Remove(_effect);
            DestroyImmediate(_effect, true);
        }
    }
}