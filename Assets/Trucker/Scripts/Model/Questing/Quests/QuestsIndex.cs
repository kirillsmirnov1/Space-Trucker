using System.Collections.Generic;
using System.Linq;
using Trucker.Model.Util;
using UnityEngine;

namespace Trucker.Model.Questing.Quests
{
    [CreateAssetMenu(fileName = "Quests Index", menuName = "Quests/Quests Index", order = 0)]
    public class QuestsIndex : InitiatedScriptableObject
    {
        [SerializeField] private List<Quest> questList;

        private Dictionary<string, Quest> Quests { get; set; }

        public override void Init() 
            => InitQuestDictionary();

        private void InitQuestDictionary()
        {
            Quests = new Dictionary<string, Quest>();
            foreach (var quest in questList)
            {
                Quests.Add(quest.title, quest);
                quest.currentGoalNumber = -1;
            }
        }

        private void OnValidate() 
            => CheckUniqueQuestNames();

        private void CheckUniqueQuestNames()
        {
            var set = new HashSet<string>();
            foreach (var quest in questList.Where(quest => quest != null && !set.Add(quest.title)))
            {
                Debug.LogWarning($"Quest {quest.name} has non-unique title");
            }
        }

        public Quest this[string title] => Quests[title];
    }
}