using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Control.Movement
{
    public class FollowTransformButStayOnOrbit : MonoBehaviour
    {
        [SerializeField] private FloatVariable orbitRadius;
        [SerializeField] private TransformVariable transformToFollow;
        
        private void FixedUpdate()
        {
            UpdatePosition();
        }

        public void UpdatePosition()
        {
            // Q is on (0,0,0), so there is no need to subtract it's position, for now
            transform.position = transformToFollow.Value.position.normalized * orbitRadius;
        }
    }
}