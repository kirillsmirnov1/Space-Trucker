using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Control.Craft.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class CraftThrust : MonoBehaviour
    {
        [SerializeField] private FloatVariable thrustValue;
        [SerializeField] private FloatVariable thrustMod;

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
            var speed = thrustValue * _craftMass * thrustMod;
            if (!(Mathf.Abs(speed) > 0f)) return;
            var thrustForce = transform.forward * speed;
            _rb.velocity = thrustForce;
        }
    }
}