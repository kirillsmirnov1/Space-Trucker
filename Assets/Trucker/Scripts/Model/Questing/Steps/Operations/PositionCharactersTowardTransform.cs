using System.Collections;
using System.Linq;
using Trucker.Model.Entities;
using Trucker.Model.Zap;
using UnityEngine;
using UnityUtils;
using UnityUtils.Variables;
using Random = System.Random;

namespace Trucker.Model.Questing.Steps.Operations
{
    [CreateAssetMenu(menuName = "Quests/Operations/PositionCharactersTowardTransform", fileName = "PositionCharactersTowardTransform", order = 0)]
    public class PositionCharactersTowardTransform : Operation
    {
        [SerializeField] private ZapCatcherVariable catcher;
        [SerializeField] private TransformVariable targetTransform;
        [SerializeField] private float jumpRatio = 0.5f;
        [SerializeField] private Vector3 endPosShift = Vector3.one;
        [SerializeField] private int steps = 100;
        
        private static readonly Random Random = new Random();
        
        public override void Start()
        {
            StartRotation();
            onCompleted?.Invoke();
        }

        private void StartRotation()
        {
            var catchees = catcher.Value.Catchees;
            var characters = catchees.Where(catchee => catchee.Type == EntityType.Character);
            foreach (var character in characters)
            {
                StartRotation(character);
            }
        }

        private void StartRotation(MonoBehaviour character)
        {
            character.StopAllCoroutines();
            character.StartCoroutine(LookRotationCoroutine(character.transform));
        }

        private IEnumerator LookRotationCoroutine(Transform character)
        {
            var targetPosition = targetTransform.Value.position;
            var startPosition = character.position;
            var startRotation = character.rotation;

            var charToTarget = targetPosition - startPosition;
            var endPosition = startPosition + jumpRatio * charToTarget + Random.NextVector(-endPosShift, endPosShift);
            var endRotation = Quaternion.LookRotation(targetPosition - endPosition);
            
            for (float i = 0; i <= 1; i += 1f/steps)
            {
                character.rotation = Quaternion.Slerp(startRotation, endRotation, i);
                character.position = Vector3.Lerp(startPosition, endPosition, i);
                yield return null;
            }
        }
    }
}