using System.Collections.Generic;
using System.Linq;
using Trucker.Model.Questing.Goals;
using UnityEngine;

namespace Trucker.Model.Questing.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quests/Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        public string title;
        public string description;
        [SerializeField] private List<Goal> goals;
        
        // TODO init goals 
        // TODO check goals on completeness 
        // TODO give reward
        // TODO consequences (like taking objects from zap catcher)
    }
}