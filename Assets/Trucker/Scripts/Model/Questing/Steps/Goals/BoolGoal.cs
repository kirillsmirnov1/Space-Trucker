using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Model.Questing.Steps.Goals
{
    [CreateAssetMenu(menuName = "Quests/Goals/BoolGoal", fileName = "BoolGoal", order = 0)]
    public class BoolGoal : Goal
    {
        [SerializeField] private BoolVariable variable;
        [SerializeField] private bool requiredValue = true;

        public override void Start()
        {
            base.Start();
            variable.OnChange += Check;
            Check(variable);
        }

        public override void Stop()
        {
            base.Stop();
            variable.OnChange -= Check;
        }

        private void Check(bool newValue) 
            => Completed = newValue == requiredValue;
    }
}