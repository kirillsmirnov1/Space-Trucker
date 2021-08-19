using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Model.Questing.Steps.Operations
{
    [CreateAssetMenu(menuName = "Quests/Operations/SetBoolVariable", fileName = "SetBoolVariable", order = 0)]
    public class SetBoolVariable : Operation
    {
        [SerializeField] private BoolVariable variable;
        [SerializeField] private bool value;
        
        public override void Start()
        {
            variable.Value = value;
            onCompleted?.Invoke();
        }
    }
}