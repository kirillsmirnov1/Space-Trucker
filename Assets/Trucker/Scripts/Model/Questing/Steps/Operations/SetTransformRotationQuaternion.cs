using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Model.Questing.Steps.Operations
{
    [CreateAssetMenu(menuName = "Quests/Operations/SetTransformRotationQuaternion", fileName = "SetTransformRotationQuaternion", order = 0)]
    public class SetTransformRotationQuaternion : Operation
    {
        [SerializeField] private TransformVariable subject;
        [SerializeField] private Quaternion desiredRotation;

        public override void Start()
        {
            subject.Value.rotation = desiredRotation;
            onCompleted?.Invoke();
        }
    }
}