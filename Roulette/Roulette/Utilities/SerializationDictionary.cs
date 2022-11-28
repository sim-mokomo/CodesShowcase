using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Roulette.Utilities
{
    public class SerializationDictionary<TKey, TValue> : ISerializationCallbackReceiver
    {
        [SerializeField] private List<TKey> _keys;
        [SerializeField] private List<TValue> _values;
        public Dictionary<TKey, TValue> BuiltedDictionary { get; private set; } = new();

        public void OnBeforeSerialize()
        {
            _keys = new List<TKey>(BuiltedDictionary.Keys);
            _values = new List<TValue>(BuiltedDictionary.Values);
        }

        public void OnAfterDeserialize()
        {
            var minLength = Mathf.Min(_keys.Count, _values.Count);
            BuiltedDictionary = new Dictionary<TKey, TValue>(minLength);
            foreach (var i in Enumerable
                         .Range(0, minLength))
                BuiltedDictionary.Add(_keys[i], _values[i]);
        }
    }
}