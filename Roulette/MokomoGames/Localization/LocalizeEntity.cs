using System.Collections.Generic;
using MokomoGames;
using MokomoGames.Localization;
using UnityEngine;

namespace Localization
{
    public class LocalizeEntity
    {
        private readonly Dictionary<string, string> _localizeTable;

        public LocalizeEntity(LocalizeConfig config, Dictionary<string, string> localizeTable)
        {
            _localizeTable = localizeTable;
            Config = config;
        }

        public LocalizeConfig Config { get; }

        public string GetLocalizedString(string key)
        {
            if (_localizeTable.TryGetValue(key, out var message)) return message;

            Debug.LogError($"{Config.Language.ToString()} には {key} は登録されていません");
            return string.Empty;
        }

        public static AppLanguage ConvertSystemLanguage2AppLanguage(SystemLanguage systemLanguage)
        {
            return systemLanguage switch
            {
                SystemLanguage.Arabic => AppLanguage.Arabic,
                SystemLanguage.Korean => AppLanguage.Korean,
                SystemLanguage.ChineseSimplified => AppLanguage.ChineseSimplified,
                SystemLanguage.ChineseTraditional => AppLanguage.ChineseTraditional,
                SystemLanguage.Japanese => AppLanguage.Japanese,
                SystemLanguage.English => AppLanguage.English,
                _ => AppLanguage.English
            };
        }
    }
}