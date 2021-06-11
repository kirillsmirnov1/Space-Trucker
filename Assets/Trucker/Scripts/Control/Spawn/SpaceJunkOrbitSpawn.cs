using Unity.Mathematics;
using UnityEngine;
using UnityUtils;
using Random = System.Random;

namespace Trucker.Control.Spawn
{
    public class SpaceJunkOrbitSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject prefabToSpawn; // might extract factory later
        [SerializeField] private Transform centralObject;
        [SerializeField] private int numberOfObjects;
        [SerializeField] private float orbitRadius;
        [SerializeField] private float spawnCircleRadius; // might change to ellipsis later
        [SerializeField] private Vector2 sizeMinMax = new Vector2(1f, 10f);
        
        private static readonly Random Random = new Random();

        private void Awake()
        {
            Spawn();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = Color.yellow;
            UnityEditor.Handles.DrawWireDisc(centralObject.position, Vector3.up, orbitRadius);
            UnityEditor.Handles.DrawWireDisc(centralObject.position, Vector3.up, orbitRadius - spawnCircleRadius);
            UnityEditor.Handles.DrawWireDisc(centralObject.position, Vector3.up, orbitRadius + spawnCircleRadius);
        }
#endif

        public void Spawn()
        {
            RemoveOldSpawn();
            for (var i = 0; i < numberOfObjects; i++)
            {
                var obj = Instantiate(prefabToSpawn, NextPosition(), quaternion.identity, transform);
                obj.transform.localScale = Random.NextFloat(sizeMinMax.x, sizeMinMax.y) * Vector3.one;
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
            var orbitPosition = centralObject.position + Quaternion.Euler(0, angle, 0) * Vector3.right * orbitRadius;
            var shiftRadius = Vector3.one.normalized * spawnCircleRadius;
            var orbitShift = Random.NextVector(-shiftRadius, shiftRadius);
            return orbitPosition + orbitShift; 
        }
        
    }
}