using System;
using UnityEngine;

namespace Trucker.Model.Questing.Goals
{
    public abstract class Goal : ScriptableObject
    {
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
                
                if (_completed) OnCompleted?.Invoke();
                else OnNotCompleted?.Invoke();
            }
        }
        private bool _completed;

        public virtual void Init() 
            => _completed = false;
        public virtual void Stop(){}

        protected void Complete() => Completed = true;
    }
}