using System;
using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.View.Landmarks.Status
{
    public class LandmarkVisibility : MonoBehaviour
    {
        public Action<bool> onVisibilityChange;
        
        [SerializeField] private bool visible;

        [Header("Debug")]
        [SerializeField] private BoolVariable logVisibilityChange;
        
        private Camera _cam;

        public bool Visible
        {
            get => visible;
            private set
            {
                if(value == visible) return;
                visible = value;
                onVisibilityChange?.Invoke(visible);
                if(logVisibilityChange) Debug.Log($"{gameObject.name} is {(visible ? "visible" : "invisible")}");
            }
        }

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            CheckVisibility();
        }

        private void CheckVisibility()
        {
            var vpPos = _cam.WorldToViewportPoint(transform.position);
            Visible = vpPos.x >= 0 && vpPos.x <= 1 && vpPos.y >= 0 & vpPos.y <= 1 && vpPos.z >= 0;
        }
    }
}