using System;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils;
using UnityUtils.Variables;

namespace Trucker.View.Landmarks.Pointers
{
    public class LandmarkPointer : MonoBehaviour
    {
        [SerializeField] protected Image image;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float heightRatio;
        [SerializeField] protected BoolVariable landmarksRadarEnabled;
        
        [Header("Debug")]
        [SerializeField] private Vector3 screenPos;
        
        private static readonly Vector2 ScreenDim = new Vector2(Screen.width/2, Screen.height/2);
        
        protected Action onUpdate;
        protected Landmark Landmark;
        private Camera _cam;
        protected bool LandmarkVisibleOnScreen;

        private void OnValidate() 
            => this.CheckNullFields();

        private void Update() 
            => onUpdate?.Invoke();

        public virtual void Init(Sprite sprite, Landmark landmark)
        {
            _cam = Camera.main;
            
            image.sprite = sprite;
            var size = heightRatio * Screen.height;
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
            Landmark = landmark;

            landmarksRadarEnabled.OnChange += OnRadarStatusChange;
            Landmark.OnVisibilityChange += OnVisibilityChange;
            OnVisibilityChange(Landmark.Visible);
        }

        protected virtual void OnDestroy()
        {
            landmarksRadarEnabled.OnChange -= OnRadarStatusChange;
            Landmark.OnVisibilityChange -= OnVisibilityChange;
        }

        private void OnRadarStatusChange(bool radarEnabled) => OnVisibilityChange(Landmark.Visible);

        protected virtual void OnVisibilityChange(bool visible)
        {
            LandmarkVisibleOnScreen = visible;
            SetImageVisibility();
            SetUpdateMethod();
        }

        protected virtual void SetUpdateMethod()
        {
            if (landmarksRadarEnabled)
            {
                onUpdate = LandmarkVisibleOnScreen ? (Action) DisplayInsideScreen : DisplayOnScreenBorder;
            }
            else
            {
                onUpdate = null;
            }
        }

        protected virtual void SetImageVisibility() 
            => image.gameObject.SetActive(landmarksRadarEnabled);

        protected void DisplayInsideScreen() 
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