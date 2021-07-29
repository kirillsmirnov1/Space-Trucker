using Trucker.Model.Entities;
using Trucker.Model.Zap;
using UnityEngine;

namespace Trucker.Model.Questing.Goals
{
    [CreateAssetMenu(fileName = "CatcheeGoal", menuName = "Quests/Goals/Catchee Goal", order = 0)]
    public class CatcheeGoal : Goal
    {
        [SerializeField] private EntityType targetCatcheeType;
        [SerializeField] private int requiredAmount;
        
        [SerializeField] private TypesOfCatchedObjects catchedByTypes;

        public override void Init()
        {
            base.Init();
            CheckGoal(targetCatcheeType, catchedByTypes.Count(targetCatcheeType));
            catchedByTypes.OnChange += CheckGoal;
        }

        public override void Stop()
        {
            base.Stop();
            catchedByTypes.OnChange -= CheckGoal;
        }

        private void CheckGoal(EntityType changedType, int count)
        {
            if(targetCatcheeType != changedType) return;
            if (count >= requiredAmount)
            {
                Complete();
            }
        }
    }
}