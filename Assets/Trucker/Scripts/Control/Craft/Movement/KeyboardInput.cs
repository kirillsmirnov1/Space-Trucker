using UnityEngine;
using UnityUtils;
using UnityUtils.Variables;

namespace Trucker.Control.Craft.Movement
{
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private Vector3Variable attitudeChange;
        [SerializeField] private FloatVariable thrustValue;
        
        private Vector3 _attitudeChangeKeyboard;
        private bool _beenThrusting;

        private void OnValidate() => this.CheckNullFields();

#if UNITY_EDITOR
        private void Update()
        {
            UpdateAttitudeInput();
            UpdateThrustInput();
        }
#endif

        private void UpdateAttitudeInput()
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

            attitudeChange.Value = _attitudeChangeKeyboard;
        }

        private void UpdateThrustInput()
        {
            if (Input.GetKey(KeyCode.RightShift))
            {
                thrustValue.Value = 1f;
                _beenThrusting = true;
            }
            else if (Input.GetKey(KeyCode.RightControl))
            {
                thrustValue.Value = -1f;
                _beenThrusting = true;
            }
            else if(_beenThrusting)
            {
                thrustValue.Value = 0f;
                _beenThrusting = false;
            }
        }
    }
}