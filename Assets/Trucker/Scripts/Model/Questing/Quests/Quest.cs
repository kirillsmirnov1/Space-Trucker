using System;
using System.Collections.Generic;
using System.Linq;
using Trucker.Model.Questing.Steps;
using Trucker.Model.Questing.Steps.Monitors;
using Trucker.Model.Questing.Steps.Operations;
using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Model.Questing.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quests/Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        public static event Action<string> OnQuestTaken; 
        public static event Action<string, QuestStatus> OnQuestStop;
        
        public string title;
        [TextArea(1, 20)] public string description;
        [SerializeField] private bool finishedFromDialog;
        [SerializeField] private List<BoolVariable> conditions;
        [SerializeField] private List<Monitor> monitors;
        [SerializeField] private List<Step> steps;
        [SerializeField] private List<Operation> consequences;
        
        public int currentStepNumber = -1;

        [Header("Debug")]
        [SerializeField] private BoolVariable logQuestSteps;
        
        public string GoalsText => GoalsTextFormatter.Format(steps);

        public void Take()
        {
            Log($"Starting quest: {title}");
            OnQuestTaken?.Invoke(title);
            StartQuest();
        }

        public void StartQuest()
        {
            InitMonitors();
            GuaranteeCleanSteps();
            StartStep(0);
        }

        private void InitMonitors()
        {
            foreach (var monitor in monitors)
            {
                monitor.Init(this);
            }
        }

        private void GuaranteeCleanSteps()
        {
            if (currentStepNumber >= 0 && currentStepNumber < steps.Count)
            {
                StopCurrentStep();
            }

            foreach (var step in steps)
            {
                step.Init();
            }
        }

        private void StartStep(int stepToStart)
        {
            currentStepNumber = stepToStart;

            Log($"Starting step: {CurrentStep.name}");
            
            CurrentStep.onCompleted += OnStepCompleted;
            CurrentStep.Start();
        }

        private void OnStepCompleted()
        {
            Log($"Completed step: {CurrentStep.name}");
            StopCurrentStep();
            IterateSteps();
        }

        private void StopCurrentStep()
        {
            if(currentStepNumber == -1) return;
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
            => NotInProgress && AllConditionsPass && NotFailed;

        private bool NotFailed 
            => QuestLogEntries.NotFailed(title);

        public bool InProgress
            => currentStepNumber != -1;

        private bool NotInProgress 
            => !InProgress;

        private bool AllConditionsPass 
            => conditions.All(condition => condition == true);

        public bool CanBeFinished 
            => currentStepNumber >= steps.Count;

        public void Finish()
        {
            currentStepNumber = -1;
            OnQuestStop?.Invoke(title, QuestStatus.Completed);
            Log($"Quest {title} finished");
            InvokeConsequences();
        }

        public void Drop()
        {
            StopCurrentStep();
            currentStepNumber = -1;
            OnQuestStop?.Invoke(title, QuestStatus.None);
        }

        public void Fail()
        {
            if(currentStepNumber == -1) return;
            StopCurrentStep();
            currentStepNumber = -1;
            OnQuestStop?.Invoke(title, QuestStatus.Failed);
        }

        private void InvokeConsequences()
        {
            foreach (var consequence in consequences)
            {
                consequence.Start();
            }
        }

        private void Log(string s)
        {
            if(logQuestSteps) Debug.Log(s);
        }
    }
}