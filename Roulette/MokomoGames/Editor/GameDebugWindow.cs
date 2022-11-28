using MokomoGames.Debugger;
using MokomoGames.Localization;
using UnityEditor;
using UnityEngine;

namespace MokomoGames.Editor
{
    public class GameDebugWindow : EditorWindow
    {
        private GameDebugSaveData _saveData;

        [MenuItem("MokomoGames/ゲームデバッグウィンドウを開く")]
        public static void Open()
        {
            GetWindow<GameDebugWindow>();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("デバッグ用セーブデータを作成"))
            {
                var saveData = CreateInstance<GameDebugSaveData>();
                AssetDatabase.CreateAsset(saveData, GameDebugSaveData.SaveDataFileName);
                AssetDatabase.SaveAssets();
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = saveData;
            }
            
            if (GUILayout.Button("デバッグ用セーブデータを読み込み"))
            {
                _saveData = AssetDatabase.LoadAssetAtPath<GameDebugSaveData>(GameDebugSaveData.SaveDataFileName);
            }
            
            if (_saveData)
            {
                var preLanguage = _saveData.GameLanguage;
                var curLanguage = (AppLanguage) EditorGUILayout.EnumPopup("ゲーム内言語", _saveData.GameLanguage);
                if (preLanguage != curLanguage)
                {
                    _saveData.GameLanguage = curLanguage;
                }
            }

            if (!GUI.changed) return;
            
            var localizedManager = FindObjectOfType<LocalizeManager>();
            localizedManager.LoadAsync(_saveData.GameLanguage);
        }
    }
}
