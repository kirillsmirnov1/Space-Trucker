using Trucker.Model.Zap;
using UnityEngine;

namespace Trucker.Control.Zap
{
    public class ZapCatchee : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] protected ZapCatcherVariable zapCatcherVariable;
        
        protected ZapCatcher Catcher => zapCatcherVariable.Value;
        public Rigidbody Rigidbody => rb;

        private void OnMouseDown() => TryCatch();

        protected virtual void TryCatch()
        {
            Catcher.TryCatch(this);
        }
    }
}