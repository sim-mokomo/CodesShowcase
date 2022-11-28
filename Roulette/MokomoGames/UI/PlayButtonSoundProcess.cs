using MokomoGames.Audio;
using MokomoGames.Audio.MasterTable;
using MokomoGames.GameConfig;
using UnityEngine;
using UnityEngine.UI;

namespace MokomoGames.UI
{
    public class PlayButtonSoundProcess : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private SoundName soundName;

        private void Start()
        {
            button.onClick.AddListener(() =>
            {
                new AudioService().PlayOneShot(
                    FindObjectOfType<AudioManager>(),
                    soundName,
                    FindObjectOfType<GameConfigManager>()
                );
            });
        }
    }
}