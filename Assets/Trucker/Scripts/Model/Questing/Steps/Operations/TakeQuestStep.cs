using Trucker.Model.Questing.Quests;
using UnityEngine;

namespace Trucker.Model.Questing.Steps.Operations
{
    [CreateAssetMenu(menuName = "Quests/Operations/TakeQuestStep", fileName = "TakeQuestStep", order = 0)]
    public class TakeQuestStep : Operation
    {
        [SerializeField] private Quest quest;
        
        public override void Start()
        {
            quest.Take();
            onCompleted?.Invoke();
        }
    }
}