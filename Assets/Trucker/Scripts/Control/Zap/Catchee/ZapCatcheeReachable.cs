using System;
using UnityEngine;

namespace Trucker.Control.Zap.Catchee
{
    public class ZapCatcheeReachable : MonoBehaviour
    {
        public Action<bool> onStatusChange;

        private bool _reachable;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SetStatus(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SetStatus(false);
            }
        }

        private void SetStatus(bool reachable)
        {
            _reachable = reachable;
            onStatusChange?.Invoke(_reachable);
        }
    }
}