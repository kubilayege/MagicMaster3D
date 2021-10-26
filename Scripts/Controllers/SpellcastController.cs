using System.Collections;
using System.Linq;
using DG.Tweening;
using Objects;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Controllers
{
    public class SpellcastController : MonoBehaviour
    {
        public Spell currentSpell;
        public Agent agent;
        public Vector2 offset;
        private bool isDragged;

        [SerializeField] private bool stopOnCasting;
        
        public void OnDragStart(Vector2 point, PointerEventData pEventData)
        {
            // Debug.Log(pEventData.pointerPress);
            if(pEventData.pointerCurrentRaycast.gameObject.transform.parent.parent.TryGetComponent(out currentSpell))
            {
                if (!currentSpell.TryToCast())
                    currentSpell = null;
                else
                {
                    if (stopOnCasting)
                        agent.StopMoving();
                }
            }
        }
        
        public void OnDrag(Vector2 point, PointerEventData pEventData)
        {
            if (currentSpell == null) return;

            isDragged = true;
            agent.PlayAnimation(agent.PlayerAnimIdleCasting, true);
            if(!stopOnCasting)
                Helper.DoTimeScaleManipulation(0.2f,0.3f);
            
            var worldPos = SpellHelper.GetWorldPos(pEventData.position + offset);

            currentSpell.ToggleSpellGhost(worldPos != Vector3.zero);

            currentSpell.SpellMove(pEventData.position + offset);
        }

        public void OnDragEnd(Vector2 point, PointerEventData pEventData)
        {
            if(currentSpell == null)
                return;
            var worldPoint = SpellHelper.GetWorldPos(pEventData.position + offset);
            
            if(!stopOnCasting)
                Helper.DoTimeScaleManipulation(1f,0.3f);

            if (worldPoint == Vector3.zero)
            {
                currentSpell.CancelCasting();
                agent.StartMoving();
            }
            else
            {
                if (!isDragged)
                {
                    Debug.Log("AUTOSPELL");
                    worldPoint = GetCastPoint();
                    agent.StartMoving();
                }
                else
                {
                    if (stopOnCasting)
                    {
                        StopCoroutine(nameof(CastSpellAnimation));
                        StartCoroutine(nameof(CastSpellAnimation));
                    }
                }
                currentSpell.Cast(agent,worldPoint);
            }

            
            currentSpell = null;
            isDragged = false;
        }

        private IEnumerator CastSpellAnimation()
        {
            yield return Wait.ForSeconds(1f);
            agent.StartMoving();
        }

        private Vector3 GetCastPoint()
        {
            var agentForward = agent.Parent.forward;
            Vector3 finalPosition = PlayerController.Instance.GetNextSplinePos(Player.Instance.Parent.position + (agentForward * 15f));
            var count = 0;
            for (int i = 10; i < 80; i++)
            {
                var position = Player.Instance.Parent.position + (agentForward * i);
                position = PlayerController.Instance.GetNextSplinePos(position);
                Ray ray =new Ray(position + Vector3.up, Vector3.down);

                if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, PlayerController.Instance.ground))
                {
                    var liveEnemyCount = currentSpell.spellType.GetEnemies(hit.point)
                        .Count(enemy => enemy._currentHealth> 0);
                    if (liveEnemyCount > count)
                    {
                        finalPosition = position;
                        count = liveEnemyCount;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return finalPosition;
        }
    }
}