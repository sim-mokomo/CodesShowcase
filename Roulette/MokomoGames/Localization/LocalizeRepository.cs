using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Localization;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Utilities;

namespace MokomoGames.Localization
{
    public class LocalizeRepository : MonoBehaviour
    {
        [SerializeField] private List<LocalizeConfig> configTable = new List<LocalizeConfig>();

        public void Load(AppLanguage language, Action<LocalizeEntity> onEnd)
        {
            PlayFabClientAPI.GetTitleData(
                new GetTitleDataRequest(),
                result =>
                {
                    var config = GetLocalizeConfig(language);
                    if (result.Data.TryGetValue(config.TableName, out var json))
                    {
                        var dic = JsonUtility.FromJson<SerializationDictionary<string, string>>(json).BuiltedDictionary;
                        onEnd?.Invoke(new LocalizeEntity(config, dic));
                    }
                },
                error => { });
        }

        public UniTask<LocalizeEntity> LoadAsync(AppLanguage language)
        {
            var source = new UniTaskCompletionSource<LocalizeEntity>();
            Load(
                language,
                entity =>
                {
                    source.TrySetResult(entity);
                });
            return source.Task;
        }

        private LocalizeConfig GetLocalizeConfig(AppLanguage language)
        {
            var config = configTable.FirstOrDefault(x => x.Language == language);
            return config == null ? configTable.FirstOrDefault(x => x.Language == AppLanguage.English) : config;
        }
    }
}