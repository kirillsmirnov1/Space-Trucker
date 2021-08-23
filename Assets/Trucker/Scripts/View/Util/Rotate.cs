using UnityEngine;

namespace Trucker.View.Util
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 10f;

        // Update is called once per frame
        private void Update()
        {
            transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
        }
    }
}
