using UnityEngine;
using UnityEngine.Events;

namespace Trucker.Control.Spawn
{
    public class Spawnee : MonoBehaviour
    {
        [SerializeField] private UnityEvent onRespawnStart;
        [SerializeField] private UnityEvent onRespawnEnd;
        private SpaceJunkOrbitSpawn _spaceJunkOrbitSpawn;
        private bool _respawning;

        public void Init(SpaceJunkOrbitSpawn spaceJunkOrbitSpawn)
        {
            _spaceJunkOrbitSpawn = spaceJunkOrbitSpawn;
        }

        private void OnTriggerExit(Collider other)
        {
            if(_respawning || !other.CompareTag("SpawnEdge")) return;
            lock (gameObject)
            {
                OnRespawnStart();
                ResetPosition();
                OnRespawnEnd();
            }

        }

        private void ResetPosition() 
            => transform.position = _spaceJunkOrbitSpawn.RespawnPosition(transform.position);

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
