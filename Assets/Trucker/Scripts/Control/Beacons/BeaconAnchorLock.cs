using Trucker.Control.Zap.Catchee;
using Trucker.Model.Beacons;
using UnityEngine;

namespace Trucker.Control.Beacons
{
    [RequireComponent(typeof(AnchorPosProvider))]
    [RequireComponent(typeof(BeaconStatusProvider))]
    public class BeaconAnchorLock : MonoBehaviour
    {
        [SerializeField] private BeaconStatusProvider statusProvider;
        [SerializeField] private AnchorPosProvider anchorPosProvider;
        [SerializeField] private ZapCatchee catchee;
        [SerializeField] private NearestBeaconAnchor nearestBeaconAnchor;

        public bool Anchored { get; private set; }

        private void OnValidate()
        {
            statusProvider = GetComponent<BeaconStatusProvider>();
            anchorPosProvider = GetComponent<AnchorPosProvider>();
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
            Anchored = false;
            nearestBeaconAnchor.Unlock(anchorPosProvider.Pos);
            anchorPosProvider.Unlock();
            // TODO unlock catchee 
        }

        private void TryLock()
        {
            var currentAnchorPos = anchorPosProvider.Pos;

            if (nearestBeaconAnchor.TryLock(currentAnchorPos))
            {
                Anchored = true;
                anchorPosProvider.Lock(currentAnchorPos);
                // TODO lock catchee 
            }
        }
    }
}