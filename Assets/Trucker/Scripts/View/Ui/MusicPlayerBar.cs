using TMPro;
using Trucker.Model.Audio;
using UnityEngine;

namespace Trucker.View.Ui
{
    public class MusicPlayerBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI songName;
        [SerializeField] private MusicFiles music;
        [SerializeField] private AudioClipVariable currentClip;
        
        private AudioClip[] _clips;
        private int _currentSongIndex;
        
        private void OnEnable()
        {
            GetSongs();
            SetIterator();
            SetSongName();
        }

        private void GetSongs()
        {
            _clips = music.AvailableSongs;
        }

        private void SetIterator()
        {
            while (currentClip.Value.name != _clips[_currentSongIndex].name)
            {
                _currentSongIndex++;
            }
        }

        private void SetSongName()
        {
            songName.text = _clips[_currentSongIndex].name;
        }

        public void Iterate(int i)
        {
            _currentSongIndex = (_currentSongIndex + i) % _clips.Length;
            if (_currentSongIndex < 0) _currentSongIndex += _clips.Length;
            
            var newClip = _clips[_currentSongIndex];
            currentClip.Value = newClip;
            SetSongName();
        }
    }
}