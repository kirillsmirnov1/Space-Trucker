namespace Trucker.Control.Zap.Catchee.States
{
    public class CatchedUnavailable : Catched
    {
        public CatchedUnavailable(ZapCatchee catchee) : base(catchee) { }
        public override void OnPointerDown() { }
    }
}