using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

namespace MokomoGames.Editor.DefineSymbol
{
    public class DefineSymbolWindow : EditorWindow
    {
        [MenuItem("MokomoGames/DefineSymbolWindow")]
        public static void Open()
        {
            GetWindow<DefineSymbolWindow>();
        }
        
        private Vector2 _scrollPos;
        private string _newSymbolInputName;
        private DefineSymbolSettingList _defineSymbolSettingList = new DefineSymbolSettingList();
        private List<DefineSymbolView> _defineSymbolViews = new List<DefineSymbolView>();

        private void OnGUI()
        {
            if (GUILayout.Button("シンボル一覧の読み込み"))
            {
                _defineSymbolSettingList = new DefineSymbolService().LoadDefineSymbol();
                _defineSymbolViews =
                    _defineSymbolSettingList
                        .Settings
                        .Select(x => new DefineSymbolView(x))
                        .ToList();
            }

            
            _newSymbolInputName = EditorGUILayout.TextField("新規シンボル名:", _newSymbolInputName);
            if (GUILayout.Button("シンボルの追加"))
            {
                _defineSymbolSettingList.AddSymbol(_newSymbolInputName);
                var symbolSetting = _defineSymbolSettingList.GetSymbolSettingByKey(_newSymbolInputName);
                _defineSymbolViews.Add(new DefineSymbolView(symbolSetting));
            }

            if (GUILayout.Button("反映"))
            {
                UnityDefineSymbolRepository.SaveSymbols(_defineSymbolSettingList);
            }

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            {
                for (var i = _defineSymbolViews.Count - 1; i > 0; i--)
                {
                    var defineSymbolView = _defineSymbolViews[i];
                    defineSymbolView.OnGUI();

                    if (defineSymbolView.IsClickedDeleteButton)
                    {
                        _defineSymbolSettingList.RemoveSymbol(defineSymbolView.DefineSymbolSetting.key);
                        _defineSymbolViews.RemoveAt(i);
                    }

                    defineSymbolView.ToggledResultList.ForEach(x =>
                    {
                        defineSymbolView.DefineSymbolSetting.DefinedInPlatform(x.Platform, x.Define);
                    });
                }
            }
            EditorGUILayout.EndScrollView();
        }
    }
}
