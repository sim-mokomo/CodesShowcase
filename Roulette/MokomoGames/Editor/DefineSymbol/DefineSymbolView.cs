using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MokomoGames.Editor.DefineSymbol
{
        public class DefineSymbolView
        {
            public class ToggleResult
            {
                public string Platform;
                public bool Define;
            }
            
            public DefineSymbolView(DefineSymbolSetting defineSymbolSetting)
            {
                DefineSymbolSetting = defineSymbolSetting;
            }
            
            public DefineSymbolSetting DefineSymbolSetting { get; private set; }
            public bool IsClickedDeleteButton { get; private set; }
            public List<ToggleResult> ToggledResultList { get; private set; } = new List<ToggleResult>();
            
            public void OnGUI()
            {
                IsClickedDeleteButton = false;
                ToggledResultList.Clear();

                using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                {
                    if (GUILayout.Button("削除"))
                    {
                        IsClickedDeleteButton = true;
                    }
                    EditorGUILayout.LabelField($"シンボル名: {DefineSymbolSetting.key}", EditorStyles.boldLabel);
                    EditorGUILayout.LabelField("定義先プラットフォーム一覧:", EditorStyles.boldLabel);

                    foreach (var platform in DefineSymbolConfig.SupportPlatformList)
                    {
                        var defined = EditorGUILayout.Toggle(
                            platform.TargetName,
                            DefineSymbolSetting.GetPlatformSetting(platform.TargetName).defined
                        );
                        ToggledResultList.Add(new ToggleResult
                        {
                            Platform = platform.TargetName,
                            Define = defined
                        });
                    }
                }
            }
        }
}