using UnityEditor;
using UnityEngine;

namespace Trucker.Control.Spawn
{
    [CustomEditor(typeof(SpaceJunkOrbitSpawn))]
    public class SpaceJunkOrbitSpawnEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var obj = (SpaceJunkOrbitSpawn) target;
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