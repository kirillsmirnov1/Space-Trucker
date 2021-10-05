using System;
using Trucker.Model.Questing.Quests;
using UnityEngine;

namespace Trucker.Control.Questing
{
    public class StartQuestsOnGameLoad : MonoBehaviour
    {
        [SerializeField] private QuestLog questLog;

        private void Start()
        {
            StartQuests();
        }

        private void StartQuests()
        {
            questLog.StartTakenQuests();
        }
    }
}