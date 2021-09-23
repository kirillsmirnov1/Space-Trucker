using System;
using Trucker.Model.Beacons;
using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Control.Beacons
{
    [RequireComponent(typeof(BeaconAnchorPosProvider))]
    [RequireComponent(typeof(BeaconAnchorLock))]
    public class BeaconDrift : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private BeaconAnchorLock anchorLock;
        [SerializeField] private BeaconAnchorPosProvider posProvider;
        [SerializeField] private Rigidbody rb;

        [Header("Data")] 
        [SerializeField] private FloatVariable driftForceModificator;
        [SerializeField] private FloatVariable maxDistanceFromAnchor;

        private Action _onFixedUpdate;
        private Vector3 _anchorPos;

        private void OnValidate()
        {
            anchorLock = GetComponent<BeaconAnchorLock>();
            posProvider = GetComponent<BeaconAnchorPosProvider>();
            rb = GetComponentInParent<Rigidbody>();
        }

        private void Awake()
            => anchorLock.OnAnchored += SetDrift;

        private void Start()
            => SetDrift(anchorLock.Anchored);

        private void FixedUpdate()
            => _onFixedUpdate?.Invoke();

        private void SetDrift(bool anchored)
        {
            if (anchored)
            {
                _anchorPos = posProvider.Pos;
                _onFixedUpdate = Drift;
            }
            else
                _onFixedUpdate = null;
        }

        private void Drift()
        {
            var currentPos = rb.position;
            var distanceToTarget = Vector3.Distance(currentPos, _anchorPos);
            if (distanceToTarget < maxDistanceFromAnchor) return;

            var desiredForceDirection = (_anchorPos - currentPos).normalized;
            var desiredForceMagnitude = Mathf.Pow(distanceToTarget, 2) * driftForceModificator;
            var desiredForce = desiredForceDirection * desiredForceMagnitude;
            var velocityOnDesiredDirection = Vector3.Project(rb.velocity, desiredForceDirection);
            var appliedForce = desiredForce - velocityOnDesiredDirection;

            rb.AddForce(appliedForce, ForceMode.Acceleration);
            
            // Debug.DrawLine(currentPos, _anchorPos, Color.red);
            // Debug.DrawRay(currentPos, desiredForceDirection, Color.blue);
            // Debug.DrawRay(currentPos, appliedForce, Color.green);
        }
    }
}