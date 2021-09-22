using UnityEngine;
using UnityUtils.Variables;
using UnityUtils.Variables.Binding;

namespace Trucker.Model.Bindings
{
    public class BindPosToArray : XVariableBindingRoot
    {
        [SerializeField] private int index = -1;
        [SerializeField] private Vector3ArrayVariable array;
        
        protected override void BindValue() 
            => array[index] = transform.position;

        protected override void ClearValue() 
            => array[index] = Vector3.zero;
    }
}