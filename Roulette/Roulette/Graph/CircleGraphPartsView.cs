using UnityEngine;
using UnityEngine.UI;

namespace Roulette.Graph
{
    public class CircleGraphPartsView : MonoBehaviour
    {
        [SerializeField] private Image image;

        public void Setup(CircleGraphData data)
        {
            image.name = data.Name;
            image.color = data.Color;
            image.fillAmount = data.Rate;
            image.transform.SetAsLastSibling();
            image.rectTransform.anchoredPosition = Vector2.zero;
            image.rectTransform.localPosition = Vector2.zero;
        }
    }
}