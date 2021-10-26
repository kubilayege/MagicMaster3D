using Controllers;
using Core;
using DG.Tweening;
using ScriptableObjects;
using UnityEngine;

namespace Objects
{
    public class Agent : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        public Transform Parent => parent;

        [SerializeField] private Rigidbody parentBody;
        
        public Rigidbody ParentBody => parentBody;
        
        [SerializeField] private Transform spellCastOrigin;
        
        public Transform SpellCastOrigin => spellCastOrigin;

        [SerializeField] protected Animator animator;

        public StateMachine stateMachine;
        public HpBarController HpBarController;

        
        
        public readonly int PlayerAnimCastAoe = Animator.StringToHash("CastAoe");
        public readonly int PlayerAnimIdleCasting = Animator.StringToHash("Casting");
        public readonly int PlayerAnimCastProjectile = Animator.StringToHash("CastProjectile");
        public readonly int PlayerAnimRun = Animator.StringToHash("Run");
        public readonly int PlayerAnimIdle = Animator.StringToHash("MageIdle");
        public readonly int AgentGetHit = Animator.StringToHash("GetHit");
        
        public readonly int EnemyAnimIdle = Animator.StringToHash("Idle");
        public readonly int EnemyAnimHit = Animator.StringToHash("Hit");
        public readonly int EnemyAnimRun = Animator.StringToHash("EnemyRun");

        
        public int currentAnim;
        public int lastAnim;
        public float _currentHealth;
        
        [SerializeField] public SkinnedMeshRenderer _meshRenderer;

        protected bool isDead;
        public virtual void Status(Alignment alignment)
        {
            
        }

        public void PlayAnimation(int ID, bool forceAnimation)
        {
            if (!forceAnimation && currentAnim == ID)
                return;

            currentAnim = lastAnim;
            animator.SetBool(ID, true);
            currentAnim = ID;
        }

        public void ResumeAnimation()
        {
            animator.speed = 1;
        }
        public void PauseAnimation()
        {
            animator.speed = 0;
        }
        
        private void LateUpdate()
        {
            if (currentAnim != lastAnim)
            {
                animator.SetBool(currentAnim, false);
                if(lastAnim != 0)
                    animator.SetBool(lastAnim, false);
            }
        }
        
        public void GetHitEffect()
        {
            // _meshRenderer.material.DOColor(Color.red, 0.1f).SetLoops(1, LoopType.Yoyo).SetEase(Ease.Flash).OnComplete(
            //     () =>
            //     {
            //         _meshRenderer.material.color = Color.white;
            //     });
            
        }

        public virtual void StopMoving()
        {
            
        }
        public virtual void StartMoving()
        {
            
        }
        
        public virtual void Damage(float value)
        {
            if (_currentHealth > 0 && value > 0)
            {
                GetHitEffect();
                
                HpBarController.Decrease(value);
            }

            _currentHealth -= value;

            if (_currentHealth < 0)
                _currentHealth = 0;

            PlayAnimation(AgentGetHit, true);
            
            if (_currentHealth == 0)
            {
                stateMachine.Die();
                
                _meshRenderer.material.DOColor(Color.gray, 0.3f).SetEase(Ease.Flash).OnComplete(
                    () =>
                    {
                        _meshRenderer.material.color = Color.gray;
                    });
                
                _meshRenderer.materials[1].DOColor(Color.gray, 0.3f).SetEase(Ease.Flash).OnComplete(
                    () =>
                    {
                        _meshRenderer.materials[1].color = Color.gray;
                    });
            }
        }
    }
}