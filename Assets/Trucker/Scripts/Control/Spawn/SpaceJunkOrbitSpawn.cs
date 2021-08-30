using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Control.Spawn
{
    public class SpaceJunkOrbitSpawn : BaseSpawn
    {
        [SerializeField] private TransformVariable spawnSphere;
        [SerializeField] private FloatVariable spawnEdgeRadius;
        [SerializeField] private FloatVariable respawnDistanceCoefficient;

        protected override Vector3 NextSpawnPosition()
        {
            return UnityEngine.Random.insideUnitSphere * spawnEdgeRadius + spawnSphere.Value.position;
        }

        public override Vector3 RespawnPosition(Vector3 oldPos)
        {
            var direction = (spawnSphere.Value.position - oldPos).normalized;
            return oldPos + direction * respawnDistanceCoefficient * spawnEdgeRadius;
        }
    }
}