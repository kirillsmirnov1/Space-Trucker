using UnityEngine;

namespace Trucker.Control.Craft.Movement
{
    public class CraftAttitude : MonoBehaviour
    {
        private Rigidbody _rb;

        private Vector3 _defaultAttitude;
        private Vector3 DefaultAttitude
        {
            get
            {
                if (_defaultAttitude == Vector3.zero)
                {
                    _defaultAttitude = Input.acceleration;
                }
                return _defaultAttitude;
            }
        }
        private Vector3 _attitude;
        private Vector3 _attitudeToRotation;
        private Vector3 _keyboardInput;
        private Vector3 _rotation;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rotation = _rb.rotation.eulerAngles;
        }

        private void Update()
        {
            UpdateAttitude();
            HandleKeyBoardInput();
            Rotate();
        }

        private void UpdateAttitude()
        {
            _attitude = DefaultAttitude - Input.acceleration;
            _attitudeToRotation = new Vector3(_attitude.z, -_attitude.x, 0);
        }

        private void HandleKeyBoardInput()
        {
            _keyboardInput = Vector3.zero;
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _keyboardInput += Vector3.down;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _keyboardInput += Vector3.up;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                _keyboardInput += Vector3.left;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                _keyboardInput += Vector3.right;
            }
            
            _keyboardInput.Normalize();
        }

        private void Rotate()
        {
            // Using V3 as rotation storage bc using Qs caused rotation around Z and I dont want that
            var rotationChangeV3 = _keyboardInput + _attitudeToRotation;
            
            if (transform.up.y < 0) rotationChangeV3.y *= -1;
            _rotation += rotationChangeV3;
            
            _rb.MoveRotation(Quaternion.Euler(_rotation));
        }
    }
}