using System.Threading.Tasks;
using Localization;
using MokomoGames.Debugger;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MokomoGames.Localization
{
    public enum AppLanguage
    {
        Arabic,
        Japanese,
        English,
        Korean,
        ChineseSimplified,
        ChineseTraditional
    }
    public class LocalizeManager : MonoBehaviour
    {
        [SerializeField] private LocalizeRepository localizeRepository;
        public LocalizeEntity LocalizeEntity { get; private set; }
        public event System.Action OnChangedLanguage;
        
#if UNITY_EDITOR
        private GameDebugSaveData _gameDebugSaveData;
#endif

        private void Awake()
        {
#if UNITY_EDITOR
            _gameDebugSaveData = AssetDatabase.LoadAssetAtPath<GameDebugSaveData>(GameDebugSaveData.SaveDataFileName);
            _gameDebugSaveData.OnChangedGameLanguage += async language =>
            {
                if (!isEndedLoading())
                {
                    return;
                }

                LocalizeEntity = await LoadAsync(language);
            };
#endif
        }

        public bool isEndedLoading() => LocalizeEntity != null;

        public async Task<LocalizeEntity> LoadAsync(SystemLanguage language)
        {
            return await LoadAsync(LocalizeEntity.ConvertSystemLanguage2AppLanguage(language));
        }

        public async Task<LocalizeEntity> LoadAsync(AppLanguage language)
        {
            var localizedEntity = await localizeRepository.LoadAsync(language);
            LocalizeEntity = localizedEntity;
            OnChangedLanguage?.Invoke();
            return localizedEntity;
        }

        public string GetLocalizedString(string textKey)
        {
            return LocalizeEntity == null ? string.Empty : LocalizeEntity.GetLocalizedString(textKey);
        }
    }
}