using System.Collections.Generic;
using Trucker.Model.Questing.Quests;
using Trucker.Model.Questing.Steps.Operations;
using UnityEngine;

namespace Trucker.Model.Questing.Steps.Monitors
{
    public abstract class Monitor : ScriptableObject
    {
        [SerializeField] protected List<Operation> consequences;
        protected Quest quest;
        public virtual void Init(Quest quest) => this.quest = quest;
        public abstract void Start();
        public abstract void Stop();

        protected virtual void InvokeConsequences()
        {
            foreach (var consequence in consequences)
            {
                consequence.Start();
            }
        }
    }
}