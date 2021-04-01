using UnityEngine;
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
            var targetRotation = Quaternion.Euler(0, _rb.rotation.eulerAngles.y + directionInput.Value.x, 0);
            var lerpT = rotationSpeed * directionInput.Value.magnitude;
            
            var newRotation = Quaternion.Lerp(_rb.rotation, targetRotation, lerpT);
            
            _rb.MoveRotation(newRotation);
        }
    }
}