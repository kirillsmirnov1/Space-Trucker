using System;

namespace Trucker.Model.Questing.Steps.Goals
{
    public abstract class Goal : Step
    {
        public static event Action<string> OnStart;
        public static event Action<string> OnCompletion;
        
        public string description;

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

        public override void Start()
        {
            OnStart?.Invoke(description);
            _completed = false;
        }

        protected void Complete() => Completed = true;
    }
}