using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Model.Beacons
{
    public class BeaconAnchorPosProvider : MonoBehaviour
    {
        [SerializeField] private NearestBeaconAnchor nearestAnchor;
        [SerializeField] private BoolVariable logBeaconLockChange;
        
        private AnchorProviderState _state;

        public Vector3 Pos => _state.Pos;

        private void Awake() 
            => Unlock();

        public void Lock(Vector3 lockPos) 
            => _state = new Locked(this, lockPos);

        public void Unlock() 
            => _state = new Free(this);

        private abstract class AnchorProviderState
        {
            protected readonly BeaconAnchorPosProvider posProvider;

            protected AnchorProviderState(BeaconAnchorPosProvider posProvider)
            {
                this.posProvider = posProvider;
                LogStateChange();
            }

            private void LogStateChange()
            {
                if (posProvider.logBeaconLockChange)
                {
                    Debug.Log($"{posProvider.transform.parent.name} is {GetType().Name}");
                }
            }

            public abstract Vector3 Pos { get; }
        }

        private class Free : AnchorProviderState
        {
            public Free(BeaconAnchorPosProvider posProvider) : base(posProvider) { }
            public override Vector3 Pos 
                => posProvider.nearestAnchor.NearestAnchorFor(posProvider.transform.position);
        }

        private class Locked : AnchorProviderState
        {
            private readonly Vector3 _anchorPos;
            
            public Locked(BeaconAnchorPosProvider posProvider, Vector3 anchorPos) : base(posProvider) 
                => _anchorPos = anchorPos;

            public override Vector3 Pos 
                => _anchorPos;
        }
        
        
    }
}