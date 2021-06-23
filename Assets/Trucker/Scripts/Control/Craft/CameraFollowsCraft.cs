using UnityEngine;

namespace Trucker.Control.Craft
{
    public class CameraFollowsCraft : MonoBehaviour
    {
        [SerializeField] private Transform cameraPivot;
        [SerializeField] private Transform craft;
        [SerializeField] private float craftForwardLookDistance = 10f;
        [SerializeField] private float speedMove = 1f;
        [SerializeField] private float speedRotate = 1f;
        

        private void FixedUpdate()
        {
            MovePosition();
            MoveRotation();
        }

        private void MovePosition() 
            => transform.position = Vector3.Lerp(transform.position, cameraPivot.position, Time.deltaTime * speedMove);

        private void MoveRotation()
        {
            var pointToLookAt = craft.position + craft.forward * craftForwardLookDistance;
            var targetRotation = Quaternion.LookRotation(pointToLookAt - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation, Time.deltaTime * speedRotate);
        }
    }
}