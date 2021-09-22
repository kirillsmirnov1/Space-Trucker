using UnityEngine;
using UnityUtils.Saves;
using UnityUtils.Variables;

namespace Trucker.Model.Beacons
{
    [CreateAssetMenu(menuName = "Logic/NearestBeaconAnchor", fileName = "NearestBeaconAnchor", order = 0)]
    public class NearestBeaconAnchor : InitiatedScriptableObject // IMPR that's actually beacon's anchors data 
    {
        [SerializeField] private Vector3ArrayVariable anchorPositions;
        [SerializeField] private BoolArrayVariable anchorsLocked;
        
        private Vector3[] _anchors;
        
        private static readonly object Lock = new object();
        
        public override void Init()
        {
            _anchors = anchorPositions.Value;
        }

        public Vector3 NearestAnchorFor(Vector3 targetPos)
        {
            var bestDistance = float.MaxValue;
            var nearestAnchor = Vector3.zero;
            
            for (int i = 0; i < _anchors.Length; i++)
            {
                if(anchorsLocked[i]) continue;
                var anchor = _anchors[i];
                var distance = Vector3.Distance(targetPos, anchor); 
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    nearestAnchor = anchor;
                }
            }

            return nearestAnchor;
        }

        public bool TryLock(Vector3 anchorPos)
        {
            lock (Lock)
            {
                var index = FindAnchorIndex(anchorPos);

                if (index == -1) return false;
                
                if (anchorsLocked[index]) return false;
                
                anchorsLocked[index] = true;
                return true;
            }
        }

        public void Unlock(Vector3 anchorPos)
        {
            lock (Lock)
            {
                var index = FindAnchorIndex(anchorPos);
                if (index == -1) return;
                anchorsLocked[index] = false;
            }
        }

        private int FindAnchorIndex(Vector3 anchorPos)
        {
            var index = -1;
            
            for (int i = 0; i < _anchors.Length; i++)
            {
                if (anchorPos.Equals(_anchors[i]))
                {
                    index = i;
                    break;
                }
            }

            return index;
        }
    }
}