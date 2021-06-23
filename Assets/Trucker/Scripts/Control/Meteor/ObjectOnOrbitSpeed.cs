using UnityEngine;
using UnityUtils;
using UnityUtils.Variables;
using Random = System.Random;

namespace Trucker.Control.Meteor
{
    public class ObjectOnOrbitSpeed : MonoBehaviour
    {
        [SerializeField] private TransformVariable centralObject;
        [SerializeField] private FloatVariable stayOnOrbitForce;
        [SerializeField] private FloatVariable orbitSpeed;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private FloatVariable orbitRadius;
        [SerializeField] private FloatVariable orbitCircleRadius;
                
        private static readonly Random Random = new Random();
        private float _personalOrbitRadius;

        private Vector3 CentralObjectPos => centralObject.Value.position;
        private Vector3 ToCentral => CentralObjectPos - transform.position;
        private Vector3 ToCentralNormalized => ToCentral.normalized;
        private Vector3 OrbitDirection => Vector3.Cross(ToCentral, Vector3.up).normalized;
        private Vector3 PushForce => (OrbitDirection + Random.insideUnitSphere * 0.1f) * orbitSpeed;

        private void OnValidate() => this.CheckNullFields();

        private void Start() => Init();

        private void FixedUpdate()
        {
            MoveToOrbit();
            PushObject();
        }

        private void Init() // TODO call on respawn 
        {
            SetPersonalOrbitRadius();
            SetStartSpeed();
        }

        private void SetPersonalOrbitRadius() 
            => _personalOrbitRadius = ToCentral.magnitude;

        private void SetStartSpeed() 
            => rb.velocity = PushForce;

        private void PushObject() => rb.AddForce(PushForce);
        
        private void MoveToOrbit() 
        {
            var distanceFromOrbitCenter = ToCentral.magnitude - _personalOrbitRadius;
            var moveDirection = ToCentralNormalized * distanceFromOrbitCenter;
            rb.AddForce(moveDirection * stayOnOrbitForce * rb.velocity.magnitude);
        }
    }
}