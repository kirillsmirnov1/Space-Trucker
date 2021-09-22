﻿using System.Linq;
using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Model.Questing.Steps.Goals
{
    public class BoolArraySumGoal : Goal
    {
        [SerializeField] private int requiredTrueCount;
        [SerializeField] private BoolArrayVariable data;
        
        public override void Start()
        {
            base.Start();
            data.OnEntryChange += Check;
            Check();
        }

        public override void Stop()
        {
            base.Stop();
            data.OnEntryChange -= Check;
        }

        private void Check(int arg1, bool arg2) 
            => Check();

        private void Check()
        {
            var flagsChecked = data.Value.Count(flag => flag);
            Completed = flagsChecked >= requiredTrueCount;
        }
    }
}