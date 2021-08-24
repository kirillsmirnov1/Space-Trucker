using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Trucker.View.Tutorials
{
    public class MovementTutorial : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform phone;
        [SerializeField] private TextMeshProUGUI rotationPrompt;
        [SerializeField] private GameObject thrustPrompts;
        
        [Header("Data")]
        [SerializeField] private float maxAngle = 45f;
        [SerializeField] private float iterationDuration = 2f;
        [SerializeField] private int iterationSteps = 120;

        public void StartTutorial() => StartTutorial(null);
        
        public void StartTutorial(Action callback)
        {
            gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(Tutorial(callback));
        }

        private IEnumerator Tutorial(Action callback)
        {
            EnableObjects(true, true, false);
            yield return TiltTutorial();
            yield return RotationTutorial();
            EnableObjects(false, false, true);
            yield return ThrustTutorial();
            // TODO callback 
            EnableObjects(false, false, false);
            gameObject.SetActive(false);
            callback?.Invoke();
        }

        private void EnableObjects(bool phoneImage, bool rotationPromptText, bool thrustPromptTexts)
        {
            phone.gameObject.SetActive(phoneImage);
            rotationPrompt.gameObject.SetActive(rotationPromptText);
            thrustPrompts.SetActive(thrustPromptTexts);

        }

        private IEnumerator TiltTutorial()
        {
            phone.localRotation = Quaternion.Euler(0f, 0f, 0f);
            rotationPrompt.text = "tilt phone to tilt ship";
            yield return RotateTransform(phone, Vector3.right, 2);
        }

        private IEnumerator RotationTutorial()
        {
            phone.localRotation = Quaternion.Euler(0f, 0f, 0f);
            rotationPrompt.text = "rotate phone to rotate ship";
            yield return RotateTransform(phone, Vector3.forward, 2);
        }

        private IEnumerator ThrustTutorial()
        {
            thrustPrompts.gameObject.SetActive(true);
            yield return new WaitForSeconds(iterationDuration);
        }

        private IEnumerator RotateTransform(Transform subject, Vector3 axis, int times)
        {
            var wait = new WaitForSeconds(iterationDuration/iterationSteps);
            var totalAngleDistance = maxAngle * 4;
            var anglePerStep = totalAngleDistance / iterationSteps;
            
            for (int i = 0; i < times; i++)
            {
                var stepChangeDir = iterationSteps / 4;
                for (int step = 0; step < iterationSteps; step++)
                {
                    if (step == stepChangeDir)
                    {
                        anglePerStep = -anglePerStep;
                        stepChangeDir *= 3;
                    }

                    subject.Rotate(axis, anglePerStep);
                    
                    yield return wait;
                }
            }
        }
    }
}
