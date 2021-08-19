using System.Collections.Generic;
using System.Text;
using Trucker.Model.Questing.Steps.Goals;

namespace Trucker.Model.Questing.Quests
{
    public static class GoalsTextFormatter
    {
        public static string Format(List<Goal> goals, int currentGoal)
        {
            var str = new StringBuilder();

            for (int i = 0; i < goals.Count; i++)
            {
                var goal = goals[i];
                str.Append(Prefix(i - currentGoal));
                str.Append(" ");
                str.Append(goal.description);
                str.Append("\n");
            }
            
            return str.ToString();
        }

        private static string Prefix(int distanceFromCurrent)
        {
            if (distanceFromCurrent == 0) return "•";
            return distanceFromCurrent < 0 ? "+" : "-";
        }
    }
}