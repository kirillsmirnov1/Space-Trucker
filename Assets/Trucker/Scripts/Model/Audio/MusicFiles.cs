using System;
using System.Collections.Generic;
using System.Linq;
using OneLine;
using Trucker.Model.Landmarks;
using UnityEngine;
using UnityUtils.Saves;

namespace Trucker.Model.Audio
{
    [CreateAssetMenu(menuName = "Audio/MusicFiles", fileName = "MusicFiles", order = 0)]
    public class MusicFiles : InitiatedScriptableObject
    {
        [SerializeField, OneLine, HideLabel] private MusicData[] music;

        private Dictionary<LandmarkType, AudioClip> _landmarkMusic;
        public AudioClip[] AvailableSongs 
            => music.Select(md => md.clip).ToArray(); // TODO return only available 

        public override void Init()
        {
            InitLandmarkMusic();
        }

        public AudioClip LandmarkMusic(LandmarkType landmark) 
            => _landmarkMusic[landmark]; 

        private void InitLandmarkMusic()
        {
            _landmarkMusic = new Dictionary<LandmarkType, AudioClip>();
            for (int i = 0; i < music.Length; i++)
            {
                var landmark = music[i].landmark;
                if (landmark == LandmarkType.None) continue;
                _landmarkMusic.Add(landmark, music[i].clip);
            }
        }

        public AudioClip GetSongByName(string songName)
        {
            var song = music
                .Select(md => md.clip)
                .FirstOrDefault(clip => clip.name.Equals(songName));
            return song == null ? AvailableSongs[0] : song;
        }
    }

    [Serializable]
    public struct MusicData
    {
        public AudioClip clip;
        public LandmarkType landmark;
    }
}