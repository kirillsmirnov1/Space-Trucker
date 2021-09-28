using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trucker.Model.Questing.Steps;
using Trucker.Model.Questing.Steps.Goals;

namespace Trucker.Model.Questing.Quests
{
    public static class GoalsTextFormatter
    {
        public static string Format(List<Step> steps)
        {
            var str = new StringBuilder();
            AppendGoalsDescriptions(steps, str);
            return str.ToString();
        }

        private static void AppendGoalsDescriptions(List<Step> steps, StringBuilder str)
        {
            for (int i = 0; i < steps.Count; i++)
            {
                var step = steps[i];
                if (step.Type != StepType.Goal) continue;

                switch (step)
                {
                    case ParallelGoal pg:
                        AppendGoalsDescriptions(pg.goals.Select(g => (Step) g).ToList(), str);
                        break;
                    case Goal goal:
                        AppendGoalDescription(str, goal);
                        break;
                }
            }
        }

        private static void AppendGoalDescription(StringBuilder strToAppendTo, Goal goal)
        {
            if(string.IsNullOrEmpty(goal.description)) return;
            strToAppendTo.Append(Prefix(goal.Stage));
            strToAppendTo.Append(" ");
            strToAppendTo.Append(goal.description);
            strToAppendTo.Append("\n");
        }

        private static string Prefix(Goal.GoalStage stage) 
        {
            return stage switch
            {
                Goal.GoalStage.Completed => "+",
                Goal.GoalStage.InProgress => "•",
                Goal.GoalStage.NotStarted => "-",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}