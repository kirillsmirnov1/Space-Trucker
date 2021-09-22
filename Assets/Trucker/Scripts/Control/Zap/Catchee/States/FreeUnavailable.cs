namespace Trucker.Control.Zap.Catchee.States
{
    public class FreeUnavailable : ZapCatcheeState
    {
        public FreeUnavailable(ZapCatchee zapCatchee) : base(zapCatchee) { }
        public override void EnterState() 
            => Catchee.crosshairHolder.SetActive(false);
    }
}