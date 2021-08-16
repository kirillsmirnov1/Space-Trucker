using UnityEditor;
using UnityEngine;

namespace Trucker.View
{
    [CustomEditor(typeof(PerlinNoise))]
    public class PerlinNoiseEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Regenerate"))
            {
                var noiseGen = (PerlinNoise) target;
                noiseGen.RegenerateTexture();
            }

            if (GUILayout.Button("Apply new texture size"))
            {
                var noiseGen = (PerlinNoise) target;
                noiseGen.SetNewTexture();
            }
        }
    }
}