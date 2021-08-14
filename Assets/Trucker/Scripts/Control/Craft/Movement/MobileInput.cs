using UnityEngine;
using UnityUtils;
using UnityUtils.Variables;

namespace Trucker.Control.Craft.Movement
{
    public class MobileInput : MonoBehaviour
    {
        [SerializeField] private Vector3Variable attitudeInput;

        private Vector3 _baseGyroAttitude;
        private Vector3 BaseGyroAttitude
        {
            get
            {
                if (_baseGyroAttitude == Vector3.zero)
                {
                    ResetBaseGyroAttitude();
                }
                return _baseGyroAttitude;
            }
        }
        
        private void OnValidate() => this.CheckNullFields();

        private void Update()
        {
            UpdateAttitudeInput();
        }

        public void ResetBaseGyroAttitude() 
            => _baseGyroAttitude = Input.acceleration;

        private void UpdateAttitudeInput()
        {
            var rawGyroChange = BaseGyroAttitude - Input.acceleration;
            attitudeInput.Value = new Vector3(rawGyroChange.z, -rawGyroChange.x, 0);
        }
    }
}