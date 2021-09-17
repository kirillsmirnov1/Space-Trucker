namespace Trucker.Control.Zap.Catchee.States
{
    public class Unavailable : ZapCatcheeState
    {
        public Unavailable(ZapCatchee zapCatchee) : base(zapCatchee) { }
        public override void EnterState() 
            => Catchee.crosshairHolder.SetActive(false);
    }
}