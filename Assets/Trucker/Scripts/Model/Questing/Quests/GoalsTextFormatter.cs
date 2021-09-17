using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trucker.Model.Questing.Steps;
using Trucker.Model.Questing.Steps.Goals;
using UnityEngine.Rendering;

namespace Trucker.Model.Questing.Quests
{
    public static class GoalsTextFormatter
    {
        public static string Format(List<Step> steps, int currentGoal)
        {
            var str = new StringBuilder();

            AppendGoalDescriptions(steps, currentGoal, str);

            return str.ToString();
        }

        private static void AppendGoalDescriptions(List<Step> steps, int currentGoal, StringBuilder str)
        {
            for (int i = 0; i < steps.Count; i++)
            {
                var step = steps[i];
                if (step.Type != StepType.Goal) continue;

                switch (step)
                {
                    case ParallelGoal pg:
                        AppendGoalDescriptions(pg.goals.Select(g => (Step) g).ToList(), currentGoal, str);
                        break;
                    case Goal goal:
                        AppendGoalDescription(str, goal, i - currentGoal);
                        break;
                }
            }
        }

        private static void AppendGoalDescription(StringBuilder strToAppendTo, Goal goal, int distanceFromCurrentGoal)
        {
            strToAppendTo.Append(Prefix(distanceFromCurrentGoal));
            strToAppendTo.Append(" ");
            strToAppendTo.Append(goal.description);
            strToAppendTo.Append("\n");
        }

        private static string Prefix(int distanceFromCurrent) // IMPR dont use index 
        {
            if (distanceFromCurrent == 0) return "•";
            return distanceFromCurrent < 0 ? "+" : "-";
        }
    }
}