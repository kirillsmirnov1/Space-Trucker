﻿namespace Trucker.Control.Zap.Catchee.States
{
    public class FreeReachable : ZapCatcheeState
    {
        public FreeReachable(ZapCatchee catchee) : base(catchee) { }
        
        public override void EnterState() => 
            Catchee.crosshairHolder.SetActive(true);

        public override void OnUnreachable() 
            => Catchee.SetState(new FreeUnreachable(Catchee));

        public override void OnPointerDown() 
            => TryCatch();

        protected virtual void TryCatch()
        {
            if (Catchee.Catcher.TryCatch(Catchee))
            {
                Catchee.OnCatch();
                Catchee.SetState(new Catched(Catchee));
            }
        }
    }
}