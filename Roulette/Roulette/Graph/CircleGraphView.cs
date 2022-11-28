using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Roulette.Graph
{
    public class CircleGraphView : MonoBehaviour
    {
        [SerializeField] private float circleSize;
        [SerializeField] private LayoutElement layoutGraph;
        [SerializeField] private LayoutElement layoutText;
        [SerializeField] private CircleGraphPartsView circlePartsPrefab;
        [SerializeField] private CircleGraphText circleGraphTextPrefab;
        [SerializeField] private Image disableCircleImage;
        private readonly List<CircleGraphText> circleGraphTexts = new();
        private readonly List<CircleGraphPartsView> circlePartsList = new();

        private void Awake()
        {
            layoutGraph.minHeight = circleSize;
            layoutGraph.minWidth = circleSize;
            layoutGraph.preferredHeight = circleSize;
            layoutGraph.preferredWidth = circleSize;

            layoutText.minHeight = circleSize;
            layoutText.minWidth = circleSize;
            layoutText.preferredHeight = circleSize;
            layoutText.preferredWidth = circleSize;
        }

        public void CreateGraphs(IReadOnlyList<CircleGraphData> datas)
        {
            foreach (var image in circlePartsList) Destroy(image.gameObject);
            circlePartsList.Clear();

            foreach (var circleGraphText in circleGraphTexts) Destroy(circleGraphText.gameObject);
            circleGraphTexts.Clear();

            var prevPercentRate = 0f;
            foreach (var data in datas)
            {
                var circleParts = Instantiate(circlePartsPrefab, Vector3.zero, Quaternion.identity,
                    layoutGraph.transform);
                circlePartsList.Add(circleParts);
                circleParts.transform.Rotate(new Vector3(0, 0, -360 * prevPercentRate));
                circleParts.Setup(data);

                var textParts = Instantiate(circleGraphTextPrefab, Vector3.zero, Quaternion.identity,
                    layoutText.transform);
                circleGraphTexts.Add(textParts);
                textParts.gameObject.transform.localPosition = Vector3.zero;
                textParts.Setup(data.Name, -360.0f * prevPercentRate, data.Rate);

                prevPercentRate += data.Rate;
            }
        }
    }
}