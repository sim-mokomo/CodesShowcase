using DG.Tweening;
using UnityEngine;

namespace MokomoGames.UI.Animation
{
    public class ScaleAnimator : MonoBehaviour
    {
        private Vector3 hiddenScale;
        private Vector3 showScale;

        public void Init(Vector3 hiddenScale, Vector3 showScale)
        {
            this.hiddenScale = hiddenScale;
            this.showScale = showScale;
        }

        public void PlayShowAnimation(bool show, float duration)
        {
            Vector3 GetAnimationScale(bool show)
            {
                return show ? showScale : hiddenScale;
            }

            if (duration <= 0f)
            {
                transform.localScale = GetAnimationScale(show);
                gameObject.SetActive(show);
                return;
            }

            if (gameObject.activeSelf == show) return;

            if (show) gameObject.SetActive(true);

            transform.localScale = GetAnimationScale(!show);
            transform.DOScale(GetAnimationScale(show), duration).onComplete += () =>
            {
                if (show == false) gameObject.SetActive(false);
            };
        }
    }
}