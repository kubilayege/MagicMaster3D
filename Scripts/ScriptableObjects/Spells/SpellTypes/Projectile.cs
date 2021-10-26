using System.Collections.Generic;
using Objects;
using UnityEngine;
using Utils;

namespace ScriptableObjects.Spells.SpellTypes
{
    [CreateAssetMenu(menuName = "Spell Type/Projectile")]
    public class Projectile : SpellType
    {
        public float range;
        public float width;
        public Color indicatorColor;
        public override GameObject ToggleGhostIndicator(GameObject indicatorInstance, bool value)
        {
            var tempIndicator = indicatorInstance;
            if (indicatorInstance == null)
            {
                tempIndicator = Instantiate(indicatorPrefab, Player.Instance.Parent.position, Quaternion.identity, Player.Instance.Parent);
                tempIndicator.transform.localScale = (Vector3.one * range).WithY(1f).WithX(width);
                tempIndicator.GetComponentInChildren<SpriteRenderer>().color = indicatorColor;
                
                tempIndicator.SetActive(value);
                return tempIndicator;
            }
            
            indicatorInstance.SetActive(value);
            return indicatorInstance;
        }

        public override void MoveGhostIndicator(GameObject indicatorInstance, Vector2 mousePoint)
        {
            var pointPosition = SpellHelper.GetWorldPos(mousePoint).WithY(0f);
            var playerPos = Player.Instance.Parent.position.WithY(0f);
            indicatorInstance.transform.forward = (pointPosition - playerPos).normalized.WithY(0f);
        }

        public override float GetEffectArea()
        {
            return width * range;
        }

        public override List<Enemy> GetEnemies(Vector3 atPos)
        {
            Collider[] hitColliders = new Collider[20];
            var i =Physics.OverlapSphereNonAlloc(atPos, width, hitColliders);

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