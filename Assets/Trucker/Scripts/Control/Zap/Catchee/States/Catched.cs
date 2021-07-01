namespace Trucker.Control.Zap.Catchee.States
{
    public class Catched : ZapCatcheeState
    {
        public Catched(ZapCatchee catchee) : base(catchee) { }

        public override void EnterState()
        {
            Catchee.crosshairHolder.SetActive(false);
        }
    }
}