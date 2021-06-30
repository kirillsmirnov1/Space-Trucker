using System.Collections.Generic;
using System.Linq;
using Trucker.Model.Zap;
using UnityEngine;
using UnityUtils;
using UnityUtils.Variables;

namespace Trucker.Control.Zap
{
    public class ZapCatcher : MonoBehaviour
    {
        [SerializeField] private SpringJointSettings springSettings;
        [SerializeField] private IntVariable catcheesCount;
        
        private List<ZapCatchee> catchees = new List<ZapCatchee>();
        public List<Vector3> CatcheesPositions => catchees.Select(x => x.transform.position).ToList();
        
        private void OnValidate() => this.CheckNullFields();

        private void Awake() => UpdateCatcheesCount();

        public bool TryCatch(ZapCatchee zapCatchee)
        {
            if (catchees.Contains(zapCatchee)) return false;
            
            var springJoint = SetSpringJoint(zapCatchee);
            SetAnchorConnection(zapCatchee, springJoint);
            catchees.Add(zapCatchee);

            UpdateCatcheesCount();
            
            return true;
        }

        private void UpdateCatcheesCount() 
            => catcheesCount.Value = catchees.Count;

        private void SetAnchorConnection(ZapCatchee zapCatchee, SpringJoint springJoint)
        {
            var bodyToConnectTo = catchees.Count > 0 ? catchees[catchees.Count - 1].transform : transform;
            var jointAnchorConnection = zapCatchee.gameObject.AddComponent<JointAnchorConnection>();
            jointAnchorConnection.joint = springJoint;
            jointAnchorConnection.connectedBody = bodyToConnectTo;
        }

        private SpringJoint SetSpringJoint(ZapCatchee zapCatchee)
        {
            var springJoint = zapCatchee.gameObject.AddComponent<SpringJoint>();

            springJoint.autoConfigureConnectedAnchor = springSettings.autoConfigureConnectedAnchor;
            springJoint.minDistance = springSettings.minDistance;
            springJoint.maxDistance = springSettings.maxDistance;
            springJoint.spring = springSettings.spring;
            springJoint.damper = springSettings.damper;
            return springJoint;
        }
    }
}