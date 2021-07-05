namespace Trucker.Control.Zap.Catchee.States
{
    public class Catched : ZapCatcheeState
    {
        public Catched(ZapCatchee catchee) : base(catchee) { }

        public override void EnterState()
        {
            Catchee.crosshairHolder.SetActive(false);
            Catchee.Catcher.TryCatch(Catchee);
            Catchee.OnCatch();
        }

        public override void OnPointerDown()
        {
            // TODO Set Freeing state 
            Free();
        }

        private void Free()
        {
            if (Catchee.Catcher.TryFree(Catchee))
            {
                Catchee.OnFree();
            }
        }
    }
}