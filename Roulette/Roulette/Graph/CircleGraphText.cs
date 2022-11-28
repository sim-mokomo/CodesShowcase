using TMPro;
using UnityEngine;

namespace Roulette.Graph
{
    public class CircleGraphText : MonoBehaviour
    {
        [SerializeField] private GameObject nameRoot;
        [SerializeField] private TextMeshProUGUI content;

        public void Setup(string contentString, float totalRot, float graphRatio)
        {
            SetContent(contentString);
            UpdateRate(totalRot, graphRatio);
        }

        public void SetContent(string contnetString)
        {
            content.text = contnetString;
        }

        public void UpdateRate(float totalRot, float graphRatio)
        {
            var centerRatio = -360.0f * graphRatio * 0.5f;
            nameRoot.transform.Rotate(Vector3.forward, totalRot + centerRatio);
        }
    }
}