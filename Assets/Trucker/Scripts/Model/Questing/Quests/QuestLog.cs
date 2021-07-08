using System.Collections.Generic;
using System.Linq;
using Trucker.Model.Util;
using UnityEngine;
using UnityUtils;

namespace Trucker.Model.Questing.Quests
{
    [CreateAssetMenu(fileName = "QuestLog", menuName = "Quests/Log", order = 0)]
    public class QuestLog : InitiatedScriptableObject
    {
        [SerializeField] private QuestsIndex questsIndex;
        [SerializeField] private QuestLogEntries questLogEntries;

        private void OnValidate() 
            => this.CheckNullFields();

        public List<Quest> Taken
        {
            get
            {
                return questLogEntries.Value
                    .Where(entry => entry.status == QuestStatus.Taken)
                    .Select(entry => questsIndex[entry.title])
                    .ToList();
            }
        }
        
        public override void Init()
        {
            SubscribeOnCallbacks();
            InitQuests();
        }

        private void InitQuests()
        {
            foreach (var takenQuest in Taken)
            {
                takenQuest.StartGoal(0);
            }
        }

        private void SubscribeOnCallbacks()
        {
            Quest.OnQuestTaken += OnQuestTaken;
            Quest.OnQuestFinished += OnQuestFinished;
        }

        private void OnQuestTaken(string title)
        {
            questLogEntries.QuestTaken(title);
        }

        private void OnQuestFinished(string title)
        {
            questLogEntries.QuestFinished(title);
        }
    }
}