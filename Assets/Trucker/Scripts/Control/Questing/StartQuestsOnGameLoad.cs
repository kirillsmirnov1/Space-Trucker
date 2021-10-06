using Trucker.Model.Questing.Quests;
using UnityEngine;
using UnityUtils.VisualEffects;

namespace Trucker.Control.Questing
{
    public class StartQuestsOnGameLoad : MonoBehaviour
    {
        [SerializeField] private QuestLog questLog;
        [SerializeField] private Quest firstQuest;
        [SerializeField] private UiFadePanel titleScreen;
        
        private void Start()
        {
            StartQuests();
            PerformFirstQuestCheck();
            titleScreen.Hide();
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