using System;
using UnityEngine;

namespace Trucker.Control.Entities
{
    [RequireComponent(typeof(Collider))]
    public class UraniumHitWatcher : MonoBehaviour
    {
        public static event Action OnUraniumHit;
        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Uranium"))
            {
                OnUraniumHit?.Invoke();
            }
        }
    }
}