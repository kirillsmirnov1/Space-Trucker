using Trucker.Model.Questing.Quests;
using UnityEngine;

namespace Trucker.Model.Questing.Steps.Operations
{
    [CreateAssetMenu(menuName = "Quests/Operations/FailQuestStep", fileName = "FailQuestStep", order = 0)]
    public class FailQuestStep : Operation
    {
        [SerializeField] private Quest quest;
        
        public override void Start()
        {
            quest.Fail();
            onCompleted?.Invoke();
        }
    }
}