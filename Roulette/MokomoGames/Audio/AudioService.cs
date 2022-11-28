using System;
using System.Linq;
using MokomoGames.Audio.MasterTable;
using MokomoGames.GameConfig;

namespace MokomoGames.Audio
{
    public class AudioService
    {
        public void PlayOneShot(AudioManager audioManager, SoundName soundName, GameConfigManager gameConfigManager)
        {
            if (gameConfigManager.Config.IsMute)
            {
                return;
            }

            var speakerPresenter = audioManager.GetSpeaker(soundName);
            speakerPresenter.PlayOneShot();
        }

        public void Stop(AudioManager audioManager, SoundName soundName)
        {
            var speakerPresenter = audioManager.GetSpeaker(soundName);
            speakerPresenter.Stop();
        }
        
        public void StopAll(AudioManager audioManager)
        {
            var soundNames = Enum.GetValues(typeof(SoundName)).Cast<SoundName>().ToList();
            foreach (var soundName in soundNames)
            {
                Stop(audioManager, soundName);   
            }
        }
    }
}