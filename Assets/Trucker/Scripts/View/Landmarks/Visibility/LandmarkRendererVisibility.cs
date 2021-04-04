using UnityEngine;

namespace Trucker.Scripts.View.Landmarks.Visibility
{
    public class LandmarkRendererVisibility : MonoBehaviour
    {
        // TODO notify parent 
        private void OnBecameVisible()
        {
            Debug.Log($"{gameObject.name} became visible");
        }

        private void OnBecameInvisible()
        {
            Debug.Log($"{gameObject.name} became invisible");
        }
    }
}