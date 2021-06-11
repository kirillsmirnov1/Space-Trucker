using Unity.Mathematics;
using UnityEngine;

namespace Trucker.Control.Spawn
{
    public class SpaceJunkOrbitSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject prefabToSpawn; // might extract factory later
        [SerializeField] private Transform centralObject;
        [SerializeField] private int numberOfObjects;
        [SerializeField] private float orbitRadius;
        [SerializeField] private float spawnCircleRadius; // might change to ellipsis later

        // TODO show spawn location 
        
        public void Spawn()  
        {
            RemoveOldSpawn();
            for (var i = 0; i < numberOfObjects; i++)
            {
                Instantiate(prefabToSpawn, NextPosition(), quaternion.identity, transform);
                // TODO change size and rotation 
            }
        }

        private void RemoveOldSpawn()
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        private Vector3 NextPosition()
        {
            return Vector3.zero; // TODO 
        }
        
    }
}