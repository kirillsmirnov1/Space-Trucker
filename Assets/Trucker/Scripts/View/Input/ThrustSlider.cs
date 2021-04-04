using System;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils.Variables;

namespace Trucker.View.Input
{
    [RequireComponent(typeof(Slider))]
    public class ThrustSlider : MonoBehaviour
    {
        [SerializeField] private FloatVariable thrust;
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(SetThrust);
            SetThrust(0);
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(SetThrust);
        }

        private void SetThrust(float sliderValue) => thrust.Value = sliderValue;
    }
}