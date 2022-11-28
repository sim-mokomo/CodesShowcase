using System;
using UnityEngine;

namespace MokomoGames.GameConfig
{
    public class GameConfigManager : MonoBehaviour
    {
        private readonly GameConfigPlayerPrefsRepository _gameConfigRepository;
        public GameConfig Config { get; private set; }
        public event Action<GameConfig> OnUpdatedGameConfig;

        public GameConfigManager()
        {
            _gameConfigRepository = new GameConfigPlayerPrefsRepository();
        }

        public void Save()
        {
            _gameConfigRepository.Save(Config);
            OnUpdatedGameConfig?.Invoke(Config);
        }

        public GameConfig Load()
        {
            Config = _gameConfigRepository.Load();
            OnUpdatedGameConfig?.Invoke(Config);
            return Config;
        }
    }
}