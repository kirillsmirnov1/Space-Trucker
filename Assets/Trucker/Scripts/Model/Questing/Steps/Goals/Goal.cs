using System;
using UnityEngine;

namespace Trucker.Model.Questing.Steps.Goals
{
    public abstract class Goal : Step
    {
        public static event Action<string> OnStart;
        public static event Action<string> OnCompletion;
        
        public string description;
        [SerializeField] private bool forceResetAtInit;
        
        public override StepType Type => StepType.Goal;

        public bool Completed
        {
            get => _completed;
            set
            {
                if(value == _completed) return;
                _completed = value;

                if (_completed)
                {
                    onCompleted?.Invoke();
                    OnCompletion?.Invoke(description);
                }
            }
        }
        private bool _completed;
        private bool _started;

        public GoalStage Stage
        {
            get
            {
                if (_completed) return GoalStage.Completed;
                if (!_started) return GoalStage.NotStarted;
                return GoalStage.InProgress;
            }
        }
        
        public override void Init()
        {
            base.Init();
            _completed = false;
            _started = false;
            if (forceResetAtInit) Reset();
        }

        public abstract void Reset();

        public override void Start()
        {
            OnStart?.Invoke(description);
            _started = true;
        }

        protected void Complete() => Completed = true;

        public enum GoalStage
        {
            NotStarted, InProgress, Completed,
        }
    }
}