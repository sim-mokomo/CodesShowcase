namespace MokomoGames.GameConfig
{
    public class GameConfig
    {
        public bool IsMute { get; private set; }

        public GameConfig(bool isMute)
        {
            IsMute = isMute;
        }

        public void ToggleMute()
        {
            IsMute = !IsMute;
        }
    }
}