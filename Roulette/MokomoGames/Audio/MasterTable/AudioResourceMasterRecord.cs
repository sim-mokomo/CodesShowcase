using System;
using UnityEngine;

namespace MokomoGames.Audio.MasterTable
{
    [Serializable]
    [CreateAssetMenu(
        fileName = "AudioResourceMasterRecord", 
        menuName = "MokomoGames/Audio/Create Audio Resource Master Record")]
    public class AudioResourceMasterRecord : ScriptableObject
    {
        [SerializeField] private SoundName soundName;
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private float sourceVolume = 1.0f;

        public SoundName SoundName => soundName;

        public AudioClip AudioClip => audioClip;

        public float SourceVolume => sourceVolume;
    }
}