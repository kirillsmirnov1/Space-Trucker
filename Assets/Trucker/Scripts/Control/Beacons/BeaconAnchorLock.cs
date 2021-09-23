using Trucker.Control.Zap.Catchee;
using Trucker.Model.Beacons;
using UnityEngine;
using UnityUtils;

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

        public Observable<bool> anchored;

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
            => anchored && !InPosition;

        private bool MightLock 
            => !anchored && InPosition && !catchee.Catched;

        private bool InPosition => statusProvider.Status == BeaconStatus.InPosition;

        private void Unlock()
        {
            nearestBeaconAnchor.Unlock(anchorPosProvider.Pos);
            anchorPosProvider.Unlock();
            catchee.SetFreeState();
            anchored.Value = false;
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
            anchored.Value = true;
        }

        public void InitLock(Vector3 pos) 
            => Lock(pos);
    }
}