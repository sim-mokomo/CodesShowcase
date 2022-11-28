using System;
using System.Collections.Generic;
using System.Linq;
using MokomoGames.Utilities;
using UnityEngine;
using Utilities;

namespace MokomoGames.ObjectPool
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> prefabs = new List<MonoBehaviour>();
        [SerializeField] private GameObject poolRoot;
        private readonly Dictionary<Type, List<MonoBehaviour>> _poolableDictionary = 
            new Dictionary<Type, List<MonoBehaviour>>();

        public IEnumerable<T> Pool<T>(int poolNum) where T : MonoBehaviour
        {
            var prefab = prefabs.FirstOrDefault(x => x is T);
            if (prefab == null) return null;
            var objs = new List<T>();
            for (int i = 0; i < poolNum; i++)
            {
                var obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, poolRoot.transform);
                objs.Add(obj.GetComponent<T>());
            }

            if (_poolableDictionary.TryGetValue(typeof(T), out var list))
            {
            }
            else
            {
                list = new List<MonoBehaviour>();
                _poolableDictionary.Add(typeof(T), list);
            }

            foreach (var obj in objs)
            {
                var poolable = obj as MonoBehaviour;
                poolable.gameObject.SetActive(false);
                list.Add(poolable);
            }

            return objs;
        }

        public T Get<T>() where T : MonoBehaviour, new()
        {
            return Get<T>(Vector3.zero, Quaternion.identity);
        }

        public T Get<T>(Vector3 pos, Quaternion rot) where T : MonoBehaviour, new()
        {
            T ret = null;

            if (_poolableDictionary.TryGetValue(typeof(T), out var list))
                foreach (var item in list)
                    if (!item.gameObject.activeSelf)
                    {
                        ret = item as T;
                        break;
                    }

            if (ret == null) ret = Pool<T>(1).FirstOrDefault();

            ret.gameObject.transform.SetPositionAndRotation(pos, rot);
            ret.gameObject.SetActive(true);
            return ret;
        }

        public void Destroy<T>(T obj) where T : MonoBehaviour
        {
            DeactivateObj(obj);
        }

        private void DeactivateObj<T>(T obj) where T : MonoBehaviour
        {
            if (obj is IReleaseable releasable)
            {
                releasable.Release();
            }
            obj.gameObject.SetActive(false);
            obj.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(poolRoot.transform, false);
        }
    }
}