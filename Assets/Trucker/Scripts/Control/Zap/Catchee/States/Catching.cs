using System;
using UnityEngine;

namespace Trucker.Control.Zap.Catchee.States
{
    public class Catching : ZapCatcheeState
    {
        public static event Action<Transform> OnCatchingStarted;
        public static event Action<Transform> OnCatchingFinished;
        
        public Catching(ZapCatchee catchee) : base(catchee) { }

        private Material _defaultMaterial;
        private Material _progressMaterial;
        private float _timeCatching;
        private static readonly int Arc1 = Shader.PropertyToID("_Arc1");

        public override void EnterState()
        {
            OnCatchingStarted?.Invoke(Catchee.transform);
            SetProgressMaterial();
        }

        private void SetProgressMaterial()
        {
            _defaultMaterial = Catchee.progressDisplay.material;
            _progressMaterial = new Material(_defaultMaterial);
            Catchee.progressDisplay.material = _progressMaterial;
        }

        public override void OnUpdate()
        {
            ApproachCatcher();
            UpdateCatchingProgressDisplay();
        }

        private void ApproachCatcher()
        {
            var dir = Catchee.Catcher.transform.position - Catchee.transform.position;
            var translation = dir * (Time.deltaTime * Catchee.approachSpeed);
            Catchee.transform.Translate(translation);
        }

        private void UpdateCatchingProgressDisplay()
        {
            _timeCatching += Time.deltaTime;

            var progress = Mathf.Lerp(360f, 0f, _timeCatching/Catchee.catchingDuration);
            _progressMaterial.SetFloat(Arc1, progress);
            
            if (_timeCatching >= Catchee.catchingDuration)
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
            Catchee.progressDisplay.material = _defaultMaterial;
            OnCatchingFinished?.Invoke(Catchee.transform);
        }
    }
}