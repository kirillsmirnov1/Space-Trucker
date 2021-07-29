using System;
using UnityEngine;

namespace Trucker.View.Landmarks
{
    [RequireComponent(typeof(SphereCollider))]
    public class PlayerInRangeDetector : MonoBehaviour
    {
        public Action<bool> onPlayerInRangeChange;
        
        private bool _withinRange;

        public bool WithinRange
        {
            get => _withinRange;
            private set
            {
                _withinRange = value;
                onPlayerInRangeChange?.Invoke(_withinRange);
            }
        }

        private void OnTriggerEnter(Collider other) 
            => WithinRange = true;

        private void OnTriggerExit(Collider other) 
            => WithinRange = false;
    }
}