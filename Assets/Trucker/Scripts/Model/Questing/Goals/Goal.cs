using System;
using UnityEngine;

namespace Trucker.Model.Questing.Goals
{
    public abstract class Goal : ScriptableObject
    {
        public static event Action<string> OnStart;
        public static event Action<string> OnCompletion;
        
        public event Action OnCompleted;
        public event Action OnNotCompleted;

        public string description;
        
        public bool Completed
        {
            get => _completed;
            set
            {
                if(value == _completed) return;
                _completed = value;

                if (_completed)
                {
                    OnCompleted?.Invoke();
                    OnCompletion?.Invoke(description);
                }
                else OnNotCompleted?.Invoke();
            }
        }
        private bool _completed;

        public virtual void Init()
        {
            OnStart?.Invoke(description);
            _completed = false;
        }

        public virtual void Stop(){}

        protected void Complete() => Completed = true;
    }
}