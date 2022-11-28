using System;
using MokomoGames.Localization;
using UnityEngine;

namespace MokomoGames.Debugger
{
    public class GameDebugSaveData : ScriptableObject
    {
        #if UNITY_EDITOR
        public const string SaveDataFileName = "Assets/MokomoGames/Debugger/GameDebugSaveData.asset";
        #endif
        
        private AppLanguage _gameLanguage;
        public AppLanguage GameLanguage
        {
            get => _gameLanguage;
            set
            {
                _gameLanguage = value;
                OnChangedGameLanguage?.Invoke(_gameLanguage);
            }
        }

        public Action<AppLanguage> OnChangedGameLanguage;
    }
}