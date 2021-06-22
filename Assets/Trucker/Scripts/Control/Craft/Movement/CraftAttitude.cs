using UnityEngine;

namespace Trucker.Control.Craft.Movement
{
    public class CraftAttitude : MonoBehaviour
    {
        private Rigidbody _rb;

        private Vector3 _baseGyroAttitude;
        private Vector3 BaseGyroAttitude
        {
            get
            {
                if (_baseGyroAttitude == Vector3.zero)
                {
                    _baseGyroAttitude = Input.acceleration;
                }
                return _baseGyroAttitude;
            }
        }

        private Vector3 _attitudeChangeGyro;
        private Vector3 _attitudeChangeKeyboard;
        
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
            var rawGyroChange = BaseGyroAttitude - Input.acceleration;
            _attitudeChangeGyro = new Vector3(rawGyroChange.z, -rawGyroChange.x, 0);
        }

        private void HandleKeyBoardInput()
        {
            _attitudeChangeKeyboard = Vector3.zero;
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _attitudeChangeKeyboard += Vector3.down;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _attitudeChangeKeyboard += Vector3.up;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                _attitudeChangeKeyboard += Vector3.left;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                _attitudeChangeKeyboard += Vector3.right;
            }
            
            _attitudeChangeKeyboard.Normalize();
        }

        private void Rotate()
        {
            var attitudeChangeV3 = _attitudeChangeKeyboard + _attitudeChangeGyro;
            
            if (transform.up.y < 0) attitudeChangeV3.y *= -1;

            var attitudeChangeQx = Quaternion.Euler(attitudeChangeV3.x, 0, 0);
            var attitudeChangeQy = Quaternion.Euler(0, attitudeChangeV3.y, 0);
            
            _rb.MoveRotation(attitudeChangeQy * _rb.rotation * attitudeChangeQx);
        }
    }
}