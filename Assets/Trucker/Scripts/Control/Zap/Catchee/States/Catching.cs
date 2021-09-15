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
        private float _duration;
        private static readonly int Arc1 = Shader.PropertyToID("_Arc1");

        public override void EnterState()
        {
            OnCatchingStarted?.Invoke(Catchee.transform);
            Catchee.NotifyOnCatchingStart();
            _duration = Catchee.catcheeSettings.catchingDuration;
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
            var translation = dir * (Time.deltaTime * Catchee.catcheeSettings.approachSpeed);
            Catchee.transform.Translate(translation);
        }

        private void UpdateCatchingProgressDisplay()
        {
            _timeCatching += Time.deltaTime;

            var progress = Mathf.Lerp(360f, 0f, _timeCatching/_duration);
            _progressMaterial.SetFloat(Arc1, progress);
            
            if (_timeCatching >= _duration)
            {
                Catchee.Catcher.TryCatch(Catchee);
            }
        }

        public override void OnPointerUp() 
            => Catchee.SetFreeState();

        public override void OnUnreachable() 
            => Catchee.SetFreeState();

        public override void ExitState()
        {
            Catchee.progressDisplay.material = _defaultMaterial;
            OnCatchingFinished?.Invoke(Catchee.transform);
        }
    }
}