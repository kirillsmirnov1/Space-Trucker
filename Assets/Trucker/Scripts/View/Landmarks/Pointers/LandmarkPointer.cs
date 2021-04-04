using System;
using Trucker.View.Landmarks.Visibility;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils;

namespace Trucker.View.Landmarks.Pointers
{
    public class LandmarkPointer : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private RectTransform rectTransform;
        
        private Action _onUpdate;
        private LandmarkVisibility _landmarkVisibility;
        private Camera _cam;

        private void OnValidate() 
            => this.CheckNullFields();

        private void Update() 
            => _onUpdate?.Invoke();

        public void Init(Sprite sprite, LandmarkVisibility landmarkVisibility)
        {
            _cam = Camera.main;
            
            image.sprite = sprite;
            _landmarkVisibility = landmarkVisibility;

            _landmarkVisibility.OnVisibilityChange += SetDisplayMethod;
            SetDisplayMethod(_landmarkVisibility.Visible);
        }

        private void OnDestroy()
        {
            _landmarkVisibility.OnVisibilityChange -= SetDisplayMethod;
        }

        private void SetDisplayMethod(bool visible)
        {
            _onUpdate = visible ? (Action) DisplayInsideScreen : DisplayOnScreenBorder;
        }

        private void DisplayInsideScreen() 
            => rectTransform.position = _cam.WorldToScreenPoint(_landmarkVisibility.transform.position);

        private void DisplayOnScreenBorder()
        {
            // TODO
        }
    }
}