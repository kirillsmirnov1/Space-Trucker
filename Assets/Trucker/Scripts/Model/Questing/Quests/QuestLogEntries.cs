using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Model.Questing.Quests
{
    [CreateAssetMenu(fileName = "QuestLogEntries", menuName = "Quests/QuestLogEntries", order = 0)]
    public class QuestLogEntries : XArrayVariable<QuestLogEntryIdentificator>
    {
        
    }
}