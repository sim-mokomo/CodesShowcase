using System.Collections;
using UnityEngine;

namespace Utilities
{
    public class CoroutineHandler : MonoBehaviour
    {
        public Coroutine WrapStartCoroutine(IEnumerator routine)
        {
            var coroutine = base.StartCoroutine(routine);
            return coroutine;
        }

        public void WrapStopCoroutine(Coroutine coroutine)
        {
            if (coroutine == null) return;
            base.StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}