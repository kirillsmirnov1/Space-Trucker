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
            for (var i = 0; i < _clips.Length; i++)
            {
                if (_clips[i].name == currentClip.Value.name)
                {
                    _currentSongIndex = i;
                    return;
                }
            }

            _currentSongIndex = 0;
            UpdateClipVariable();
        }

        private void SetSongName()
        {
            songName.text = _clips[_currentSongIndex].name;
        }

        public void Iterate(int i)
        {
            _currentSongIndex = (_currentSongIndex + i) % _clips.Length;
            if (_currentSongIndex < 0) _currentSongIndex += _clips.Length;
            
            UpdateClipVariable();
            SetSongName();
        }

        private void UpdateClipVariable() 
            => currentClip.Value = _clips[_currentSongIndex];
    }
}