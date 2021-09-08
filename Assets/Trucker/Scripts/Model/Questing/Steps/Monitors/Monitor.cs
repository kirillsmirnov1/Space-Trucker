using System.Collections.Generic;
using Trucker.Model.Questing.Quests;
using Trucker.Model.Questing.Steps.Operations;
using UnityEngine;

namespace Trucker.Model.Questing.Steps.Monitors
{
    public abstract class Monitor : ScriptableObject
    {
        [SerializeField] protected List<Operation> consequences;
        private Quest _quest;
        public virtual void Init(Quest quest) => _quest = quest;
        public abstract void Start();
        public abstract void Stop();
    }
}