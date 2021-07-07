using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Model.Questing.Goals
{
    [CreateAssetMenu(fileName = "Integer Goal", menuName = "Quests/Goals/Integer Goal", order = 0)]
    public class IntegerGoal : Goal
    {
        [SerializeField] private int requiredValue;
        [SerializeField] private IntVariable currentValue;

        public override void Init()
        {
            base.Init();
            CheckGoal(currentValue);
            currentValue.OnChange += CheckGoal;
        }

        public override void Stop()
        {
            base.Stop();
            currentValue.OnChange -= CheckGoal;
        }

        private void CheckGoal(int value)
        {
            if (value >= requiredValue)
            {
                Complete();
            }
        }
    }
}