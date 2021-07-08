using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Trucker.Model.Questing.Quests
{
    [CreateAssetMenu(fileName = "Quests Index", menuName = "Quests/Quests Index", order = 0)]
    public class QuestsIndex : ScriptableObject
    {
        [SerializeField] private List<Quest> questList;

        private Dictionary<string, Quest> _questDict;

        public Dictionary<string, Quest> Quests
        {
            get
            {
                if (_questDict == null)
                {
                    InitQuestDictionary();
                }
                
                return _questDict;
            }
            private set => _questDict = value;
        }

        private void InitQuestDictionary()
        {
            _questDict = new Dictionary<string, Quest>();
            foreach (var quest in questList)
            {
                _questDict.Add(quest.title, quest);
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
    }
}