using Trucker.Control.Craft.Movement;
using Trucker.Model.Craft;
using UnityEngine;
using UnityUtils;

namespace Trucker.Control.Craft
{
    public class CameraFollowsCraft : MonoBehaviour
    {
        [SerializeField] private Transform cameraPivot;
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
            var slerpedRotation =
                Quaternion.Slerp(transform.rotation, cameraPivot.rotation, Time.deltaTime * settings.speedRotate); 
            transform.rotation = slerpedRotation;
        }
    }
}