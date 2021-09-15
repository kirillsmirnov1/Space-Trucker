namespace Trucker.Control.Zap.Catchee.States
{
    public class FreeUnreachable : ZapCatcheeState
    {
        public FreeUnreachable(ZapCatchee catchee) : base(catchee) { }

        public override void EnterState() 
            => Catchee.crosshairHolder.SetActive(false);

        public override void OnReachable() 
            => Catchee.SetFreeState();
    }
}