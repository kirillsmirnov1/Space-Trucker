using UnityEngine;

namespace Trucker.Control.Zap.Catchee.States
{
    public class Catching : ZapCatcheeState
    {
        public Catching(ZapCatchee catchee) : base(catchee) { }

        public override void EnterState() { }

        public override void OnUpdate()
        {
            // IMPR draw ray 
            // IMPR slowly move towards Catcher 
            EnlargeCrosshairFiller();
        }

        private void EnlargeCrosshairFiller()
        {
            // FIXME refactor
            // FIXME speed 
            Catchee.crosshairFiller.transform.localScale += Time.deltaTime * Vector3.one;
            if (Catchee.crosshairFiller.transform.localScale.magnitude >= 1)
            {
                Catchee.SetState(new Catched(Catchee));
            }
        }

        public override void OnPointerUp() 
            => Catchee.SetState(new FreeReachable(Catchee));

        public override void OnUnreachable() 
            => Catchee.SetState(new FreeUnreachable(Catchee));

        public override void ExitState()
        {
            Catchee.crosshairHolder.transform.localScale = Vector3.one * 0.2f; // FIXME MN 
        }
    }
}