using System;
using UnityEngine;

namespace Trucker.Model.Questing.Steps
{
    public abstract class Step : ScriptableObject
    {
        public Action onCompleted;
        public abstract StepType Type { get; }
        public virtual void Init() { }
        public abstract void Start();
        public virtual void Stop(){}
    }
}