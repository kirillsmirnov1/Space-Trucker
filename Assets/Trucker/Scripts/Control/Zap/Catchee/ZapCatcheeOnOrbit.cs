using Trucker.Control.Asteroid;
using Trucker.Control.Spawn;
using UnityEngine;

namespace Trucker.Control.Zap.Catchee
{
    public class ZapCatcheeOnOrbit : ZapCatchee
    {
        [Header("On Orbit / Components")]
        [SerializeField] private ObjectOnOrbitSpeed orbitSpeed;
        [SerializeField] private Spawnee spawnee;
        
        public override void OnCatch()
        {
            base.OnCatch();
            orbitSpeed.enabled = false;
            spawnee.enabled = false;
        }

        public override void OnFree()
        {
            base.OnFree();
            orbitSpeed.enabled = true;
            spawnee.enabled = true;
            spawnee.ResetPosition();
            orbitSpeed.SetPersonalOrbitRadius();
        }
    }
}