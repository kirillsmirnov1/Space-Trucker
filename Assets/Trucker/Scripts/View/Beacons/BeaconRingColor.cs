using System;
using Trucker.Model.Beacons;
using UnityEngine;

namespace Trucker.View.Beacons
{
    public class BeaconRingColor : MonoBehaviour
    {
        [SerializeField] private BeaconStatusProvider beaconStatusProvider;
        [SerializeField] private MeshRenderer[] renderers;
        
        private Material _material;
        
        private void OnValidate()
        {
            beaconStatusProvider = GetComponentInParent<BeaconStatusProvider>();
            renderers = GetComponentsInChildren<MeshRenderer>();
        }

        private void Awake()
        {
            InitMaterial();
            beaconStatusProvider.OnBeaconStatusChange += SetRingColor;
        }

        private void Start()
        {
            SetRingColor(beaconStatusProvider.Status);
        }

        private void OnDestroy()
        {
            beaconStatusProvider.OnBeaconStatusChange -= SetRingColor;
        }

        private void InitMaterial()
        {
            _material = new Material(renderers[0].material);
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material = _material;
            }
        }

        private void SetRingColor(BeaconStatus beaconStatus)
        {
            // TODO use emission 
            _material.color = beaconStatus switch
            {
                BeaconStatus.Far => Color.red,
                BeaconStatus.Near => Color.yellow,
                BeaconStatus.InPosition => Color.green,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}