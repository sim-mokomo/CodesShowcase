using System.Collections.Generic;
using System.Linq;
using MokomoGames.Audio.Speaker;
using UnityEngine;

namespace MokomoGames.Audio.MasterTable
{
    public enum SoundName
    {
        DrumRoll01,
        DrumRollFinish01,
        NormalButtonClick,
    }
    
    public class AudioResourceMasterTable : MonoBehaviour
    {
        [SerializeField] private List<AudioResourceMasterRecord> records
            = new List<AudioResourceMasterRecord>();
        
        public SpeakerEntity Find(SoundName soundName)
        {
            var record = records.FirstOrDefault(x => x.SoundName == soundName);
            if (record == null)
            {
                Debug.LogError($"{soundName.ToString()}にひもづくマスターデータが存在しません。");
                return null;
            }
            var entity = new SpeakerEntity(
                soundName,
                record.AudioClip,
                record.SourceVolume);
            return entity;
        }
    }
}