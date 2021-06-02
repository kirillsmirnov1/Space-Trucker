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
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _keyboardInput = Vector3.down;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                _keyboardInput = Vector3.up;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                _keyboardInput = Vector3.right;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                _keyboardInput = Vector3.left;
            }
            else
            {
                _keyboardInput = Vector3.zero;
            }
        }

        private void Rotate()
        {
            var targetRotation = _rb.rotation.eulerAngles + _keyboardInput + _attitudeToRotation;
            _rb.MoveRotation(Quaternion.Euler(targetRotation));
        }
    }
}