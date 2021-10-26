using System;
using DG.Tweening;
using UnityEngine;

namespace Controllers
{
    public class TutorialController : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI tutorialText;
        [TextArea]
        public string TutorialString;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                tutorialText.transform.localScale = Vector3.zero;
                tutorialText.transform.DOScale(Vector3.one, 2f).SetEase(Ease.OutElastic);
                tutorialText.text = TutorialString;
            }
        }
    }
}