using Trucker.Control.Meteor;
using UnityEngine;

namespace Trucker.Control.Zap.Catchee
{
    public class ZapCatcheeOnOrbit : ZapCatchee
    {
        [Header("On Orbit / Components")]
        [SerializeField] private ObjectOnOrbitSpeed orbitSpeed;

        public override void OnCatch()
        {
            base.OnCatch();
            orbitSpeed.enabled = false;
        }

        public override void OnFree()
        {
            base.OnFree();
            orbitSpeed.enabled = true;
            orbitSpeed.SetPersonalOrbitRadius();
        }
    }
}