using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build;

namespace MokomoGames.Editor.DefineSymbol
{
    /// <summary>
    /// 1つのキーに対応するDefineSymbol設定
    /// </summary>
    [Serializable]
    public class DefineSymbolSetting
    {
        public List<PlatformSetting> platformSettings;
        public string key;

        /// <summary>
        /// プラットフォーム別のDefineSymbol設定
        /// </summary>
        [Serializable]
        public class PlatformSetting
        {
            public string platform;
            public bool defined;

            public PlatformSetting(string platform, bool defined)
            {
                this.platform = platform;
                this.defined = defined;
            }
        }

        public DefineSymbolSetting(string key)
        {
            this.key = key;
            platformSettings = DefineSymbolConfig
                .SupportPlatformList
                .Select(x => new PlatformSetting(x.TargetName, false))
                .ToList();
        }

        public bool IsDefineInPlatform(NamedBuildTarget platform) =>
            GetPlatformSetting(platform.TargetName).defined;
        public void DefinedInPlatform(NamedBuildTarget platform, bool define) =>
            DefinedInPlatform(platform.TargetName, define);
        public void DefinedInPlatform(string platformTargetName, bool define) =>
            GetPlatformSetting(platformTargetName).defined = define;
        
        public PlatformSetting GetPlatformSetting(string platformTargetName) =>
            platformSettings.First(x => x.platform == platformTargetName);
    }
}