using System.Linq;
using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Control.Beacons
{
    public class BeaconsAnchorLockInit : MonoBehaviour
    {
        [SerializeField] private Vector3ArrayVariable anchorPos;
        [SerializeField] private BoolArrayVariable anchorLocked;
        [SerializeField] private BeaconAnchorLock[] beacons;

        private Vector3[] _initialPositions;
        
        private void OnValidate()
        {
            beacons = GetComponentsInChildren<BeaconAnchorLock>();
        }

        private void Awake()
        {
            SaveBeaconsInitialPositions();
        }

        private void Start()
        {
            PositionBeacons();
        }

        private void SaveBeaconsInitialPositions()
        {
            _initialPositions = beacons.Select(beacon => beacon.transform.parent.position).ToArray();
        }

        private void PositionBeacons()
        {
            for (int i = 0; i < beacons.Length; i++)
            {
                if (anchorLocked[i])
                {
                    var pos = anchorPos[i];
                    beacons[i].transform.parent.position = pos;
                    beacons[i].InitLock(pos);
                }
            }    
        }

        public void ResetBeaconsLocks()
        {
            for (int i = 0; i < beacons.Length; i++)
            {
                beacons[i].transform.parent.position = _initialPositions[i];
            }
        }
    }
}