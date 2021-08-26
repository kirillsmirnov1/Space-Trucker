using UnityEngine;
using UnityUtils.Events;

namespace Trucker.Model.Questing.Steps.Operations
{
    [CreateAssetMenu(menuName = "Quests/Operations/InvokeGameEventWithCallback", fileName = "InvokeGameEventWithCallback", order = 0)]
    public class InvokeActionGameEvent : Operation
    {
        [SerializeField] private ActionGameEvent actionGameEvent;
        
        public override void Start()
        {
            actionGameEvent.Raise(onCompleted);
        }
    }
}