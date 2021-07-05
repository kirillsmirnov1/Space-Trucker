using System.Collections.Generic;
using System.Linq;
using Trucker.Control.Zap.Catchee;
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
        
        private LinkedList<ZapCatchee> catchees = new LinkedList<ZapCatchee>();
        public List<Vector3> CatcheesPositions => catchees.Select(x => x.transform.position).ToList();

        private Transform LastCatchedTransform 
            => catchees.Count > 0 ? catchees.Last.Value.transform : transform;
        
        private void OnValidate() => this.CheckNullFields();

        private void Awake() => UpdateCatcheesCount();
        
        public bool TryCatch(ZapCatchee zapCatchee)
        {
            if (catchees.Contains(zapCatchee)) return false;

            zapCatchee.SetConnection(springSettings, LastCatchedTransform);
            
            catchees.AddLast(zapCatchee);

            UpdateCatcheesCount();
            
            return true;
        }

        private void UpdateCatcheesCount() 
            => catcheesCount.Value = catchees.Count;

        public bool TryFree(ZapCatchee catchee)
        {
            if (!catchees.Contains(catchee)) return false;

            catchee.DestroyConnection();      

            ReconnectNextCatchee(catchee);

            catchees.Remove(catchee);
            
            UpdateCatcheesCount();
            return true;
        }

        private void ReconnectNextCatchee(ZapCatchee catchee)
        {
            var node = catchees.Find(catchee);
            
            var prev = node.Previous;
            var next = node.Next;

            next?.Value.SetConnectedBody(
                prev == null ? transform : prev.Value.transform);
        }
    }
}