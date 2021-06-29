using UnityEngine;

namespace Trucker.Control.Zap
{
    public class JointAnchorConnection : MonoBehaviour
    {
        public Transform connectedBody;
        public Joint joint;

        private void FixedUpdate() 
            => joint.connectedAnchor = connectedBody.position;
    }
}