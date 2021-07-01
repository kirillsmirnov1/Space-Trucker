using Trucker.Model.Zap;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Trucker.Control.Zap
{
    public class ZapCatchee : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] protected ZapCatcherVariable zapCatcherVariable;
        
        protected ZapCatcher Catcher => zapCatcherVariable.Value;
        public Rigidbody Rigidbody => rb;
        public void OnPointerDown(PointerEventData eventData) => TryCatch();

        protected virtual void TryCatch()
        {
            if (Catcher.TryCatch(this))
                OnCatch();
            else
                OnNoCatch();
        }

        protected virtual void OnCatch() { }

        protected virtual void OnNoCatch() { }
    }
}