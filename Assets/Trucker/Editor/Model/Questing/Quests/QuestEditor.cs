using UnityEditor;
using UnityEngine;

namespace Trucker.Model.Questing.Quests
{
    [CustomEditor(typeof(Quest))]
    public class QuestEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Take quest"))
            {
                ((Quest)target).Take();
            }
            if (GUILayout.Button("Finish quest"))
            {
                ((Quest)target).Finish();
            }
        }
    }
}