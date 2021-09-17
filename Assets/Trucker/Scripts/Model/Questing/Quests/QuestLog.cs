using System.Collections.Generic;
using System.Linq;
using UnityUtils.Saves;
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
                takenQuest.StartQuest();
            }
        }

        private void SubscribeOnCallbacks()
        {
            Quest.OnQuestTaken += OnQuestTaken;
            Quest.OnQuestStop += OnQuestStop;
        }

        private void OnQuestTaken(string title)
        {
            questLogEntries.QuestTaken(title);
        }

        private void OnQuestStop(string title, QuestStatus questStatus)
        {
            questLogEntries.QuestStopped(title, questStatus);
        }
    }
}