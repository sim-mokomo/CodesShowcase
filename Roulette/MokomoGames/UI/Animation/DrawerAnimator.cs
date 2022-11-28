using DG.Tweening;
using UnityEngine;

namespace MokomoGames.UI.Animation
{
    public class DrawerAnimator : MonoBehaviour
    {
        [SerializeField] private bool isRight;
        [SerializeField] private RectTransform _rectTransform;

        public RectTransform RectTransform => _rectTransform;

        public void OpenAnimation(float duration, bool isImiddiatery = false)
        {
            var endValue = RectTransform.rect.width / 2;
            if (isImiddiatery) duration = 0.001f;

            DOTween.Sequence()
                .Append(RectTransform.DOAnchorPosX(endValue, duration))
                .Play();
        }

        public void CloseAnimation(float duration, bool isImiddiatery = false)
        {
            var endValue = isRight ? RectTransform.rect.width * 2 : -RectTransform.rect.width / 2;
            if (isImiddiatery) duration = 0.001f;
            DOTween.Sequence()
                .Append(RectTransform.DOAnchorPosX(endValue, duration))
                .Play();
        }
    }
}