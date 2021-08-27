using Trucker.Model.NPC;
using Trucker.Model.Questing.Quests;
using UnityEditor;
using UnityEngine;

namespace Trucker.Control.Questing
{
    [CreateAssetMenu(menuName = "Logic/ResetProgress", fileName = "ResetProgress", order = 0)]
    public class ResetProgress : ScriptableObject
    {
        [SerializeField] private QuestLogEntries questLogEntries;
        [SerializeField] private Dialogue[] dialogues;
        [SerializeField] private Quest starterQuest;

        private void OnValidate()
        {
            dialogues = GetAllSoInstances<Dialogue>();
        }

        public void Invoke()
        {
            questLogEntries.Clear();
            foreach (var dialogue in dialogues)
            {
                dialogue.Value = false;
            }

            starterQuest.Take();
        }

        public static T[] GetAllSoInstances<T>() where T : ScriptableObject // TODO move to UU 
        {
            var guids = AssetDatabase.FindAssets("t:" + typeof(T).Name); //FindAssets uses tags check documentation for more info
            var instances = new T[guids.Length];
            for (var i = 0; i < guids.Length; i++) //probably could get optimized 
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                instances[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }
            return instances;
        }
    }
}