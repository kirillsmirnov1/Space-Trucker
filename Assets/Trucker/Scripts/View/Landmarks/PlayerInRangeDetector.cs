using UnityEngine;
using UnityUtils;
using UnityUtils.Variables;

namespace Trucker.View.Landmarks
{
    [RequireComponent(typeof(SphereCollider))]
    public class PlayerInRangeDetector : MonoBehaviour
    {
        [SerializeField] private BoolVariable playerInRange;

        private void OnValidate() => this.CheckNullFieldsIfNotPrefab();

        private void Awake() => playerInRange.Value = false;

        private void OnTriggerEnter(Collider other)
        {
            if(other.isTrigger) return;
            playerInRange.Value = true;
        }

        private void OnTriggerExit(Collider other) 
            => playerInRange.Value = false;
    }
}