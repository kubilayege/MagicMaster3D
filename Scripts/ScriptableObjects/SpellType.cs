using System.Collections.Generic;
using Objects;
using UnityEngine;

namespace ScriptableObjects
{
    public abstract class SpellType : ScriptableObject
    {
        [SerializeField] protected GameObject indicatorPrefab;
        
        public abstract GameObject ToggleGhostIndicator(GameObject indicatorInstance,bool value);

        public abstract void MoveGhostIndicator(GameObject indicatorInstance, Vector2 mousePoint);

        public abstract float GetEffectArea();
        
        public abstract List<Enemy> GetEnemies(Vector3 atPos);
    }
}