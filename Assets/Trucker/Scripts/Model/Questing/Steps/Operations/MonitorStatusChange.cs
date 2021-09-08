using System;
using Trucker.Model.Questing.Steps.Monitors;
using UnityEngine;

namespace Trucker.Model.Questing.Steps.Operations
{
    public class MonitorStatusChange : Operation
    {
        [SerializeField] private Monitor monitor;
        [SerializeField] private MonitorAction monitorAction;

        public override void Start()
        {
            if (monitorAction == MonitorAction.Start)
                monitor.Start();
            else
                monitor.Stop();
        }
        
        [Serializable]
        public enum MonitorAction
        {
            Start,
            Stop
        }
    }
}