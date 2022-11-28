using System;
using MokomoGames.Audio.MasterTable;
using UnityEngine;

namespace MokomoGames.Audio.Speaker
{
    public class SpeakerEntity
    {
        public float Volume { get; set; }
        public SoundName SoundName { get; }
        public AudioClip AudioClip { get; }

        public SpeakerEntity(SoundName soundName, AudioClip audioClip,float volume)
        {
            SoundName = soundName;
            AudioClip = audioClip;
            Volume = volume;
        }
    }
}
