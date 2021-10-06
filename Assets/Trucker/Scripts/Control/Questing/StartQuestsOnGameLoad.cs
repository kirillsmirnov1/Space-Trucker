using Trucker.Model.Questing.Quests;
using UnityEngine;

namespace Trucker.Control.Questing
{
    public class StartQuestsOnGameLoad : MonoBehaviour
    {
        [SerializeField] private QuestLog questLog;
        [SerializeField] private Quest firstQuest;

        private void Start()
        {
            StartQuests();
            PerformFirstQuestCheck();
            // TODO hide title screen 
        }

        private void StartQuests()
        {
            questLog.StartTakenQuests();
        }

        private void PerformFirstQuestCheck()
        {
            if (QuestLogEntries.NeverBeenStarted(firstQuest.title))
            {
                firstQuest.Take();
            }
        }
    }
}