using UnityEngine;

namespace Roulette.Graph
{
    public class CircleGraphData
    {
        public CircleGraphData(float rate, string name, Color color)
        {
            Rate = rate;
            Name = name;
            Color = color;
        }

        public float Rate { get; }
        public string Name { get; }
        public Color Color { get; }
    }
}