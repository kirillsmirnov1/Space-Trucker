using Trucker.Control.Craft.Movement;
using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Control.Zap.Catchee
{
    [RequireComponent(typeof(TrailRenderer))]
    public class CatcheeTrailRendererTime : MonoBehaviour
    {
        [SerializeField] private TrailRenderer trailRenderer;
        [SerializeField] private ZapCatchee catchee;

        [SerializeField] private FloatVariable catchedTrailTimeModificator;
        [SerializeField] private ShipModelParamsVariable shipParams;
        
        private float _baseTrailTime;

        private void Awake()
        {
            _baseTrailTime = trailRenderer.time;
            
            catchee.OnCatched += OnCatched;
            catchee.OnFreed += OnFreed;
        }

        private void OnDestroy()
        {
            catchee.OnCatched -= OnCatched;
            catchee.OnFreed -= OnFreed;
        }

        private void OnFreed()
        {
            trailRenderer.time = _baseTrailTime;
        }

        private void OnCatched()
        {
            trailRenderer.time = _baseTrailTime * catchedTrailTimeModificator * shipParams.Value.maxSpeed;
        }
    }
}