namespace Trucker.Control.Zap.Catchee.States
{
    public class Free : ZapCatcheeState
    {
        public Free(ZapCatchee catchee) : base(catchee) { }

        private bool Reachable => Catchee.reachableStatus.Reachable; // TODO split state 
        
        public override void EnterState() => SetCrosshair(Reachable);

        public override void OnReachable() => SetCrosshair(true);

        public override void OnUnreachable() => SetCrosshair(false);

        public override void OnPointerDown()
        {
            if(Reachable) TryCatch();
        }

        private void SetCrosshair(bool reachable) => Catchee.crosshairHolder.SetActive(reachable);

        protected virtual void TryCatch()
        {
            if (Catchee.Catcher.TryCatch(Catchee))
            {
                Catchee.OnCatch();
                Catchee.SetState(new Catched(Catchee));
            }
        }
    }
}