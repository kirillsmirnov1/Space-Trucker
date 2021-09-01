using Trucker.Model.Landmarks;
using Trucker.View.Dialogue;
using UnityEngine;
using UnityUtils;

namespace Trucker.Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private AudioSource playerMusic;
        [SerializeField] private AudioSource locationMusic;
        
        [Header("Events")]
        [SerializeField] private LandmarkTypeEvent onLandmarkInteraction;

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
            // TODO use some coroutine for actual crossfade 
            from.Stop();
            to.Play();
        }

        // TODO OnPlayerMusicChange
    }
}