using System.Collections.Generic;
using Managers;
using Objects;
using UnityEngine;
using Utils;

namespace ScriptableObjects.Spells.SpellTypes
{
    [CreateAssetMenu(menuName = "Spell Type/Circle Aoe")]
    public class CircleAoe : SpellType
    {
        public float radius = 0.5f;
        public override GameObject ToggleGhostIndicator(GameObject indicatorInstance, bool value)
        {
            var tempIndicator = indicatorInstance;
            if (indicatorInstance == null)
            {
                tempIndicator = Instantiate(indicatorPrefab, Vector3.zero, Quaternion.identity, LevelManager.Instance.currentLevel.transform);
                tempIndicator.transform.localScale = (Vector3.one * radius * 2).WithY(1f);
                tempIndicator.SetActive(value);
                return tempIndicator;
            }
            
            indicatorInstance.SetActive(value);
            return indicatorInstance;

        }

        public override void MoveGhostIndicator(GameObject indicatorInstance, Vector2 mousePoint)
        {
            Vector3 worldPosition = SpellHelper.GetWorldPos(mousePoint);

            indicatorInstance.transform.position = worldPosition;
        }

        public override float GetEffectArea()
        {
            return radius;
        }

        public override List<Enemy> GetEnemies(Vector3 atPos)
        {
            Collider[] hitColliders = new Collider[20];
            var i =Physics.OverlapSphereNonAlloc(atPos, radius, hitColliders);

            var enemies = new List<Enemy>();
            for (int j = 0; j < i; j++)
            {
                var enemy = hitColliders[j].GetComponent<Enemy>();
                if(enemy != null)
                    enemies.Add(enemy);
            }
            
            return enemies;
        }
    }
}