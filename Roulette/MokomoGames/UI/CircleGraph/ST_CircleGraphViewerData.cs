using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MokomoGames
{
    public class ST_CircleGraphViewData
    {
        public float Rate { get; }
        public string Name { get; }
        public Color Color { get; }
                
        public ST_CircleGraphViewData(float rate, string name, Color color)
        {
            Rate = rate;
            Name = name;
            Color = color;
        }
    }
}
