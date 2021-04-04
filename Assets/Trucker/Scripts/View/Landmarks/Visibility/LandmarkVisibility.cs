using UnityEngine;

namespace Trucker.Scripts.View.Landmarks.Visibility
{
    public class LandmarkVisibility : MonoBehaviour
    {
        // TODO bind callbacks from LandmarkRendererVisibility 
        // TODO display smth on canvas 
        
        private void Awake() => InitChildRenderers();

        private void InitChildRenderers()
        {
            foreach (var childRenderer in GetComponentsInChildren<Renderer>())
            {
                childRenderer.gameObject.AddComponent<LandmarkRendererVisibility>();
            }
        }
    }
}