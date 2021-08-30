using UnityEngine;
using UnityEngine.Events;

namespace Trucker.Control.Spawn
{
    public class Spawnee : MonoBehaviour
    {
        [SerializeField] private UnityEvent onRespawnStart;
        [SerializeField] private UnityEvent onRespawnEnd;
        private BaseSpawn _spawn;
        private bool _respawning;

        public void Init(BaseSpawn spaceJunkOrbitSpawn)
        {
            _spawn = spaceJunkOrbitSpawn;
        }

        private void OnTriggerExit(Collider other)
        {
            if(_respawning || !other.CompareTag("SpawnEdge") || !enabled) return;
            lock (gameObject)
            {
                OnRespawnStart();
                ResetPosition();
                OnRespawnEnd();
            }

        }

        public void ResetPosition() 
            => transform.position = _spawn.RespawnPosition(transform.position);

        private void OnRespawnStart()
        {
            _respawning = true;
            gameObject.SetActive(false);
            onRespawnStart.Invoke();
        }

        private void OnRespawnEnd()
        {
            onRespawnEnd.Invoke();
            gameObject.SetActive(true);
            _respawning = false;
        }
    }
}
