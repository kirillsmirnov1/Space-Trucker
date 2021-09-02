using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Model.Audio
{
    [CreateAssetMenu(menuName = "Data/SerializePlayerSong", fileName = "SerializePlayerSong", order = 0)]
    public class SerializePlayerSong : StringVariable
    {
        [Header("Player Song Serializer")]
        [SerializeField] private AudioClipVariable playerClipVariable;
        [SerializeField] private MusicFiles musicFiles;
        
        public override void Init()
        {
            base.Init();
            playerClipVariable.OnChange += UpdateName;
            playerClipVariable.Value = musicFiles.GetSongByName(Value);
        }

        private void UpdateName(AudioClip newClip)
        {
            Value = newClip.name;
        }
    }
}