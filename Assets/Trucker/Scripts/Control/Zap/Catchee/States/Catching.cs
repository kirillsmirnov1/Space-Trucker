using UnityEngine;

namespace Trucker.Control.Zap.Catchee.States
{
    public class Catching : ZapCatcheeState
    {
        
        public Catching(ZapCatchee catchee) : base(catchee) { }

        private Material _defaultMaterial;
        private Material _progressMaterial;
        private float _progress = 360f;
        private static readonly int Arc1 = Shader.PropertyToID("_Arc1");

        public override void EnterState() 
            => SetProgressMaterial();

        private void SetProgressMaterial()
        {
            _defaultMaterial = Catchee.progressDisplay.material;
            _progressMaterial = new Material(_defaultMaterial);
            Catchee.progressDisplay.material = _progressMaterial;
        }

        public override void OnUpdate()
        {
            // IMPR draw ray 
            // IMPR slowly move towards Catcher 
            UpdateCatchingProgressDisplay();
        }

        private void UpdateCatchingProgressDisplay()
        {
            // FIXME speed 

            _progress -= Time.deltaTime * 100;
            _progressMaterial.SetFloat(Arc1, _progress);
            
            if (_progress <= 0)
            {
                Catchee.SetState(new Catched(Catchee));
            }
        }

        public override void OnPointerUp() 
            => Catchee.SetState(new FreeReachable(Catchee));

        public override void OnUnreachable() 
            => Catchee.SetState(new FreeUnreachable(Catchee));

        public override void ExitState() 
            => Catchee.progressDisplay.material = _defaultMaterial;
    }
}