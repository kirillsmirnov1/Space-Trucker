using System;
using Trucker.View.Landmarks.Status;
using UnityEngine;
using UnityEngine.Events;

namespace Trucker.View.Landmarks
{
    public class Landmark : MonoBehaviour
    {
        [SerializeField] private LandmarkVisibility visibility;
        [SerializeField] private PlayerInRangeDetector playerDetector;
        
        public UnityEvent onInteraction;

        public bool Visible => visibility.Visible;
        public Action<bool> OnVisibilityChange
        {
            get => visibility.onVisibilityChange;
            set => visibility.onVisibilityChange = value;
        }

        public bool PLayerWithinRange => playerDetector.WithinRange;
        public Action<bool> OnPlayerInRangeChange
        {
            get => playerDetector.onPlayerInRangeChange;
            set => playerDetector.onPlayerInRangeChange = value;
        }
    }
}