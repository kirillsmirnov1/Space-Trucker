using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Control.Spawn
{
    public class SpawnSphereOnOrbit : MonoBehaviour
    {
        [SerializeField] private SphereCollider sphereCollider;
        [SerializeField] private FloatVariable spawnSphereRadius;
        [SerializeField] private FloatVariable orbitRadius;
        
        private Transform _player;
        
        private void Awake()
        {
            DetachFromPlayer();
            SetSpawnSphereRadius(spawnSphereRadius);
            spawnSphereRadius.OnChange += SetSpawnSphereRadius;
        }

        private void DetachFromPlayer()
        {
            _player = transform.parent;
            transform.SetParent(null);
        }

        private void OnDestroy()
        {
            spawnSphereRadius.OnChange -= SetSpawnSphereRadius;
        }

        private void SetSpawnSphereRadius(float newRadius)
        {
            sphereCollider.radius = newRadius;
        }

        private void FixedUpdate()
        {
            // Q is on (0,0,0), so there is no need to subtract it's position, for now
            transform.position = _player.position.normalized * orbitRadius;
        }
    }
}
