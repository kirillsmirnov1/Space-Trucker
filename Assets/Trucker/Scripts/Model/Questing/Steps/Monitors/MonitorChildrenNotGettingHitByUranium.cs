using Trucker.Control.Entities;
using Trucker.Model.Zap.Level;
using UnityEngine;

namespace Trucker.Model.Questing.Steps.Monitors
{
    public class MonitorChildrenNotGettingHitByUranium : Monitor
    {
        [SerializeField] private ZapLevelVariable zapLevelVariable;
        
        public override void Start()
        {
            if(HasZapProtection) return;
            UraniumHitWatcher.OnUraniumHit += FailQuest;
        }

        public override void Stop()
        {
            if(HasZapProtection) return;
            UraniumHitWatcher.OnUraniumHit -= FailQuest;
        }

        private bool HasZapProtection => zapLevelVariable.Value == ZapLevel.Plus;

        private void FailQuest()
        {
            quest.Fail();
            InvokeConsequences();
        }
    }
}