using Trucker.Model;
using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Control.Spawn
{
    public class SpaceJunkOrbitSpawn : BaseSpawn
    {
        [SerializeField] private TransformVariable spawnZone;
        [SerializeField] private FloatVariable respawnDistanceCoefficient;

        private BoxCollider _boxCollider;

        protected override void Start()
        {
            _boxCollider = spawnZone.Value.GetComponent<BoxCollider>();
            base.Start();
        }

        protected override Vector3 NextSpawnPosition()
        {
            return Random.GetRandomPointInsideCollider(_boxCollider);
        }

        public override Vector3 RespawnPosition(Vector3 oldPos)
        {
            var direction = spawnZone.Value.position - oldPos; 
            return oldPos + respawnDistanceCoefficient * direction;
        }
    }
}