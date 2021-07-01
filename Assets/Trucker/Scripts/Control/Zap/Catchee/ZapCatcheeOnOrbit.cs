using Trucker.Control.Meteor;
using UnityEngine;

namespace Trucker.Control.Zap.Catchee
{
    public class ZapCatcheeOnOrbit : ZapCatchee
    {
        [SerializeField] private ObjectOnOrbitSpeed orbitSpeed;

        public override void OnCatch()
        {
            base.OnCatch();
            orbitSpeed.enabled = false;
        }
    }
}