using Unity.Mathematics;
using UnityEngine;
using UnityUtils;
using UnityUtils.Variables;
using Random = System.Random;

namespace Trucker.Control.Spawn
{
    public class BaseSpawn : MonoBehaviour
    {
        [SerializeField] protected GameObject prefabToSpawn; // might extract factory later
        [SerializeField] protected IntVariable numberOfObjects;
        [SerializeField] protected Vector2Variable scale;
        
        protected static readonly Random Random = new Random();

        private void OnValidate() => this.CheckNullFieldsIfNotPrefab();

        protected virtual void Start()
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
            obj.transform.localScale = Random.NextFloat(scale.Value.x, scale.Value.y) * Vector3.one;
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

        protected virtual Vector3 NextSpawnPosition() => transform.position;

        public virtual Vector3 RespawnPosition(Vector3 oldPos) => transform.position;
    }
}