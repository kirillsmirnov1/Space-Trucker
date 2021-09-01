using System;
using Trucker.Model.Landmarks;
using Trucker.View.Dialogue;
using UnityEngine;
using UnityUtils;
using UnityUtils.Variables;

namespace Trucker.Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private AudioSource playerMusic;
        [SerializeField] private AudioSource locationMusic;
        
        [Header("Events")]
        [SerializeField] private LandmarkTypeEvent onLandmarkInteraction;

        [Header("Data")]
        [SerializeField] private FloatVariable crossFadeDuration;
        
        private Action _onUpdate;
        private AudioSource _from;
        private AudioSource _to;
        private float _crossFadeTimeLeft;
        
        private void OnValidate() => this.CheckNullFields();

        private void Awake()
        {
            DialogueView.OnClose += OnLandmarkInteractionEnd;
            onLandmarkInteraction.RegisterAction(OnLandmarkInteractionStart);
        }

        private void OnDestroy()
        {
            DialogueView.OnClose -= OnLandmarkInteractionEnd;
            onLandmarkInteraction.UnregisterAction(OnLandmarkInteractionStart);
        }

        private void Update() => _onUpdate?.Invoke();

        private void OnLandmarkInteractionStart(LandmarkType landmarkType)
        {
            // TODO set locationMusicFile
            CrossFade(playerMusic, locationMusic);
        }

        private void OnLandmarkInteractionEnd()
        {
            CrossFade(locationMusic, playerMusic);
        }

        private void CrossFade(AudioSource from, AudioSource to)
        {
            _from = from;
            _to = to;
            _from.volume = 1f;
            _to.volume = 0f;
            _crossFadeTimeLeft = crossFadeDuration;
            _to.Play();
            _onUpdate = ApplyCrossFade;
        }

        private void ApplyCrossFade()
        {
            _crossFadeTimeLeft -= Time.deltaTime;
            
            _from.volume = Mathf.InverseLerp(0f, crossFadeDuration, _crossFadeTimeLeft);
            _to.volume = Mathf.InverseLerp(crossFadeDuration, 0f, _crossFadeTimeLeft);
            
            if (_crossFadeTimeLeft <= 0f)
            {
                _from.Stop();
                _onUpdate = null;
            }
        }

        // TODO OnPlayerMusicChange
    }
}