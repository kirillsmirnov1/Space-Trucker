using System.Collections;
using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Model.Questing.Steps.Operations
{
    [CreateAssetMenu(menuName = "Quests/Operations/RotateTransformTowardTransform", fileName = "RotateTransformTowardTransform", order = 0)]
    public class RotateTransformTowardTransform : Operation
    {
        [SerializeField] private TransformVariable subject;
        [SerializeField] private TransformVariable target;
        [Tooltip("More steps — smoother transition")]
        [SerializeField] private int steps = 100;
        
        public override void Start()
        {
            subject.Value
                .GetComponent<MonoBehaviour>() // IMPR provide MB context in a more elegant way
                .StartCoroutine(Rotate(subject.Value, target.Value, steps));
            onCompleted?.Invoke();
        }

        private static IEnumerator Rotate(Transform subject, Transform target, int steps)
        {
            for (var i = 0; i < steps; i++)
            {
                yield return null;
                var lookRotation = Quaternion.LookRotation(target.position - subject.position);
                var currentRotation = subject.rotation; 
                if(Quaternion.Angle(currentRotation, lookRotation) < 1f) break;
                subject.rotation = Quaternion.Slerp(currentRotation, lookRotation, 1.0f * i / steps);
            }
        }
    }
}