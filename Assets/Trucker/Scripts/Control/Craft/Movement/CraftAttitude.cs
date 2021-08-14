using UnityEngine;
using UnityUtils;
using UnityUtils.Extensions;
using UnityUtils.Variables;

namespace Trucker.Control.Craft.Movement
{
    public class CraftAttitude : MonoBehaviour
    {
        [SerializeField] private Vector3Variable attitudeInput;
        
        private Rigidbody _rb;

        private void OnValidate() => this.CheckNullFields();

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Rotate();
        }

        private void Rotate()
        {
            var attitudeChangeV3 = attitudeInput.Value;
            
            if (transform.UpsideDown()) attitudeChangeV3.y *= -1;

            var attitudeChangeQx = Quaternion.Euler(attitudeChangeV3.x, 0, 0);
            var attitudeChangeQy = Quaternion.Euler(0, attitudeChangeV3.y, 0);
            
            _rb.MoveRotation(attitudeChangeQy * _rb.rotation * attitudeChangeQx);
        }
    }
}