using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MokomoGames.UI.Animation
{
    public class DrawerAnimationContainer : MonoBehaviour
    {
        private List<DrawerAnimator> _items;

        private void Awake()
        {
            _items = GetComponentsInChildren<DrawerAnimator>().ToList();
        }

        public void Draw(bool open, float animationDuration, float animationSpan, bool isImmediatery = false)
        {
            if (open)
                StartCoroutine(OpenAnimation(animationDuration, animationSpan, isImmediatery));
            else
                StartCoroutine(CloseAnimation(animationSpan, isImmediatery));
        }

        private IEnumerator OpenAnimation(float animationDuraiton, float animationSpan, bool isImmediatery = false)
        {
            if (isImmediatery)
            {
                foreach (var animator in _items) animator.OpenAnimation(0f, true);
                yield break;
            }

            yield return null;

            foreach (var animationItem in _items)
            {
                animationItem.OpenAnimation(animationDuraiton);
                yield return new WaitForSeconds(animationSpan);
            }
        }

        private IEnumerator CloseAnimation(float animationSpan, bool isImmediatery = false)
        {
            if (isImmediatery)
            {
                foreach (var animator in _items) animator.CloseAnimation(0f, true);
                yield break;
            }

            yield return null;

            foreach (var animationItem in _items)
            {
                animationItem.CloseAnimation(animationSpan);
                yield return new WaitForSeconds(animationSpan);
            }
        }
    }
}