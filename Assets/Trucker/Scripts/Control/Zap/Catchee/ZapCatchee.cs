﻿using Trucker.Control.Zap.Catchee.States;
using Trucker.Model.Zap;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityUtils;
using UnityUtils.Variables;

namespace Trucker.Control.Zap.Catchee
{
    public class ZapCatchee : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] protected ZapCatcherVariable zapCatcherVariable;
        [SerializeField] public ZapCatcheeReachable reachableStatus;
        [SerializeField] public GameObject crosshairHolder;
        [SerializeField] public SpriteRenderer progressDisplay;
        [SerializeField] public FloatVariable catchingDuration;
        
        private ZapCatcheeState _state;
        public ZapCatcher Catcher => zapCatcherVariable.Value;

        private void OnValidate() => this.CheckNullFields();

        private void Awake()
        {
            SetFreeState();
            SubscribeOnReachableStatusChange();
        }

        private void Update() 
            => _state.OnUpdate();

        private void SetFreeState()
        {
            var initialState = reachableStatus.Reachable 
                ? (ZapCatcheeState) new FreeReachable(this) 
                : new FreeUnreachable(this);
            
            SetState(initialState);
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

        public void OnPointerDown(PointerEventData eventData) 
            => _state.OnPointerDown();

        public void OnPointerUp(PointerEventData eventData) 
            => _state.OnPointerUp();

        public virtual void OnCatch() { }

        public virtual void OnFree() 
            => SetFreeState();
    }
}