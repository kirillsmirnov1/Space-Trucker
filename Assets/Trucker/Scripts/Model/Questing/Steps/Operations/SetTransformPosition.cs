using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Model.Questing.Steps.Operations
{
    [CreateAssetMenu(menuName = "Quests/Operations/SetTransformPosition", fileName = "SetTransformPosition", order = 0)]
    public class SetTransformPosition : Operation
    {
        [SerializeField] private TransformVariable subject;
        [SerializeField] private Vector3 desiredPosition;
        
        public override void Start()
        {
            subject.Value.position = desiredPosition;
            onCompleted?.Invoke();
        }
    }
}