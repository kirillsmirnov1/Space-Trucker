using System;
using Trucker.Control.Zap.Catchee.States;
using Trucker.Model.Entities;
using Trucker.Model.Zap;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityUtils;

namespace Trucker.Control.Zap.Catchee
{
    public class ZapCatchee : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action OnCatchingStarted;
        public event Action OnCatched; 
        public event Action OnFreed;
        
        [Header("Components")]
        [SerializeField] public ZapCatcheeReachable reachableStatus;
        [SerializeField] public GameObject crosshairHolder;
        [SerializeField] private EntityId entityId;
        [SerializeField] public SpriteRenderer progressDisplay;
        [SerializeField] public Rigidbody rb;
        [Header("Settings")]
        [SerializeField] public CatcheeSettings catcheeSettings;
        public bool interactableByPlayer = true;
        
        private ZapCatcheeState _state;
        private SpringJoint _springJoint;
        private JointAnchorConnection _anchorConnection;
        
        public ZapCatcher Catcher => catcheeSettings.zapCatcherVariable.Value;
        public EntityType Type => entityId.type;
        public bool Catched => _state is Catched;

        private void OnValidate() => this.CheckNullFields();

        private void Awake()
        {
            SetFreeState();
            SubscribeOnReachableStatusChange();
        }

        private void Update() 
            => _state.OnUpdate();

        private void FixedUpdate() 
            => _state.OnFixedUpdate();

        public void SetFreeState()
        {
            var initialState = reachableStatus.Reachable 
                ? (ZapCatcheeState) new FreeReachable(this) 
                : new FreeUnreachable(this);
            
            SetState(initialState);
        }

        public void SetCatchedState() => SetState(new Catched(this));
        public void SetFreeingState() => SetState(new Freeing(this));
        public void SetCatchingState() => SetState(new Catching(this));
        
        private void SubscribeOnReachableStatusChange()
        {
            reachableStatus.onStatusChange += reachable =>
            {
                if (reachable) _state.OnReachable();
                else _state.OnUnreachable();
            };
        }

        private void SetState(ZapCatcheeState newState)
        {
            _state?.ExitState();
            _state = newState;
            _state.EnterState();
        }

        public void OnPointerDown(PointerEventData eventData) 
            => _state.OnPointerDown();

        public void OnPointerUp(PointerEventData eventData) 
            => _state.OnPointerUp();

        public virtual void OnCatch() 
            => OnCatched?.Invoke();

        public void NotifyOnCatchingStart() 
            => OnCatchingStarted?.Invoke();

        public void SetConnection(SpringJointSettings springSettings, Transform bodyToConnectTo)
        {
            _springJoint = SetSpringJoint(springSettings);
            _anchorConnection = SetAnchorConnection(_springJoint);
            SetConnectedBody(bodyToConnectTo);
        }


        private SpringJoint SetSpringJoint(SpringJointSettings springSettings)
        {
            var springJoint = gameObject.AddComponent<SpringJoint>();

            springJoint.autoConfigureConnectedAnchor = springSettings.autoConfigureConnectedAnchor;
            springJoint.minDistance = springSettings.minDistance;
            springJoint.maxDistance = springSettings.maxDistance;
            springJoint.spring = springSettings.spring;
            springJoint.damper = springSettings.damper;
            
            return springJoint;
        }

        private JointAnchorConnection SetAnchorConnection(SpringJoint springJoint)
        {
            var jointAnchorConnection = gameObject.AddComponent<JointAnchorConnection>();
            jointAnchorConnection.joint = springJoint;
            return jointAnchorConnection;
        }

        public void DestroyConnection()
        {
            Destroy(_anchorConnection);
            Destroy(_springJoint);
        }

        public void SetConnectedBody(Transform bodyToConnectTo) 
            => _anchorConnection.connectedBody = bodyToConnectTo;

        public void OnCatchAttempt(bool catched)
        {
            if (catched)
            {
                SetCatchedState();
            }
            else
            {
                SetFreeState();
            }
        }

        public void OnFreeAttempt(bool freed)
        {
            if (freed)
            {
                OnCatcheeFreed();
            }
            else
            {
                SetCatchedState();
            }
        }

        protected virtual void OnCatcheeFreed()
        {
            SetFreeState();
            OnFreed?.Invoke();
        }
    }
}