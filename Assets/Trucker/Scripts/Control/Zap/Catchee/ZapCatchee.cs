using Trucker.Control.Zap.Catchee.States;
using Trucker.Model.Zap;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityUtils;

namespace Trucker.Control.Zap.Catchee
{
    public class ZapCatchee : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] protected ZapCatcherVariable zapCatcherVariable;
        [SerializeField] public ZapCatcheeReachable reachableStatus;
        [SerializeField] public GameObject crosshairHolder;
        
        private ZapCatcheeState _state;
        public ZapCatcher Catcher => zapCatcherVariable.Value;

        private void OnValidate() => this.CheckNullFields();

        private void Awake()
        {
            SetState(new Free(this));
            SubscribeOnReachableStatusChange();
        }

        private void SubscribeOnReachableStatusChange()
        {
            reachableStatus.onStatusChange += reachable =>
            {
                if (reachable) _state.OnReachable();
                else _state.OnUnreachable();
            };
        }

        public void SetState(ZapCatcheeState newState)
        {
            _state?.ExitState();
            _state = newState;
            _state.EnterState();
        }

        public void OnPointerDown(PointerEventData eventData) => _state.OnPointerDown();

        public virtual void OnCatch() { }

        protected virtual void OnNoCatch() { }
    }
}