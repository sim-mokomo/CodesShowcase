using UnityEngine;

namespace MokomoGames.UI
{
    public class UIManager : MonoBehaviour
    {
        public enum CanvasOrder
        {
            Back,
            Center,
            Front,
        }
        
        [SerializeField] private GameObject backCanvasRoot;
        [SerializeField] private GameObject middleCanvasRoot;
        [SerializeField] private GameObject frontCanvasRoot;
        [SerializeField] private ObjectPool.ObjectPool uiObjectPool;

        public T Create<T>(Transform parent, bool worldPositionStays = false) where T : MonoBehaviour, new()
        {
            var obj = uiObjectPool.Get<T>();
            obj.transform.SetParent(parent, worldPositionStays);
            return obj;
        }

        public T Create<T>(Vector3 pos, Quaternion rot, Transform parent, bool worldPositionStays = false) where T : MonoBehaviour, new()
        {
            var obj = uiObjectPool.Get<T>();
            obj.transform.SetParent(parent, worldPositionStays);
            obj.transform.SetPositionAndRotation(pos, rot);
            return obj;
        }

        public T Create<T>(CanvasOrder order) where T : MonoBehaviour, new()
        {
            var obj = uiObjectPool.Get<T>();
            obj.transform.SetParent(GetCanvas(order).transform,false);
            return obj;
        }

        public void Destroy<T>(T obj) where T : MonoBehaviour
        {
            uiObjectPool.Destroy(obj);
        }

        private GameObject GetCanvas(CanvasOrder order)
        {
            switch (order)
            {
                case CanvasOrder.Back:
                    return backCanvasRoot;
                case CanvasOrder.Center:
                    return middleCanvasRoot;
                case CanvasOrder.Front:
                    return frontCanvasRoot;
            }

            return null;
        }
    }
}