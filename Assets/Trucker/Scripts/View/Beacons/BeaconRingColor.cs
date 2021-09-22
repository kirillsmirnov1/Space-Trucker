using System;
using Trucker.Model.Beacons;
using UnityEngine;

namespace Trucker.View.Beacons
{
    public class BeaconRingColor : MonoBehaviour
    {
        [SerializeField] private BeaconStatusProvider beaconStatusProvider;
        [SerializeField] private MeshRenderer[] renderers;
        
        [Header("Pulsation")]
        [SerializeField] private AnimationCurve intensity;
        [SerializeField] private float pulsationSpeed = 1f;
        
        private Material _material;
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
        private Color _statusColor;
        private float _pulsationTime;

        private void OnValidate()
        {
            beaconStatusProvider = GetComponentInParent<BeaconStatusProvider>();
            renderers = GetComponentsInChildren<MeshRenderer>();
        }

        private void Awake()
        {
            InitMaterial();
            beaconStatusProvider.OnBeaconStatusChange += UpdateRingColor;
        }

        private void Start()
        {
            UpdateRingColor(beaconStatusProvider.Status);
        }

        private void OnDestroy()
        {
            beaconStatusProvider.OnBeaconStatusChange -= UpdateRingColor;
        }

        private void Update()
        {
            PulsateRingEmission();
        }

        private void PulsateRingEmission()
        {
            _pulsationTime += Time.deltaTime * pulsationSpeed;
            _pulsationTime %= 1f;

            var currentIntensity = intensity.Evaluate(_pulsationTime);
            var factor = Mathf.Pow(2f, currentIntensity);
            var emissionColor = _statusColor * factor;
            
            _material.SetColor(EmissionColor, emissionColor);
        }

        private void InitMaterial()
        {
            _material = new Material(renderers[0].material);
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material = _material;
            }
        }

        private void UpdateRingColor(BeaconStatus beaconStatus)
        {
            _statusColor = StatusColor(beaconStatus);
            _material.color = _statusColor;
        }

        private static Color StatusColor(BeaconStatus beaconStatus) =>
            beaconStatus switch
            {
                BeaconStatus.Far => Color.red,
                BeaconStatus.Near => Color.yellow,
                BeaconStatus.InPosition => Color.green,
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}