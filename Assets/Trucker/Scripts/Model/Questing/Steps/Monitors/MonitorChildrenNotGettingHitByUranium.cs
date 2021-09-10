using Trucker.Control.Entities;
using Trucker.Model.Zap.Level;
using UnityEngine;

namespace Trucker.Model.Questing.Steps.Monitors
{
    [CreateAssetMenu(menuName = "Quests/Monitors/MonitorChildrenNotGettingHitByUranium", fileName = "MonitorChildrenNotGettingHitByUranium", order = 0)]
    public class MonitorChildrenNotGettingHitByUranium : Monitor
    {
        [SerializeField] private ZapLevelVariable zapLevelVariable;
        
        public override void Start()
        {
            UraniumHitWatcher.OnUraniumHit += FailQuest;
        }

        public override void Stop()
        {
            UraniumHitWatcher.OnUraniumHit -= FailQuest;
        }

        private bool HasZapProtection => zapLevelVariable.Value == ZapLevel.Plus;

        private void FailQuest()
        {
            if(HasZapProtection) return;
            quest.Fail();
            InvokeConsequences();
        }
    }
}