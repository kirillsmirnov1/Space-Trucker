namespace Trucker.Control.Zap.Catchee.States
{
    public abstract class ZapCatcheeState
    {
        protected ZapCatchee Catchee;

        public ZapCatcheeState(ZapCatchee catchee) 
            => Catchee = catchee;

        public abstract void EnterState();
        public virtual void ExitState() {}

        public virtual void OnUpdate(){}
        
        public virtual void OnReachable(){}
        public virtual void OnUnreachable(){}
        
        public virtual void OnPointerDown(){}
        public virtual void OnPointerUp(){}
    }
}