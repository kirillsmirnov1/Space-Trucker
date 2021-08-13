using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityUtils;
using UnityUtils.Variables;
using Random = System.Random;

namespace Trucker.Control.Spawn
{
    public class SpaceJunkOrbitSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject prefabToSpawn; // might extract factory later
        [SerializeField] private TransformVariable centralObject;
        [SerializeField] private IntVariable numberOfObjects;
        [SerializeField] private FloatVariable orbitRadius;
        [SerializeField] private FloatVariable spawnCircleRadius; // might change to ellipsis later
        [SerializeField] private Vector2Variable spaceJunkScale;
        
        private static readonly Random Random = new Random();

        private void OnValidate() => this.CheckNullFieldsIfNotPrefab();

        private void Awake()
        {
            Spawn();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if(gameObject.InPrefabScene()) return;
            Handles.color = Color.yellow;
            Handles.DrawWireDisc(centralObject.Value.position, Vector3.up, orbitRadius);
            Handles.DrawWireDisc(centralObject.Value.position, Vector3.up, orbitRadius - spawnCircleRadius);
            Handles.DrawWireDisc(centralObject.Value.position, Vector3.up, orbitRadius + spawnCircleRadius);
        }
#endif

        public void Spawn()
        {
            RemoveOldSpawn();
            for (var i = 0; i < numberOfObjects; i++)
            {
                var obj = Instantiate(prefabToSpawn, NextPosition(), quaternion.identity, transform);
                obj.transform.localScale = Random.NextFloat(spaceJunkScale.Value.x, spaceJunkScale.Value.y) * Vector3.one;
            }
        }

        public void RemoveOldSpawn()
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        private Vector3 NextPosition()
        {
            var angle = Random.Next(0, 360);
            var orbitPosition = centralObject.Value.position + Quaternion.Euler(0, angle, 0) * Vector3.right * orbitRadius;
            var shiftRadius = Vector3.one.normalized * spawnCircleRadius;
            var orbitShift = Random.NextVector(-shiftRadius, shiftRadius);
            return orbitPosition + orbitShift; 
        }
        
    }
}