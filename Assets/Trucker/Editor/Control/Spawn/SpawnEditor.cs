using UnityEditor;
using UnityEngine;

namespace Trucker.Control.Spawn
{
    [CustomEditor(typeof(SpaceJunkOrbitSpawn), true)]
    public class SpawnEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var obj = (BaseSpawn) target;
            if (GUILayout.Button("Spawn"))
            {
                obj.Spawn();
            }
            if (GUILayout.Button("Clear"))
            {
                obj.RemoveOldSpawn();
            }
            base.OnInspectorGUI();
        }
    }
}