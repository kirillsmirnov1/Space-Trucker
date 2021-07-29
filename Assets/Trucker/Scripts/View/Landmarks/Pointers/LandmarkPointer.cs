using System;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils;

namespace Trucker.View.Landmarks.Pointers
{
    public class LandmarkPointer : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float heightRatio;
        
        [Header("Debug")]
        [SerializeField] private Vector3 screenPos;
        
        private static readonly Vector2 ScreenDim = new Vector2(Screen.width/2, Screen.height/2);
        
        private Action _onUpdate;
        protected Landmark Landmark;
        private Camera _cam;
        protected bool LandmarkVisibleOnScreen;

        private void OnValidate() 
            => this.CheckNullFields();

        private void Update() 
            => _onUpdate?.Invoke();

        public virtual void Init(Sprite sprite, Landmark landmark)
        {
            _cam = Camera.main;
            
            image.sprite = sprite;
            var size = heightRatio * Screen.height;
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
            Landmark = landmark;

            Landmark.OnVisibilityChange += OnVisibilityChange;
            OnVisibilityChange(Landmark.Visible);
        }

        protected virtual void OnDestroy()
        {
            Landmark.OnVisibilityChange -= OnVisibilityChange;
        }

        protected virtual void OnVisibilityChange(bool visible)
        {
            LandmarkVisibleOnScreen = visible;
            _onUpdate = LandmarkVisibleOnScreen ? (Action) DisplayInsideScreen : DisplayOnScreenBorder;
        }

        private void DisplayInsideScreen() 
            => rectTransform.position = GetScreenPos();

        private Vector2 GetScreenPos()
        {
            screenPos = _cam.WorldToScreenPoint(Landmark.transform.position);
            return screenPos * Mathf.Sign(screenPos.z);
        }

        private void DisplayOnScreenBorder()
        {
            var direction = (GetScreenPos() - ScreenDim).normalized;
            
            var projection = new Vector2(
                ScreenDim.y * direction.x / direction.y,
                ScreenDim.x * direction.y / direction.x);

            var clampedProjection = Mathf.Abs(projection.y) < ScreenDim.y
                ? new Vector2(ScreenDim.x, projection.y) * Mathf.Sign(direction.x)
                : new Vector2(projection.x, ScreenDim.y) * Mathf.Sign(direction.y);

            clampedProjection *= 0.9f;
            
            rectTransform.anchoredPosition = clampedProjection;
        }
    }
}