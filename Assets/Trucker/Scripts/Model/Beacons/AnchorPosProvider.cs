using UnityEngine;

namespace Trucker.Model.Beacons
{
    public class AnchorPosProvider : MonoBehaviour
    {
        [SerializeField] private NearestBeaconAnchor nearestAnchor;

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
            protected AnchorPosProvider posProvider;

            public AnchorProviderState(AnchorPosProvider posProvider)
            {
                this.posProvider = posProvider;
                Debug.Log($"{posProvider.transform.parent.name} is {GetType().Name}");
            }

            public abstract Vector3 Pos { get; }
        }

        private class Free : AnchorProviderState
        {
            public Free(AnchorPosProvider posProvider) : base(posProvider) { }
            public override Vector3 Pos 
                => posProvider.nearestAnchor.NearestAnchor();
        }

        private class Locked : AnchorProviderState
        {
            private readonly Vector3 _anchorPos;
            
            public Locked(AnchorPosProvider posProvider, Vector3 anchorPos) : base(posProvider) 
                => _anchorPos = anchorPos;

            public override Vector3 Pos 
                => _anchorPos;
        }
        
        
    }
}