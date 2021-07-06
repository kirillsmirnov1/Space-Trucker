using UnityEngine;

namespace Trucker.Control.Zap.Catchee.States
{
    public class Freeing : ZapCatcheeState
    {
        public Freeing(ZapCatchee catchee) : base(catchee) { }

        private Material _defaultMaterial;
        private Material _progressMaterial;
        private float _timeFreeing;
        private static readonly int Arc1 = Shader.PropertyToID("_Arc1");

        public override void EnterState()
        {
            Catchee.crosshairHolder.gameObject.SetActive(true);
            SetProgressMaterial();
        }

        private void SetProgressMaterial()
        {
            _defaultMaterial = Catchee.progressDisplay.material;
            _progressMaterial = new Material(_defaultMaterial);
            Catchee.progressDisplay.material = _progressMaterial;
            _progressMaterial.color = Color.red;
        }

        public override void OnUpdate() 
            => UpdateFreeingProgressDisplay();

        private void UpdateFreeingProgressDisplay()
        {
            _timeFreeing += Time.deltaTime;
            
            var progress = Mathf.Lerp(0f, 360f, _timeFreeing / Catchee.catchingDuration);
            _progressMaterial.SetFloat(Arc1, progress);

            if (_timeFreeing >= Catchee.catchingDuration)
            {
                Free();
            }
        }

        private void Free()
        {
            if (Catchee.Catcher.TryFree(Catchee))
            {
                Catchee.OnFree();
            }
        }

        public override void OnPointerUp() 
            => Catchee.SetState(new Catched(Catchee));

        public override void ExitState() 
            => Catchee.progressDisplay.material = _defaultMaterial;
    }
}