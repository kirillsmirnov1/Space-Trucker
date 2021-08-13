using UnityEngine;
using UnityUtils;
using UnityUtils.Variables;

namespace Trucker.View.Util
{
    public class LookAt : MonoBehaviour
    {
        [SerializeField] private TransformVariable target;

        private void OnValidate() 
            => this.CheckNullFields();

        private void Update() 
            => SetLookAtRotation();

        private void SetLookAtRotation() 
            => transform.rotation = Quaternion.LookRotation(target.Value.position - transform.position);
    }
}
