using UnityEditor;
using UnityEngine;

namespace MokomoGames.Editor
{
    [CustomEditor(typeof(Transform))]
    public class TransformInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var self = target as Transform;
            if (self == null)
            {
                base.OnInspectorGUI();
                return;
            }
            
            if (GUILayout.Button("Reset All Transform"))
            {
                self.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                self.localScale = Vector3.one;
            }
            base.OnInspectorGUI();
        }
    }
}
