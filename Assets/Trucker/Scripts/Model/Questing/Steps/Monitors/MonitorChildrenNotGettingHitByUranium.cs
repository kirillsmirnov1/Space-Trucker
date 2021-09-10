using Trucker.Control.Entities;
using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Model.Questing.Steps.Monitors
{
    [CreateAssetMenu(menuName = "Quests/Monitors/MonitorChildrenNotGettingHitByUranium", fileName = "MonitorChildrenNotGettingHitByUranium", order = 0)]
    public class MonitorChildrenNotGettingHitByUranium : Monitor
    {
        [SerializeField] private BoolVariable zapProtectionEnabled;
        
        public override void Start()
        {
            UraniumHitWatcher.OnUraniumHit += FailQuest;
        }

        public override void Stop()
        {
            UraniumHitWatcher.OnUraniumHit -= FailQuest;
        }

        private bool HasZapProtection => zapProtectionEnabled.Value;

        private void FailQuest()
        {
            if(HasZapProtection) return;
            quest.Fail();
            InvokeConsequences();
        }
    }
}