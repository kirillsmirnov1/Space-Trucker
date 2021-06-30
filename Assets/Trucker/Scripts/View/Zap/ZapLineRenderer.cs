using Trucker.Control.Zap;
using UnityEngine;

namespace Trucker.View.Zap
{
    [RequireComponent(typeof(ZapCatcher))]
    [RequireComponent(typeof(LineRenderer))]
    public class ZapLineRenderer : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private ZapCatcher _zapCatcher;
        
        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _zapCatcher = GetComponent<ZapCatcher>();
        }

        private void FixedUpdate() => UpdatePositions();

        private void UpdatePositions()
        {
            var positions = _zapCatcher.CatcheesPositions;
            
            if (positions.Count != 0)
            {
                positions.Insert(0, transform.position);
            }

            if (_lineRenderer.positionCount != positions.Count)
            {
                _lineRenderer.positionCount = positions.Count;
            }
            
            _lineRenderer.SetPositions(positions.ToArray());
        }
    }
}