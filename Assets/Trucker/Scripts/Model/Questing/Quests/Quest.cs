using System;
using System.Collections.Generic;
using System.Linq;
using Trucker.Model.Questing.Consequences;
using Trucker.Model.Questing.Steps;
using Trucker.Model.Questing.Steps.Goals;
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
        [TextArea(1, 20)] public string description;
        [SerializeField] private bool finishedFromDialog;
        [SerializeField] private List<BoolVariable> conditions;
        [SerializeField] private List<Step> steps;
        [SerializeField] private List<Consequence> consequences;
        
        public int currentStepNumber = -1;

        public string GoalsText => GoalsTextFormatter.Format(Goals, currentStepNumber);
        private List<Goal> Goals => steps.Where(step => step.Type == StepType.Goal).Select(step => (Goal)step).ToList();

        public void Take()
        {
            Debug.Log($"Starting quest: {title}");
            GuaranteeCleanSteps();
            OnQuestTaken?.Invoke(title);
            StartStep(0);
        }

        private void GuaranteeCleanSteps()
        {
            if (currentStepNumber >= 0 && currentStepNumber < steps.Count)
            {
                StopCurrentStep();
            }
        }

        public void StartStep(int stepToStart)
        {
            currentStepNumber = stepToStart;

            Debug.Log($"Starting step: {CurrentStep.name}");
            
            CurrentStep.onCompleted += OnStepCompleted;
            CurrentStep.Start();
        }

        private void OnStepCompleted()
        {
            Debug.Log($"Completed step: {CurrentStep.name}");
            StopCurrentStep();
            IterateSteps();
        }

        private void StopCurrentStep()
        {
            CurrentStep.Stop();
            CurrentStep.onCompleted -= OnStepCompleted;
        }

        private Step CurrentStep => steps[currentStepNumber];

        private void IterateSteps()
        {
            currentStepNumber++;

            if (currentStepNumber < steps.Count)
            {
                StartStep(currentStepNumber);
            }
            else
            {
                if(!finishedFromDialog) Finish();
            }
        }

        public bool NeverBeenStarted // IMPR naming 
            => QuestLogEntries.NeverBeenStarted(title);
        
        public bool CanBeTaken
            => NotInProgress && AllConditionsPass;

        private bool NotInProgress 
            => currentStepNumber == -1;

        private bool AllConditionsPass 
            => conditions.All(condition => condition == true);

        public bool CanBeFinished 
            => currentStepNumber >= steps.Count;

        public void Finish()
        {
            currentStepNumber = -1;
            InvokeConsequences();
            OnQuestFinished?.Invoke(title);
            Debug.Log($"Quest {title} finished");
        }

        public void Drop()
        {
            currentStepNumber = -1;
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