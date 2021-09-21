using System.Linq;
using UnityEngine;
using UnityUtils.Saves;
using UnityUtils.Variables;

namespace Trucker.Model.Beacons
{
    [CreateAssetMenu(menuName = "Logic/NearestBeaconAnchor", fileName = "NearestBeaconAnchor", order = 0)]
    public class NearestBeaconAnchor : InitiatedScriptableObject
    {
        [SerializeField] private TransformVariable playersTransformVariable;
        [SerializeField] private Vector3Variable[] anchorPositions;

        private Transform _player;
        private Vector3[] _anchors;
        
        public override void Init()
        {
            _player = playersTransformVariable;
            _anchors = anchorPositions.Select(anchorPosVariable => anchorPosVariable.Value).ToArray();
        }

        public Vector3 NearestAnchor() 
            => PickNearestAnchorFrom(_anchors); // TODO pass available ones 

        private Vector3 PickNearestAnchorFrom(Vector3[] anchors)
        {
            var playerPos = _player.position;
            var bestDistance = float.MaxValue;
            var nearestAnchor = Vector3.zero;
            
            for (int i = 0; i < anchors.Length; i++)
            {
                var anchor = anchors[i];
                var distance = Vector3.Distance(playerPos, anchor); 
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    nearestAnchor = anchor;
                }
            }

            return nearestAnchor;
        }
    }
}