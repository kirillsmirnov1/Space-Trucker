using UnityEngine;
using UnityUtils.Variables.Display;

namespace Trucker.View.Util
{
    public class IntVariableDisplayHidesOnZero : IntVariableDisplay
    {
        [SerializeField] private GameObject objectToDisable;

        protected override void Start()
        {
            base.Start();
            SetActive();
        }

        protected override void OnChange(int value)
        {
            base.OnChange(value);
            SetActive();
        }

        private void SetActive()
        {
            objectToDisable.gameObject.SetActive(variable.Value > 0);
        }
    }
}