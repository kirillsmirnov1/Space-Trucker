using System.Collections.Generic;
using Trucker.Model.Zap;
using UnityEngine;
using UnityUtils;

namespace Trucker.Control.Zap
{
    public class ZapCatcher : MonoBehaviour
    {
        [SerializeField] private SpringJointSettings springSettings;
        
        private List<ZapCatchee> catchees = new List<ZapCatchee>();

        private void OnValidate() => this.CheckNullFields();

        public bool TryCatch(ZapCatchee zapCatchee)
        {
            if (catchees.Contains(zapCatchee)) return false;
            
            var bodyToConnectTo = catchees.Count > 0 ? catchees[catchees.Count - 1].transform : transform;
            var springJoint = zapCatchee.gameObject.AddComponent<SpringJoint>();
            
            springJoint.autoConfigureConnectedAnchor = springSettings.autoConfigureConnectedAnchor;
            springJoint.minDistance = springSettings.minDistance;
            springJoint.maxDistance = springSettings.maxDistance;
            springJoint.spring = springSettings.spring;
            springJoint.damper = springSettings.damper;

            var jointAnchorConnection = zapCatchee.gameObject.AddComponent<JointAnchorConnection>();
            jointAnchorConnection.joint = springJoint;
            jointAnchorConnection.connectedBody = bodyToConnectTo;
            
            catchees.Add(zapCatchee);
            
            return true;
        }
    }
}