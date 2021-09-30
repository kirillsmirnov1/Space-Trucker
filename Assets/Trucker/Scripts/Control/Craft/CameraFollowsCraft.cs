using Trucker.Control.Craft.Movement;
using Trucker.Model.Craft;
using UnityEngine;
using UnityUtils;
using UnityUtils.Extensions;

namespace Trucker.Control.Craft
{
    public class CameraFollowsCraft : MonoBehaviour
    {
        [SerializeField] private Transform cameraPivot;
        [SerializeField] private Transform craft;
        [SerializeField] private CameraFollowsCraftSettings settings;
        [SerializeField] private ShipModelParamsVariable shipModelParams;
        
        
        private void OnValidate() => this.CheckNullFieldsIfNotPrefab();

        private void FixedUpdate()
        {
            MovePosition();
            MoveRotation();
        }

        private void MovePosition() 
            => transform.position = Vector3.Lerp(transform.position, cameraPivot.position, Time.deltaTime * MovementSpeed);

        private float MovementSpeed 
            => settings.speedMove * shipModelParams.Value.maxSpeed;
        
        private void MoveRotation()
        {
            var pointToLookAt = craft.position + craft.forward * settings.craftForwardLookDistance;
            var targetRotation = Quaternion.LookRotation(pointToLookAt - transform.position);
            if(craft.UpsideDown()) targetRotation *= Quaternion.Euler(0, 0, 180); // FIXME should change smoothly 
            var slerpedRotation =
                Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * settings.speedRotate); 
            transform.rotation = slerpedRotation;
        }
    }
}