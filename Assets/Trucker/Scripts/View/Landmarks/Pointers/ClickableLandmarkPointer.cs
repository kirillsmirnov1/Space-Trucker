using UnityEngine;

namespace Trucker.View.Landmarks.Pointers
{
    public class ClickableLandmarkPointer : LandmarkPointer
    {
        [SerializeField] private GameObject button;

        public override void Init(Sprite sprite, Landmark landmark)
        {
            base.Init(sprite, landmark);
            Landmark.OnPlayerInRangeChange += OnPlayerInRangeChange;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Landmark.OnPlayerInRangeChange -= OnPlayerInRangeChange;
        }

        private void OnPlayerInRangeChange(bool playerInRange) 
            => SetButtonVisibility();

        public void OnClick() 
            => Landmark.onInteraction?.Invoke();

        protected override void OnVisibilityChange(bool visible)
        {
            base.OnVisibilityChange(visible);
            SetButtonVisibility();
        }

        private void SetButtonVisibility()
        {
            button.SetActive(LandmarkVisibleOnScreen && Landmark.PLayerWithinRange); 
        }
    }
}