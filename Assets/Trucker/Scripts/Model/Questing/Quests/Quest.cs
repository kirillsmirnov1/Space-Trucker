using System;
using System.Collections.Generic;
using Trucker.Model.Questing.Goals;
using UnityEngine;

namespace Trucker.Model.Questing.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quests/Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        public static event Action<string> OnQuestTaken; 
        public static event Action<string> OnQuestFinished;
        
        public string title;
        public string description;
        [SerializeField] private List<Goal> goals;

        public int currentGoalNumber = -1;

        public void Take()
        {
            Debug.Log($"Starting quest: {title}");
            OnQuestTaken?.Invoke(title);
            StartGoal(0);
            // TODO when and how to subscribe to OnNotCompleted 
        }

        public void StartGoal(int goalToStart)
        {
            currentGoalNumber = goalToStart;

            Debug.Log($"Starting goal: {CurrentGoal.description}");
            
            CurrentGoal.OnCompleted += OnGoalCompleted;
            CurrentGoal.Init();
        }

        private void OnGoalCompleted()
        {
            Debug.Log($"Completed goal: {CurrentGoal.description}");
            StopCompletedGoal();
            IterateGoals();
        }

        private void StopCompletedGoal()
        {
            CurrentGoal.Stop();
            CurrentGoal.OnCompleted -= OnGoalCompleted;
        }

        private Goal CurrentGoal => goals[currentGoalNumber];

        private void IterateGoals()
        {
            currentGoalNumber++;

            if (currentGoalNumber < goals.Count)
            {
                StartGoal(currentGoalNumber);
            }
            else
            {
                FinishQuest();
            }
        }

        private void FinishQuest()
        {
            currentGoalNumber = -1;
            OnQuestFinished?.Invoke(title);
            Debug.Log($"Quest {title} finished");
            // TODO give reward
            // TODO consequences (like taking objects from zap catcher)
        }
    }
}