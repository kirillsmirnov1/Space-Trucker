using System;
using Trucker.Control.Zap.Catchee;
using Trucker.Model.Beacons;
using UnityEngine;

namespace Trucker.Control.Beacons
{
    [RequireComponent(typeof(BeaconAnchorPosProvider))]
    [RequireComponent(typeof(BeaconStatusProvider))]
    public class BeaconAnchorLock : MonoBehaviour
    {
        [SerializeField] private BeaconStatusProvider statusProvider;
        [SerializeField] private BeaconAnchorPosProvider anchorPosProvider;
        [SerializeField] private ZapCatchee catchee;
        [SerializeField] private NearestBeaconAnchor nearestBeaconAnchor;

        public bool Anchored
        {
            get => _anchored;
            private set
            {
                if(value == _anchored) return;
                _anchored = value;
                OnAnchored?.Invoke(value);
            }
        }
        private bool _anchored;
        public event Action<bool> OnAnchored;

        private void OnValidate()
        {
            statusProvider = GetComponent<BeaconStatusProvider>();
            anchorPosProvider = GetComponent<BeaconAnchorPosProvider>();
            catchee = GetComponentInParent<ZapCatchee>();
        }

        private void Awake()
        {
            catchee.OnFreed += CheckAnchorLock;
            statusProvider.OnBeaconStatusChange += OnBeaconStatusChange;
        }

        private void OnDestroy()
        {
            catchee.OnFreed -= CheckAnchorLock;
            statusProvider.OnBeaconStatusChange -= OnBeaconStatusChange;
        }

        private void Start()
        {
            CheckAnchorLock();
        }

        private void OnBeaconStatusChange(BeaconStatus obj) 
            => CheckAnchorLock();

        private void CheckAnchorLock()
        {
            if (HaveToUnlock)
                Unlock();
            else if(MightLock) 
                TryLock();
        }

        private bool HaveToUnlock 
            => Anchored && !InPosition;

        private bool MightLock 
            => !Anchored && InPosition && !catchee.Catched;

        private bool InPosition => statusProvider.Status == BeaconStatus.InPosition;

        private void Unlock()
        {
            nearestBeaconAnchor.Unlock(anchorPosProvider.Pos);
            anchorPosProvider.Unlock();
            catchee.SetFreeState();
            Anchored = false;
        }

        private void TryLock()
        {
            var currentAnchorPos = anchorPosProvider.Pos;

            if (nearestBeaconAnchor.TryLock(currentAnchorPos))
            {
                Lock(currentAnchorPos);
            }
        }

        private void Lock(Vector3 anchorPos)
        {
            anchorPosProvider.Lock(anchorPos);
            catchee.SetUnavailableState();
            Anchored = true;
        }

        public void InitLock(Vector3 pos) 
            => Lock(pos);
    }
}