using Trucker.Model.Questing.Steps;
using UnityEditor;
using UnityEngine;

namespace Trucker.Model.Questing.Quests.Steps
{
    [CustomEditor(typeof(Step), true)]
    public class StepEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Start"))
            {
                ((Step)target).Start();
            }
        }
    }
}