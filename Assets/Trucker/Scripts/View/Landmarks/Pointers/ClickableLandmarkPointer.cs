using UnityEngine;

namespace Trucker.View.Landmarks.Pointers
{
    public class ClickableLandmarkPointer : LandmarkPointer
    {
        [SerializeField] private GameObject button;
        
        public void OnClick() 
            => Landmark.onInteraction?.Invoke();

        protected override void OnVisibilityChange(bool visible)
        {
            base.OnVisibilityChange(visible);
            SetButtonVisibility();
        }

        private void SetButtonVisibility()
        {
            button.SetActive(LandmarkVisibleOnScreen); // TODO also use range 
        }
    }
}