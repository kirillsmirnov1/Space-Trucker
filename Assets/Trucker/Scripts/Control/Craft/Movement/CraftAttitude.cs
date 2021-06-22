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
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
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
            var rotationChangeV3 = _keyboardInput + _attitudeToRotation;
            
            if (transform.up.y < 0) rotationChangeV3.y *= -1;

            // FIXME apply to rb 
            transform.Rotate(rotationChangeV3.x, 0, 0, Space.Self);
            transform.Rotate(0, rotationChangeV3.y, 0, Space.World);
        }
    }
}