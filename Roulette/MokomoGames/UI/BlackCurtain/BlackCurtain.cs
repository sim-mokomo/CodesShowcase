using DG.Tweening;
using MokomoGames.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace MokomoGames.UI.BlackCurtain
{
    public class BlackCurtain : MonoBehaviour
    {
        [SerializeField] private Image background;
        private Color defaultColor;

        private void Awake()
        {
            defaultColor = background.color;
        }

        public void Show(bool show,float duration)
        {
            var distColor = show ? 
                defaultColor : 
                defaultColor.GetInvisibleColor();
            if (duration > 0f)
            {
                background.DOColor(distColor, duration);
            }
            else
            {
                background.color = distColor;
            }
        }
    }
}