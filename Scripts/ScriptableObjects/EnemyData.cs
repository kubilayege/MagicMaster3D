using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Enemy/Data")]
    public class EnemyData : ScriptableObject
    {
        public Mesh modelMesh; 
        
        [SerializeField] private float health;
        public float Health => health;

        [SerializeField] private float moveSpeed;
        public float MoveSpeed => moveSpeed;
        
        [SerializeField] private float agroRange;

        public float AgroRange => agroRange;

        [SerializeField] private float attackRange;

        public float AttackRange => attackRange;
        
        [SerializeField] private float attackSpeed;
        public float AttackSpeed => attackSpeed;
        
        [SerializeField] private float attackDamage;
        public float AttackDamage => attackDamage;
    }
}