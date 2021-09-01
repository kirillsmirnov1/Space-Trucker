﻿using UnityEngine;
using UnityEngine.Audio;
using UnityUtils.Variables;

namespace Trucker.Audio
{
    public class VolumeManagerFloat : MonoBehaviour
    {
        [SerializeField] private FloatVariable volumeVar;
        [SerializeField] private AudioMixer audioGroup;
        [SerializeField] private string volumeParamName = "Volume";
        
        private void Awake() 
            => volumeVar.OnChange += SetVolume;
        private void OnDestroy() 
            => volumeVar.OnChange -= SetVolume;

        private void Start() 
            => SetVolume(volumeVar);

        private void SetVolume(float volume)
        {
            audioGroup.SetFloat(volumeParamName, Mathf.Log10(volume) * 20);
        }
    }
}