using System.Collections.Generic;
using UnityEngine;

namespace Trucker.Control.Zap
{
    public class ZapCatcher : MonoBehaviour
    {
        private List<ZapCatchee> catchees = new List<ZapCatchee>();

        public bool TryCatch(ZapCatchee zapCatchee)
        {
            if (catchees.Contains(zapCatchee)) return false;
            
            var bodyToConnectTo = catchees.Count > 0 ? catchees[catchees.Count - 1].transform : transform;
            var springJoint = zapCatchee.gameObject.AddComponent<SpringJoint>();

            // TODO extract to SO 
            springJoint.autoConfigureConnectedAnchor = false;
            springJoint.minDistance = 1f;
            springJoint.maxDistance = 2f;
            springJoint.enableCollision = true;
            springJoint.spring = 3;
            springJoint.damper = 2;

            var jointAnchorConnection = zapCatchee.gameObject.AddComponent<JointAnchorConnection>();
            jointAnchorConnection.joint = springJoint;
            jointAnchorConnection.connectedBody = bodyToConnectTo;
            
            catchees.Add(zapCatchee);
            
            return true;
        }
    }
}