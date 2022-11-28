using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MokomoGames.UI.CircleGraph
{
    public class CircleGraphViewer : MonoBehaviour
    {
        [SerializeField] private float circleSize;
        [SerializeField] private LayoutElement layout;
        [SerializeField] private Image circleImage;
        [SerializeField] private Image disableCircleImage;
        private readonly List<Image> circlePartsList = new List<Image>();
        
        private void Awake()
        {
            layout.minHeight = circleSize;
            layout.minWidth = circleSize;
            layout.preferredHeight = circleSize;
            layout.preferredWidth = circleSize;
        }

        public void Apply(IReadOnlyList<ST_CircleGraphViewData> datas)
        {
            var prevPercentRate = 0f;

            for (int i = 0; i < datas.Count; i++)
            {
                var data = datas[i];
                Image circleParts = null;
                if (i >= circlePartsList.Count)
                {
                    circleParts = Instantiate(circleImage, Vector3.zero, Quaternion.identity, layout.transform);
                    circlePartsList.Add(circleParts);
                }
                else
                {
                    circleParts = circlePartsList[i];
                }
                circleParts.name = data.Name;
                circleParts.color = data.Color;
                circleParts.fillAmount = data.Rate;
                circleParts.transform.SetAsFirstSibling();
                circleParts.rectTransform.anchoredPosition = Vector2.zero;
                circleParts.rectTransform.localPosition = Vector2.zero;
                circleParts.transform.rotation = Quaternion.Euler(0, 0, -180);
                circleParts.transform.Rotate(new Vector3(0,0,-360 * prevPercentRate));
                prevPercentRate += circleParts.fillAmount;
            }
        }

        public void EnableGraph(bool enable)
        {
            disableCircleImage.gameObject.SetActive(!enable);
        }
    }
}