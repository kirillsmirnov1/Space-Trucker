using System.Collections.Generic;
using System.Linq;
using Trucker.Control.Zap.Catchee;
using Trucker.Model.Entities;
using Trucker.Model.Zap;
using UnityEngine;
using UnityUtils;

namespace Trucker.Control.Zap
{
    public class ZapCatcher : MonoBehaviour
    {
        [SerializeField] private SpringJointSettings springSettings;
        [SerializeField] private TypesOfCatchedObjects catcheeTypes;
        
        private LinkedList<ZapCatchee> catchees = new LinkedList<ZapCatchee>();
         
        public List<Vector3> CatcheesPositions => catchees.Select(x => x.transform.position).ToList();

        private Transform LastCatchedTransform 
            => catchees.Count > 0 ? catchees.Last.Value.transform : transform;
        
        private void OnValidate() => this.CheckNullFields();

        public bool TryCatch(ZapCatchee zapCatchee)
        {
            if (catchees.Contains(zapCatchee)) return false;

            zapCatchee.SetConnection(springSettings, LastCatchedTransform);
            
            catchees.AddLast(zapCatchee);

            catcheeTypes.ObjectCatched(zapCatchee);
            
            return true;
        }

        public bool TryFree(ZapCatchee catchee)
        {
            if (!catchees.Contains(catchee)) return false;

            catchee.DestroyConnection();      

            ReconnectNextCatchee(catchee);

            catchees.Remove(catchee);
            
            catcheeTypes.ObjectReleased(catchee);
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

        public ZapCatchee TryFree(EntityType typeToDestroy)
        {
            var catchee = catcheeTypes.GetCatcheeOfType(typeToDestroy);
            TryFree(catchee);
            return catchee;
        }
    }
}