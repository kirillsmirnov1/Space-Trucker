using System;
using Trucker.View.Landmarks.Status;
using UnityEngine;
using UnityEngine.Events;

namespace Trucker.View.Landmarks
{
    public class Landmark : MonoBehaviour
    {
        [SerializeField] private LandmarkVisibility visibility;
        public UnityEvent onInteraction;

        public bool Visible => visibility.Visible;
        public Action<bool> OnVisibilityChange
        {
            get => visibility.onVisibilityChange;
            set => visibility.onVisibilityChange = value;
        }
    }
}