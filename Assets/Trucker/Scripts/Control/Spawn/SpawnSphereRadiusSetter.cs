using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Control.Spawn
{
    public class SpawnSphereRadiusSetter : MonoBehaviour
    {
        [SerializeField] private SphereCollider sphereCollider;
        [SerializeField] private FloatVariable spawnSphereRadius;
        
        private void Awake()
        {
            SetSpawnSphereRadius(spawnSphereRadius);
            spawnSphereRadius.OnChange += SetSpawnSphereRadius;
        }

        private void OnDestroy()
        {
            spawnSphereRadius.OnChange -= SetSpawnSphereRadius;
        }

        private void SetSpawnSphereRadius(float newRadius)
        {
            sphereCollider.radius = newRadius;
        }
    }
}
