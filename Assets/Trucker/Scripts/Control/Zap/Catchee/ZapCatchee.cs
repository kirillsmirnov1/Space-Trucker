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
        [SerializeField] private SphereCollider protectiveSphereCollider;
        
        [Header("Settings")]
        [SerializeField] public CatcheeSettings catcheeSettings;
        public bool interactableByPlayer = true;
        
        private ZapCatcheeState _state;
        private SpringJoint _springJoint;
        private JointAnchorConnection _anchorConnection;
        public float SafeRadius => protectiveSphereCollider.radius 
                                 * protectiveSphereCollider.transform.localScale.x 
                                 * transform.localScale.x;

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

        public void SetUnavailableState() => SetState(new Unavailable(this));

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

        public void SetConnection(SpringJointSettings springSettings, Transform bodyToConnectTo, float prevSafeRadius)
        {
            _springJoint = SetSpringJoint(springSettings, prevSafeRadius);
            _anchorConnection = SetAnchorConnection(_springJoint);
            SetConnectedBody(bodyToConnectTo);
        }


        private SpringJoint SetSpringJoint(SpringJointSettings springSettings, float prevSafeRadius)
        {
            var springJoint = gameObject.AddComponent<SpringJoint>();
            var safeDistance = prevSafeRadius + SafeRadius;

            springJoint.autoConfigureConnectedAnchor = springSettings.autoConfigureConnectedAnchor;
            springJoint.minDistance = safeDistance;  
            springJoint.maxDistance = safeDistance * 1.2f;
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
        }

        public void OnFreeAttempt(bool freed)
        {
            if (freed)
            {
                OnCatcheeFreed();
            }
        }

        protected virtual void OnCatcheeFreed()
        {
            SetFreeState();
            OnFreed?.Invoke();
        }

        private void OnDestroy() => Catcher?.TryFree(this);
    }
}