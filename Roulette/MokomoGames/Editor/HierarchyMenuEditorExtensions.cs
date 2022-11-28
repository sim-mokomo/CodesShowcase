using MokomoGames;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class HierarchyMenuEditorExtensions
    {
        [MenuItem("GameObject/MokomoGames/Create Zero Pos Empty")]
        public static void CreateEmptyInZeroPosition()
        {
            var emptyObj = new GameObject("EmptyObj");
            if (Selection.activeObject is GameObject selectingObject)
            {
                Debug.Log(selectingObject.name);
                emptyObj.transform.SetParent(selectingObject.transform);
                emptyObj.transform.localPosition = Vector3.zero;

                Selection.SetActiveObjectWithContext(emptyObj, Selection.activeContext);
            }
        }
    }
}