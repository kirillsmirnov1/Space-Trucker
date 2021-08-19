using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Control.Util
{
    public class BoolVariableSetsEnabledStatus : MonoBehaviour // IMPR move to UU 
    {
        [SerializeField] private BoolVariable variable;
    
        private void Awake()
        {
            variable.OnChange += SetEnabledStatus;
            SetEnabledStatus(variable);
        }

        private void OnDestroy()
        {
            variable.OnChange -= SetEnabledStatus;
        }

        private void SetEnabledStatus(bool newStatus)
        {
            gameObject.SetActive(newStatus);
        }
    }
}
