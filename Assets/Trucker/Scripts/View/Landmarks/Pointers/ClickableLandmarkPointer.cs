using System;
using UnityEngine;

namespace Trucker.View.Landmarks.Pointers
{
    public class ClickableLandmarkPointer : LandmarkPointer
    {
        [SerializeField] private GameObject button;

        private bool ButtonVisible => LandmarkVisibleOnScreen && Landmark.PlayerWithinRange; 
        public override void Init(Sprite sprite, Landmark landmark)
        {
            base.Init(sprite, landmark);
            Landmark.playerInRangeChange += OnPlayerInRangeChange;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Landmark.playerInRangeChange -= OnPlayerInRangeChange;
        }

        private void OnPlayerInRangeChange(bool playerInRange) 
            => OnVisibilityChange(Landmark.Visible);

        public void OnClick() 
            => Landmark.onInteraction?.Invoke();

        protected override void OnVisibilityChange(bool visible)
        {
            base.OnVisibilityChange(visible);
            SetButtonVisibility();
        }

        protected override void SetUpdateMethod()
        {
            if (landmarksRadarEnabled)
            {
                base.SetUpdateMethod();
            }
            else
            {
                onUpdate = ButtonVisible ? (Action) DisplayInsideScreen : null;
            }
        }

        private void SetButtonVisibility() 
            => button.SetActive(ButtonVisible);

        protected override void SetImageVisibility() 
            => image.gameObject.SetActive(ButtonVisible || landmarksRadarEnabled);
    }
}