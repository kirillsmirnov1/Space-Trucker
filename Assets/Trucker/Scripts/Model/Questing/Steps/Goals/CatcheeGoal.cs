using System.Linq;
using Trucker.Model.Entities;
using Trucker.Model.Zap;
using UnityEngine;

namespace Trucker.Model.Questing.Steps.Goals
{
    [CreateAssetMenu(fileName = "CatcheeGoal", menuName = "Quests/Goals/Catchee Goal", order = 0)]
    public class CatcheeGoal : Goal
    {
        [SerializeField] private EntityType[] targetCatcheeTypes;
        [SerializeField] private int requiredAmount;
        
        [SerializeField] private TypesOfCatchedObjects catchedByTypes;

        public override void Start()
        {
            base.Start();
            catchedByTypes.OnChange += OnTypeCountChange;
        }

        public override void Stop()
        {
            base.Stop();
            catchedByTypes.OnChange -= OnTypeCountChange;
        }

        private void OnTypeCountChange(EntityType changedType)
        {
            if(!targetCatcheeTypes.Contains(changedType)) return;
            CheckGoal();
        }

        private void CheckGoal()
        {
            var count = catchedByTypes.Count(targetCatcheeTypes);
            if (count >= requiredAmount)
            {
                Complete();
            }
        }
    }
}