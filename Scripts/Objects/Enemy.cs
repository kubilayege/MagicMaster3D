using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Core.AgentStates;
using DG.Tweening;
using Managers;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Objects
{
    public class Enemy : Agent, IElementalAlignment
    {
        [SerializeField] private EnemyData enemyData;
        public EnemyData EnemyData => enemyData;
        

        public AlignmentData _currentAlignment;

        [SerializeField] private NavMeshAgent navMeshAgent;
        public NavMeshAgent NavMeshAgent => navMeshAgent;

        public List<Rigidbody> limbs;
        
        [SerializeField] private GameObject defaultMeshObject;
        [SerializeField] private GameObject transformInstance;

        [SerializeField] private GameObject coinPrefab;
        

        private void Start()
        {
            _currentHealth = enemyData.Health;
            
            _meshRenderer.sharedMesh = enemyData.modelMesh;
            
            InitAgent();
            
            PlayAnimation(EnemyAnimIdle, true);
            
            HpBarController.Init(_currentHealth);
        }

        public void InitAgent()
        {
            var stateList = new Dictionary<Type, BaseAgentStates>()
            {
                { typeof(IdleState), new IdleState(this) },
                { typeof(AgroState), new AgroState(this) },
                { typeof(AttackState), new AttackState(this) },
                { typeof(BurnState), new BurnState(this) },
                { typeof(RagdollState), new RagdollState(this) },
                { typeof(FreezeState), new FreezeState(this) },
                { typeof(TransformState), new TransformState(this) }
                
            };
            
            stateMachine.SetStates(stateList);

        }
        
        
        public void TransformInto(GameObject transformPrefab)
        {
            // _meshRenderer.sharedMesh = type;
            // _meshRenderer.material = material;
            defaultMeshObject.SetActive(false);
            transformInstance = Instantiate(transformPrefab, transform);
        }

        public void TransformIntoDefaultModel()
        {
            Destroy(transformInstance);
            defaultMeshObject.SetActive(true);
        }

        public override void Status(Alignment alignment)
        {
            if(_currentHealth > 0)
                _currentAlignment = alignment.Effect(this);
        }

        public void DisableAnimator()
        {
            animator.enabled = false;
        }
        
        public override void Damage(float value)
        {
            base.Damage(value);


            if (_currentHealth == 0 && !isDead)
            {
                var coin = Instantiate(coinPrefab, Parent.position, Quaternion.LookRotation(Player.Instance.Parent.position - Parent.position), LevelManager.Instance.currentLevel.transform);
                isDead = true;
            }
            
        }
    }
}