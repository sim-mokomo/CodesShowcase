using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

namespace MokomoGames.Editor.DefineSymbol
{
    public static class UnityDefineSymbolRepository
    {
        public static List<string> LoadSymbols(NamedBuildTarget buildTarget)
        {
            return PlayerSettings.GetScriptingDefineSymbols(buildTarget).Split(';').ToList();
        }

        public static void SaveSymbols(DefineSymbolSettingList list)
        {
            foreach (var platform in DefineSymbolConfig.SupportPlatformList)
            {
                var symbolSettings = list.GetSymbolSettingsDefinedInPlatform(platform);
                var defineString = String.Join(";", symbolSettings.Select(x => x.key));
                Debug.Log(platform.TargetName);
                Debug.Log(defineString);
                PlayerSettings.SetScriptingDefineSymbols(platform, defineString);
            }
        }
    }

}