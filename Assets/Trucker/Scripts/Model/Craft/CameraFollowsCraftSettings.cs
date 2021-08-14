using UnityEngine;

namespace Trucker.Model.Craft
{
    [CreateAssetMenu(menuName = "Data/CameraFollowsCraftSettings", fileName = "CameraFollowsCraftSettings", order = 0)]
    public class CameraFollowsCraftSettings : ScriptableObject
    {
        [SerializeField] public float craftForwardLookDistance = 10f;
        [SerializeField] public float speedMove = 7f;
        [SerializeField] public float speedRotate = 10f;
    }
}