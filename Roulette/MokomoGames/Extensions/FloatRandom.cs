using System;
using UnityEngine;

namespace MokomoGames.Extensions
{
    [Serializable]
    public class FloatRandom
    {
        [SerializeField] private float min;
        [SerializeField] private float max;
        
        public FloatRandom(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        public float Rand()
        {
            return UnityEngine.Random.Range(min, max);
        }
    }
}