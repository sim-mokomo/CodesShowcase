using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build;

namespace MokomoGames.Editor.DefineSymbol
{
    /// <summary>
    /// DefineSymbol設定のリスト
    /// </summary>
    public class DefineSymbolSettingList
    {
        public readonly List<DefineSymbolSetting> Settings;

        public DefineSymbolSettingList()
        {
            Settings = new List<DefineSymbolSetting>();
        }

        public DefineSymbolSetting GetSymbolSettingByKey(string key)
        {
            return Settings.FirstOrDefault(x => x.key == key);
        }

        public List<DefineSymbolSetting> GetSymbolSettingsDefinedInPlatform(NamedBuildTarget platform)
        {
            return Settings
                .Where(x => x.IsDefineInPlatform(platform))
                .Where(x => x.key != string.Empty)
                .ToList();
        }

        public void AddSymbol(string key)
        {
            foreach (var platform in DefineSymbolConfig.SupportPlatformList)
            {
                AddSymbol(platform, key);
            }
        }

        public void AddSymbol(NamedBuildTarget platform, string key)
        {
            var symbolExists = Settings.Exists(x => x.key == key);
            if (symbolExists)
            {
                Settings
                    .First(x => x.key == key)
                    .DefinedInPlatform(platform, true);
            }
            else
            {
                var setting = new DefineSymbolSetting(key);
                setting.DefinedInPlatform(platform, true);
                Settings.Add(setting);
            }
        }

        public void RemoveSymbol(string key)
        {
            var index = Settings.FindIndex(x => x.key == key);
            if (index < 0)
            {
                return;
            }
            Settings.RemoveAt(index);
        }
    }
}