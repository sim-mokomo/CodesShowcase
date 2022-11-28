using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using MokomoGames.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace MokomoGames.UI.Animation
{
    public class SlideInOutAnimation : MonoBehaviour
    {
        public enum Direction
        {
            DOWN_TO_UP,
            UP_TO_DOWN,
            RIGHT_TO_LEFT,
            LEFT_TO_RIGHT
        }

        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Direction direction;
        private List<Graphic> _graphics;
        private List<Color> _originColors;

        private Vector2 _originPos;

        public bool IsPlaying { get; private set; }

        private void Awake()
        {
            _originPos = rectTransform.anchoredPosition;
            _graphics = GetComponentsInChildren<Graphic>().ToList();
            _originColors = _graphics.Select(x => x.color).ToList();
        }

        public event Action OnComplete;

        public void Show(bool show, float duration, Action onComplete = null)
        {
            IsPlaying = true;
            OnComplete = null;
            OnComplete += onComplete;
            PlayColorAnimation(show, duration);
            PlayMoveAnimation(show, duration, direction);
        }

        private void PlayColorAnimation(bool show, float duration)
        {
            for (var i = 0; i < _graphics.Count; i++)
            {
                var graphic = _graphics[i];
                var distColor = show ? _originColors[i] : graphic.color.GetInvisibleColor();
                // Tweenはduration0でも1フレーム遅れるので
                if (duration <= 0f)
                {
                    graphic.color = distColor;
                    continue;
                }

                graphic.DOColor(distColor, duration);
            }
        }

        private void PlayMoveAnimation(bool show, float duration, Direction dir)
        {
            // Tweenはduration0でも1フレーム遅れるので
            if (duration <= 0f)
            {
                IsPlaying = false;
                rectTransform.anchoredPosition = show ? _originPos : GetSlideOutPosition(dir);
                return;
            }

            var distPos = show ? _originPos : GetSlideOutPosition(dir);
            rectTransform
                .DOAnchorPos(distPos, duration)
                .OnComplete(() =>
                {
                    IsPlaying = false;
                    OnComplete?.Invoke();
                });
        }

        private Vector2 GetSlideOutPosition(Direction dir)
        {
            var dist = Vector2.zero;
            switch (dir)
            {
                case Direction.DOWN_TO_UP:
                    dist = new Vector2(_originPos.x, _originPos.y - rectTransform.rect.height);
                    break;
                case Direction.UP_TO_DOWN:
                    dist = new Vector2(_originPos.x, _originPos.y + rectTransform.rect.height);
                    break;
                case Direction.RIGHT_TO_LEFT:
                    dist = new Vector2(_originPos.x + rectTransform.rect.width, _originPos.y);
                    break;
                case Direction.LEFT_TO_RIGHT:
                    dist = new Vector2(_originPos.x - rectTransform.rect.width, _originPos.y);
                    break;
            }

            return dist;
        }
    }
}