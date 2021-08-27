using Trucker.Model.NPC;
using Trucker.Model.Questing.Quests;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Trucker.Control.Questing
{
    [CreateAssetMenu(menuName = "Logic/ResetProgress", fileName = "ResetProgress", order = 0)]
    public class ResetProgress : ScriptableObject
    {
        [SerializeField] private QuestLogEntries questLogEntries;
        [SerializeField] private Dialogue[] dialogues;
        [SerializeField] private Quest[] quests;
        [SerializeField] private Quest starterQuest;
#if UNITY_EDITOR
        private void OnValidate()
        {
            dialogues = GetAllSoInstances<Dialogue>();
            quests = GetAllSoInstances<Quest>();
        }
#endif

        public void Invoke()
        {
            ResetDialogues();
            DropActiveQuests();
            questLogEntries.Clear();
            ReloadScene();
        }

        private void ReloadScene()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(0);
        }

        private void DropActiveQuests()
        {
            foreach (var quest in quests)
            {
                if (quest.InProgress)
                {
                    quest.Drop();
                }
            }
        }

        private void ResetDialogues()
        {
            foreach (var dialogue in dialogues)
            {
                dialogue.Value = false;
            }
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            starterQuest.Take();
        }

#if UNITY_EDITOR
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
#endif
    }
}