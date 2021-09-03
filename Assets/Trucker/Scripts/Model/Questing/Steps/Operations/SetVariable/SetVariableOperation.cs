using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Model.Questing.Steps.Operations.SetVariable
{
    public abstract class SetVariableOperation <T> : Operation
    {
        [SerializeField] private XVariable<T> variable;
        [SerializeField] private T value;
        
        public override void Start()
        {
            variable.Value = value;
            onCompleted?.Invoke();
        }
    }
}