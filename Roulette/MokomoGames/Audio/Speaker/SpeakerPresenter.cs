using UnityEngine;

namespace MokomoGames.Audio.Speaker
{
    [RequireComponent(typeof(AudioSource))]
    public class SpeakerPresenter : MonoBehaviour
    {
        private SpeakerEntity _speakerEntity;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Initialize(SpeakerEntity entity)
        {
            _speakerEntity = entity;
        }

        private void SetVolume(float volume)
        {
            _speakerEntity.Volume = volume;
        }

        public void PlayOneShot()
        {
            _audioSource.PlayOneShot(_speakerEntity.AudioClip);
        }

        public void Stop()
        {
            _audioSource.Stop();
        }
    }
}