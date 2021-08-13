using Unity.Mathematics;
using UnityEngine;
using UnityUtils;
using UnityUtils.Variables;
using Random = System.Random;

namespace Trucker.Control.Spawn
{
    public class SpaceJunkOrbitSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject prefabToSpawn; // might extract factory later
        [SerializeField] private TransformVariable spawnSphere;
        [SerializeField] private FloatVariable spawnEdgeRadius;
        [SerializeField] private FloatVariable respawnDistanceCoefficient;
        [SerializeField] private IntVariable numberOfObjects;
        [SerializeField] private Vector2Variable spaceJunkScale;
        
        private static readonly Random Random = new Random();

        private void OnValidate() => this.CheckNullFieldsIfNotPrefab();

        private void Start()
        {
            Spawn();
        }

        public void Spawn()
        {
            RemoveOldSpawn();
            for (var i = 0; i < numberOfObjects; i++)
            {
                SpawnObject(i);
            }
        }

        private void SpawnObject(int index)
        {
            var obj = Instantiate(prefabToSpawn, NextSpawnPosition(), quaternion.identity, transform);
            obj.transform.localScale = Random.NextFloat(spaceJunkScale.Value.x, spaceJunkScale.Value.y) * Vector3.one;
            obj.name += index;
            obj.GetComponent<Spawnee>().Init(this);
        }

        public void RemoveOldSpawn()
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        private Vector3 NextSpawnPosition()
        {
            return Random.NextVector(-Vector3.one, Vector3.one) * spawnEdgeRadius + spawnSphere.Value.position;
        }

        public Vector3 RespawnPosition(Vector3 oldPos)
        {
            var direction = (spawnSphere.Value.position - oldPos).normalized;
            return oldPos + direction * respawnDistanceCoefficient * spawnEdgeRadius;
        }
    }
}