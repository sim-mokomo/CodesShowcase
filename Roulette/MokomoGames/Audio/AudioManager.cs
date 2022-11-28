using System;
using System.Collections.Generic;
using System.Linq;
using MokomoGames.Audio.MasterTable;
using MokomoGames.Audio.Speaker;
using UnityEngine;

namespace MokomoGames.Audio
{
    [RequireComponent(typeof(AudioResourceMasterTable))]
    [RequireComponent(typeof(ObjectPool.ObjectPool))]
    public class AudioManager : MonoBehaviour
    {
        private AudioResourceMasterTable _audioResourceMasterTable;
        private ObjectPool.ObjectPool _speakerPool;
        private Dictionary<SpeakerEntity, SpeakerPresenter> _speakerCashes;
        private const int DefaultPoolSpeakerNum = 5;

        private void Awake()
        {
            _audioResourceMasterTable = GetComponent<AudioResourceMasterTable>();
            _speakerPool = GetComponent<ObjectPool.ObjectPool>();
            _speakerCashes = new Dictionary<SpeakerEntity, SpeakerPresenter>();
            _speakerPool.Pool<SpeakerPresenter>(DefaultPoolSpeakerNum);
        }

        public SpeakerPresenter GetSpeaker(SoundName soundName)
        {
            var cacheSpeaker =
                _speakerCashes
                    .FirstOrDefault(
                        x =>
                            x.Key.SoundName == soundName).Value;
            if (cacheSpeaker != null)
            {
                return cacheSpeaker;
            }

            var speaker = _speakerPool.Get<SpeakerPresenter>();
            var speakerEntity = _audioResourceMasterTable.Find(soundName);
            speaker.name = $"[Speaker]_{speakerEntity.SoundName}";
            speaker.Initialize(speakerEntity);
            _speakerCashes.Add(speakerEntity, speaker);

            return speaker;
        }
    }
}