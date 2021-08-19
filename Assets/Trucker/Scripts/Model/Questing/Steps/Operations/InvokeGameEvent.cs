using UnityEngine;
using UnityUtils.Events;

namespace Trucker.Model.Questing.Steps.Operations
{
    [CreateAssetMenu(menuName = "Quests/Operations/InvokeGameEvent", fileName = "InvokeGameEvent", order = 0)]
    public class InvokeGameEvent : Operation
    {
        [SerializeField] private GameEvent gameEvent;
        
        public override void Start()
        {
            gameEvent.Raise();
            onCompleted?.Invoke();
        }
    }
}