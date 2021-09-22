using System;
using UnityEngine;

namespace Trucker.Model.Beacons
{
    [RequireComponent(typeof(AnchorPosProvider))]
    public class BeaconStatusProvider : MonoBehaviour
    {
        public event Action<BeaconStatus> OnBeaconStatusChange;

        [SerializeField] private AnchorPosProvider anchorPosProvider;
        [SerializeField] private float inPosDistance = 500f; // TODO use vars 
        [SerializeField] private float nearDistance = 1500f;

        private BeaconStatus _status;
        public BeaconStatus Status
        {
            get => _status;
            set
            {
                if(value == _status) return;
                _status = value;
                OnBeaconStatusChange?.Invoke(_status);
            }
        }

        private void OnValidate()
        {
            anchorPosProvider = GetComponent<AnchorPosProvider>();
        }

        private void Update() 
            => UpdateStatus();

        private void UpdateStatus()
        {
            var dist = Vector3.Distance(anchorPosProvider.Pos, transform.position);

            if (dist < inPosDistance) Status = BeaconStatus.InPosition;
            else if (dist < nearDistance) Status = BeaconStatus.Near;
            else Status = BeaconStatus.Far;
        }
    }
}