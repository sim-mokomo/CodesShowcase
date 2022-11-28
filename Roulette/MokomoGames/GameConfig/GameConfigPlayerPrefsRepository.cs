using UnityEngine;

namespace MokomoGames.GameConfig
{
    public class GameConfigPlayerPrefsRepository
    {
        private const string IsMuteKey = "isMute";

        public void Save(GameConfig gameConfig)
        {
            PlayerPrefs.SetString(IsMuteKey, gameConfig.IsMute.ToString());
        }

        public GameConfig Load()
        {
            var isMute = false;
            if (PlayerPrefs.HasKey(IsMuteKey)) isMute = bool.Parse(PlayerPrefs.GetString(IsMuteKey));
            return new GameConfig(isMute);
        }
    }
}