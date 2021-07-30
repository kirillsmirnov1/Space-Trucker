using Trucker.Control.Zap.Catchee.States;
using Trucker.Model.Entities;
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
        [SerializeField] public FloatVariable approachSpeed;
        [SerializeField] private EntityId entityId;
        
        private ZapCatcheeState _state;
        private SpringJoint _springJoint;
        private JointAnchorConnection _anchorConnection;
        
        public ZapCatcher Catcher => zapCatcherVariable.Value;
        public EntityType Type => entityId.type;

        private void OnValidate() => this.CheckNullFields();

        private void Awake()
        {
            SetFreeState();
            SubscribeOnReachableStatusChange();
        }

        private void Update() 
            => _state.OnUpdate();

        public void SetFreeState()
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
    }
}