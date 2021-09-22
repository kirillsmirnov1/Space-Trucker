namespace Trucker.Control.Zap.Catchee.States
{
    public class Catched : ZapCatcheeState
    {
        public Catched(ZapCatchee catchee) : base(catchee) { }

        public override void EnterState()
        {
            Catchee.crosshairHolder.SetActive(false);
            Catchee.OnCatch();
        }

        public override void OnPointerDown()
        {
            Catchee.SetFreeingState();
        }

        public override void OnFixedUpdate()
        {
            base.OnUpdate();
            PreventGettingStuck();
        }

        private void PreventGettingStuck() => Catchee.rb.AddForce(0.00001f, 0, 0);
    }
}