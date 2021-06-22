using UnityEngine;
using UnityUtils;
using UnityUtils.Variables;

namespace Trucker.Control.Meteor
{
    public class ObjectOnOrbitSpeed : MonoBehaviour
    {
        [SerializeField] private TransformVariable centralObject;
        [SerializeField] private FloatVariable stayOnOrbitForce;
        [SerializeField] private FloatVariable orbitSpeed;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private FloatVariable orbitRadius;
        
        private Vector3 CentralObjectPos => centralObject.Value.position;
        
        private void OnValidate() => this.CheckNullFields();

        private void FixedUpdate()
        {
            MoveToOrbit();
            PushObject();
        }

        private void PushObject() => rb.AddForce(OrbitDirection * orbitSpeed); // TODO add some randomness maybe 

        private Vector3 ToCentral => CentralObjectPos - transform.position;
        private Vector3 ToCentralNormalized => ToCentral.normalized;
        private Vector3 OrbitDirection => Vector3.Cross(ToCentral, Vector3.up).normalized;
        
        private void MoveToOrbit() // TODO welp make it work nice 
        {
            var moveDirection = ToCentralNormalized * (ToCentral.magnitude - orbitRadius);
            rb.AddForce(moveDirection * stayOnOrbitForce);
        }
    }
}