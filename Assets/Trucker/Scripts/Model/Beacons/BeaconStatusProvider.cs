using System;
using UnityEngine;

namespace Trucker.Model.Beacons
{
    public class BeaconStatusProvider : MonoBehaviour
    {
        public event Action<BeaconStatus> OnBeaconStatusChange;

        [SerializeField] private NearestBeaconAnchor nearestAnchor;
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

        private void Update() 
            => UpdateStatus();

        private void UpdateStatus()
        {
            var dist = Vector3.Distance(nearestAnchor.NearestAnchor(), transform.position);

            if (dist < inPosDistance) Status = BeaconStatus.InPosition;
            else if (dist < nearDistance) Status = BeaconStatus.Near;
            else Status = BeaconStatus.Far;
        }
    }
}