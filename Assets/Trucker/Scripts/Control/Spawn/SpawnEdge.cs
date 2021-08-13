using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Control.Spawn
{
    public class SpawnEdge : MonoBehaviour
    {
        [SerializeField] private SphereCollider sphereCollider;
        [SerializeField] private FloatVariable radius;

        private void Awake()
        {
            SetRadius(radius);
            radius.OnChange += SetRadius;
        }

        private void OnDestroy()
        {
            radius.OnChange -= SetRadius;
        }

        private void SetRadius(float newRadius)
        {
            sphereCollider.radius = newRadius;
        }
    }
}
