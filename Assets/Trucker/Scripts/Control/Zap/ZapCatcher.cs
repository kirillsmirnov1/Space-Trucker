using System.Collections.Generic;
using System.Linq;
using Trucker.Control.Zap.Catchee;
using Trucker.Model.Entities;
using Trucker.Model.Zap;
using Trucker.Model.Zap.Level;
using UnityEngine;
using UnityUtils;

namespace Trucker.Control.Zap
{
    public class ZapCatcher : MonoBehaviour
    {
        [SerializeField] private SpringJointSettings springSettings;
        [SerializeField] private TypesOfCatchedObjects catcheeTypes;
        [SerializeField] private ZapLevelVariable zapLevelVariable;
        
        private readonly LinkedList<ZapCatchee> catchees = new LinkedList<ZapCatchee>();
        public ZapCatchee[] Catchees => catchees.ToArray();
         
        public List<Vector3> CatcheesPositions => catchees.Select(x => x.transform.position).ToList();

        private bool HasCatchees 
            => catchees.Count > 0;

        private ZapCatchee LastCatchee 
            => catchees.Last.Value;

        private Transform TailTransform 
            => HasCatchees ? LastCatchee.transform : transform;

        private float TailSafeRadius
            => HasCatchees ? LastCatchee.SafeRadius : 1.31f;
        
        private void OnValidate() => this.CheckNullFields();
        private void Awake() => zapLevelVariable.OnChange += CheckConsistency;
        private void OnDestroy() => zapLevelVariable.OnChange -= CheckConsistency;

        public void TryCatch(ZapCatchee zapCatchee)
        {
            if (!catchees.Contains(zapCatchee))
            {
                zapCatchee.SetConnection(springSettings, TailTransform, TailSafeRadius);
                catchees.AddLast(zapCatchee);
                catcheeTypes.ObjectCatched(zapCatchee);
                zapCatchee.OnCatchAttempt(true);
            }
            else
            {
                zapCatchee.OnCatchAttempt(false);
            }
        }

        public void TryFree(ZapCatchee catchee)
        {
            if (catchees.Contains(catchee))
            {
                catchee.DestroyConnection();
                ReconnectNextCatchee(catchee);
                catchees.Remove(catchee);
                catcheeTypes.ObjectReleased(catchee);
                catchee.OnFreeAttempt(true);
            }
            else
            {
                catchee.OnFreeAttempt(false);
            }
        }

        private void ReconnectNextCatchee(ZapCatchee catchee)
        {
            var node = catchees.Find(catchee);
            
            var prev = node.Previous;
            var next = node.Next;

            next?.Value.SetConnectedBody(
                prev == null ? transform : prev.Value.transform);
        }

        public ZapCatchee[] TryFree(EntityType[] typesToDestroy, int count)
        {
            var catcheesToFree = catcheeTypes.GetCatcheesOfTypes(typesToDestroy).Take(count).ToArray();
            foreach (var catchee in catcheesToFree)
            {
                TryFree(catchee);
            }

            return catcheesToFree;
        }

        private void CheckConsistency(ZapLevel newLevel)
        {
            var allowedCount = (int) newLevel;
            if (allowedCount < catchees.Count)
            {
                var freeCount = catchees.Count - allowedCount;
                var catcheesToFree = catchees.Take(freeCount).ToList();
                foreach (var catchee in catcheesToFree)
                {
                    TryFree(catchee);
                }
            }
        }
    }
}