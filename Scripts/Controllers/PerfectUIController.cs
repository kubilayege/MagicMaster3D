using DG.Tweening;
using Objects;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class PerfectUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI perfectText;
        [SerializeField] private float moveYDistance;
        [SerializeField] private float moveDuration;
        [SerializeField] private float fadeDuration;
        

        public void Init(string text, Color perfectTextColor)
        {
            DOTween.Kill(perfectText.GetInstanceID());
            perfectText.gameObject.SetActive(true);
            perfectText.text = text;
            perfectText.color = perfectTextColor;
            perfectText.transform.DOLocalMoveY(moveYDistance, moveDuration).OnUpdate(() =>
            {
                perfectText.transform.forward = (Camera.main.transform.forward);
            }).OnComplete(() =>
            {
                perfectText.DOFade(0f, fadeDuration).OnComplete((() =>
                {
                    perfectText.DOFade(1f, 0f);
                    perfectText.gameObject.SetActive(false);
                    perfectText.transform.DOLocalMoveY(0f, 0f);
                })).OnUpdate((() =>
                {
                    perfectText.transform.forward = (Camera.main.transform.forward);
                })).OnKill((() =>
                {
                    perfectText.DOFade(1f, 0f);
                    perfectText.transform.DOLocalMoveY(0f, 0f);
                })).SetId(perfectText.GetInstanceID());
                
            });
            
        }
    }
}