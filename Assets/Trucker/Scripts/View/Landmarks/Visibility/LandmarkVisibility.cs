using System;
using UnityEngine;

namespace Trucker.View.Landmarks.Visibility
{
    public class LandmarkVisibility : MonoBehaviour
    {
        // TODO display smth on canvas 
        public Action<bool> OnVisibilityChange;
        
        [SerializeField] private bool visible;
        
        private Camera _cam;

        public bool Visible
        {
            get => visible;
            private set
            {
                if(value == visible) return;
                visible = value;
                OnVisibilityChange?.Invoke(visible);
                Debug.Log($"{gameObject.name} is {(visible ? "visible" : "invisible")}");
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