using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Control.Craft.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class CraftThrust : MonoBehaviour
    {
        [SerializeField] private FloatVariable thrustValue;
        [SerializeField] private FloatVariable thrustMod;
        [SerializeField] private FloatVariable maxSpeed;
        
        private Rigidbody _rb;
        private float _craftMass;

        private void Awake() => InitFields();

        private void FixedUpdate() => ApplyThrust();

        private void InitFields()
        {
            _rb = GetComponent<Rigidbody>();
            _craftMass = _rb.mass;
        }

        private void ApplyThrust()
        {
            if (!(Mathf.Abs(thrustValue) > 0f)) return;
            
            var currentSpeed = _rb.velocity.magnitude;
            var speedToAdd = Mathf.Clamp(maxSpeed - currentSpeed, 0, MaxSpeedToAdd);
            var thrustForce = transform.forward * speedToAdd;
            _rb.AddForce(thrustForce, ForceMode.Acceleration);
        }

        private float MaxSpeedToAdd => thrustMod * thrustValue * _craftMass;
    }
}