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
            if(Catchee.interactableByPlayer)
                Catchee.SetState(new Freeing(Catchee));
        }
    }
}