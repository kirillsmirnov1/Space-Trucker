using System;
using System.Collections.Generic;
using System.Linq;
using Trucker.Model.Questing.Consequences;
using Trucker.Model.Questing.Goals;
using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Model.Questing.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quests/Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        public static event Action<string> OnQuestTaken; 
        public static event Action<string> OnQuestFinished;
        public static event Action<string> OnQuestDropped; 
        
        public string title;
        public string description;
        [SerializeField] private bool finishedFromDialog;
        [SerializeField] private List<BoolVariable> conditions;
        [SerializeField] private List<Goal> goals;
        [SerializeField] private List<Consequence> consequences;
        
        public int currentGoalNumber = -1;

        public string GoalsText => GoalsTextFormatter.Format(goals, currentGoalNumber);

        public void Take()
        {
            Debug.Log($"Starting quest: {title}");
            GuaranteeCleanGoals();
            OnQuestTaken?.Invoke(title);
            StartGoal(0);
            // TODO when and how to subscribe to OnNotCompleted 
        }

        private void GuaranteeCleanGoals()
        {
            if (currentGoalNumber >= 0 && currentGoalNumber < goals.Count)
            {
                StopCurrentGoal();
            }
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
            StopCurrentGoal();
            IterateGoals();
        }

        private void StopCurrentGoal()
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
                if(!finishedFromDialog) Finish();
            }
        }

        public bool CanBeTaken
            => NotStarted && AllConditionsPass;

        private bool NotStarted 
            => currentGoalNumber == -1;

        private bool AllConditionsPass 
            => conditions.All(condition => condition == true);

        public bool CanBeFinished 
            => currentGoalNumber >= goals.Count;

        public void Finish()
        {
            currentGoalNumber = -1;
            InvokeConsequences();
            OnQuestFinished?.Invoke(title);
            Debug.Log($"Quest {title} finished");
        }

        public void Drop()
        {
            currentGoalNumber = -1;
            OnQuestDropped?.Invoke(title);
        }

        private void InvokeConsequences()
        {
            foreach (var consequence in consequences)
            {
                consequence.Invoke();
            }
        }
    }
}