using UnityEditor;
using UnityEngine;

namespace Trucker.View.Util
{
    [CustomEditor(typeof(PerlinNoise), true)]
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