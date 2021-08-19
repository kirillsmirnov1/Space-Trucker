using UnityEngine;
using UnityUtils.Events;

namespace Trucker.Model.Questing.Steps.Goals
{
    [CreateAssetMenu(fileName = "Event Goal", menuName = "Quests/Goals/Event Goal", order = 0)]
    public class EventGoal : Goal
    {
        [SerializeField] private GameEvent gameEvent;

        public override void Start()
        {
            base.Start();
            gameEvent.RegisterAction(Complete);
        }

        public override void Stop()
        {
            base.Stop();
            gameEvent.UnregisterAction(Complete);
        }
    }
}