using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Controllers
{
    public class HpBarController : MonoBehaviour
    {
        [SerializeField] private Transform followTransform;
        [SerializeField] private Transform hpBar; 
        [SerializeField] private Image hpBarImage; 
        [SerializeField] private float punchStr; 
        [SerializeField] private bool alwaysOn;
        [SerializeField] private bool CLOSEALL;
        private Camera mainCamera;
        private float defaultY;
        private float maxHealth;
        private Vector3 defaultScale;
        private Transform canvas;
        

        private void Awake()
        {
            mainCamera = Camera.main;
            canvas = transform;
            defaultY = canvas.position.y;
            defaultScale = hpBar.localScale;
            if(!alwaysOn)
                hpBar.gameObject.SetActive(false);
        }


        public void Init(float maxHp)
        {
            this.maxHealth = maxHp;
            hpBarImage.fillAmount = 1f;
            Debug.Log("INIT");
        }

        private void Update()
        {
            canvas.position = followTransform.position.WithY(defaultY);
            hpBar.forward = mainCamera.transform.forward;
        }

        public void Decrease(float amount)
        {
            if (hpBarImage.fillAmount <= 0.04f)
            {
                if(!alwaysOn)
                    hpBar.gameObject.SetActive(false);
                return;
            }
            
            DOTween.Kill(GetInstanceID(), true);
            var endValue = hpBarImage.fillAmount - (amount / maxHealth);
            hpBar.gameObject.SetActive(!CLOSEALL);
            
            hpBarImage.DOFillAmount(endValue, 0.2f).SetId(GetInstanceID())
                .OnKill(() =>
                {
                    hpBarImage.fillAmount = endValue;
                    if(!alwaysOn)
                        hpBar.gameObject.SetActive(false);
                });

        }
    }
}