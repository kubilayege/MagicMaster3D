using System.Collections;
using UnityEngine;
using Utils;

namespace Objects
{
    public class CoinMover : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float coinSpeed;
        
        public void StartMoving(Transform _target)
        {
            if (target != null)
                return;
            target = _target;

            StartCoroutine(nameof(MoveCoroutine));
        }

        IEnumerator MoveCoroutine()
        {
            while (target.position.Distance(transform.position) > 0.5f)
            {
                transform.position += (target.position - transform.position).normalized * Time.deltaTime * coinSpeed;
                yield return Wait.FixedUpdate;
            }
        }
    }
}