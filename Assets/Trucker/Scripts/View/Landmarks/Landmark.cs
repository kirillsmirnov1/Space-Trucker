using System;
using Trucker.View.Landmarks.Status;
using UnityEngine;
using UnityEngine.Events;
using UnityUtils;
using UnityUtils.Variables;

namespace Trucker.View.Landmarks
{
    public class Landmark : MonoBehaviour
    {
        [SerializeField] private LandmarkVisibility visibility;
        [SerializeField] private BoolVariable playerInRange;
        
        public UnityEvent onInteraction;

        public bool Visible => visibility.Visible;
        public Action<bool> VisibilityChange
        {
            get => visibility.onVisibilityChange;
            set => visibility.onVisibilityChange = value;
        }

        public bool PlayerWithinRange => playerInRange.Value;
        [NonSerialized] public Action<bool> playerInRangeChange;

        private void OnValidate() => this.CheckNullFields();
        private void Awake() => playerInRange.OnChange += InvokeRangeChange;
        private void OnDestroy() => playerInRange.OnChange -= InvokeRangeChange;

        private void InvokeRangeChange(bool inRange) 
            => playerInRangeChange?.Invoke(inRange);
    }
}