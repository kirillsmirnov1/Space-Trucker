using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Trucker.Model.Questing.Steps.Goals
{
    [CreateAssetMenu(menuName = "Quests/Goals/ParallelGoal", fileName = "ParallelGoal", order = 0)]
    public class ParallelGoal : Goal
    {
        [SerializeField] public List<Goal> goals;

        public override void Init()
        {
            base.Init();
            foreach (var goal in goals)
            {
                goal.Init();
            }
        }

        public override void Reset()
        {
            for (int i = 0; i < goals.Count(); i++)
            {
                goals[i].Reset();
            }
        }

        public override void Start()
        {
            base.Start();
            foreach (var goal in goals)
            {
                goal.onCompleted += OnSubGoalCompleted;
                goal.Start();
            }
        }

        public override void Stop()
        {
            base.Stop();
            foreach (var goal in goals)
            {
                goal.onCompleted -= OnSubGoalCompleted;
                goal.Stop();
            }
        }

        private void OnSubGoalCompleted()
        {
            Completed = goals.All(goal => goal.Completed);
        }
    }
}