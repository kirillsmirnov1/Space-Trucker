using UnityEngine;

namespace Trucker.Control.Zap.Catchee.States
{
    public class Catching : ZapCatcheeState
    {
        private Vector3 FillerScale
        {
            get => Catchee.crosshairFiller.transform.localScale;
            set => Catchee.crosshairFiller.transform.localScale = value;
        }
        
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
            // TODO change through shader 
            // FIXME speed 
            FillerScale += Time.deltaTime * Vector3.one;
            if (FillerScale.magnitude >= 1)
            {
                Catchee.SetState(new Catched(Catchee));
            }
        }

        public override void OnPointerUp() 
            => Catchee.SetState(new FreeReachable(Catchee));

        public override void OnUnreachable() 
            => Catchee.SetState(new FreeUnreachable(Catchee));

        public override void ExitState() 
            => FillerScale = Vector3.one * 0.2f;
    }
}