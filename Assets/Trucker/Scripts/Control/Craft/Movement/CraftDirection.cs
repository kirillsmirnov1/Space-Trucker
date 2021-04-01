using UnityEngine;
using UnityUtils.Extensions;
using UnityUtils.Variables;

namespace Trucker.Control.Craft.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class CraftDirection : MonoBehaviour
    {
        [SerializeField] private Vector2Variable directionInput;
        [SerializeField] private FloatVariable rotationSpeed;
        
        private Rigidbody _rb;

        private void Awake() => _rb = GetComponent<Rigidbody>();

        private void FixedUpdate() => Rotate();

        private void Rotate()
        {
            // FIXME doesn't work as expected 

            var joystickAngle = directionInput.Value.ToAngleInDegrees();
            var targetRotation = Quaternion.Euler(0, -joystickAngle, 0);
            var lerpT = rotationSpeed * directionInput.Value.magnitude;
            
            var newRotation = Quaternion.Lerp(_rb.rotation, targetRotation, lerpT);
            
            _rb.MoveRotation(newRotation);
        }
    }
}