using System.Collections;
using TMPro;
using UnityEngine;

namespace Trucker.View.Tutorials
{
    public class MovementTutorial : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform phone;
        [SerializeField] private TextMeshProUGUI prompt;

        [Header("Data")]
        [SerializeField] private float maxAngle = 45f;
        [SerializeField] private float iterationDuration = 2f;
        [SerializeField] private int iterationSteps = 120;

        public void StartTutorial()
        {
            gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(Tutorial());
        }

        private IEnumerator Tutorial()
        {
            SetDefaults();
            yield return TiltTutorial();
            SetDefaults();
            yield return RotationTutorial();
            // TODO thrust 
            // TODO callback 
            gameObject.SetActive(false);
        }

        private void SetDefaults()
        {
            phone.rotation = Quaternion.identity;
            prompt.text = "";
        }

        private IEnumerator TiltTutorial()
        {
            prompt.text = "tilt phone to tilt ship";
            yield return RotateTransform(phone, Vector3.right, 2);
        }

        private IEnumerator RotationTutorial()
        {
            prompt.text = "rotate phone to rotate ship";
            yield return RotateTransform(phone, Vector3.forward, 2);
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
