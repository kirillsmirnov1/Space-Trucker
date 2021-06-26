using System.Collections.Generic;
using UnityEngine;

namespace Trucker.Control.Zap
{
    public class ZapCatcher : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        private List<ZapCatchee> catchees = new List<ZapCatchee>();

        public bool TryCatch(ZapCatchee zapCatchee)
        {
            if (catchees.Contains(zapCatchee)) return false;
            
            var bodyToConnectTo = catchees.Count > 0 ? catchees[catchees.Count - 1].Rigidbody : rb;
            var springJoint = zapCatchee.gameObject.AddComponent<SpringJoint>();

            springJoint.connectedBody = bodyToConnectTo;
            springJoint.autoConfigureConnectedAnchor = false;
            springJoint.connectedAnchor = Vector3.zero;
            springJoint.minDistance = 1f;
            springJoint.maxDistance = 2f;
            springJoint.enableCollision = true;
            
            catchees.Add(zapCatchee);
            
            return true;
        }
    }
}