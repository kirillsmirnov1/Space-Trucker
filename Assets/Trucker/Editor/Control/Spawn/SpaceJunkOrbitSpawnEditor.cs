using UnityEditor;
using UnityEngine;

namespace Trucker.Control.Spawn
{
    [CustomEditor(typeof(SpaceJunkOrbitSpawn))]
    public class SpaceJunkOrbitSpawnEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Spawn"))
            {
                var obj = (SpaceJunkOrbitSpawn) target;
                obj.Spawn();
            }
            base.OnInspectorGUI();
        }
    }
}