using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Control.Craft.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class CraftThrust : MonoBehaviour
    {
        [SerializeField] private FloatVariable thrustValue;

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
            var thrustForce = Vector3.forward * thrustValue * _craftMass;
            _rb.AddForce(thrustForce);
        }
    }
}