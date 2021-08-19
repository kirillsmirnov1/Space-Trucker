using System;
using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Control.Zap.Catchee
{
    public class ZapCatcheeReachable : MonoBehaviour
    {
        public Action<bool> onStatusChange;
        [SerializeField] private BoolVariable zapHasSpace;
        
        public bool Reachable => _reachableByDistance && _zapHasSpace;
        private bool _lastReachable;
        private bool _reachableByDistance;
        private bool _zapHasSpace;

        private void Awake() 
            => zapHasSpace.OnChange += OnHasSpaceChange;

        private void Start() 
            => OnHasSpaceChange(zapHasSpace);

        private void OnDestroy() 
            => zapHasSpace.OnChange -= OnHasSpaceChange;

        private void OnHasSpaceChange(bool hasSpace)
        {
            _zapHasSpace = hasSpace;
            UpdateStatus();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _reachableByDistance = true;
                UpdateStatus();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _reachableByDistance = false;
                UpdateStatus();
            }
        }

        private void UpdateStatus()
        {
            if(_lastReachable == Reachable) return;
            _lastReachable = Reachable;
            onStatusChange?.Invoke(_lastReachable);
        }
    }
}