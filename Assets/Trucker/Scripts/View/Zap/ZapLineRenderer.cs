using Trucker.Control.Zap;
using UnityEngine;
using UnityUtils;

namespace Trucker.View.Zap
{
    public class ZapLineRenderer : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private ZapCatcher zapCatcher;

        private void OnValidate() => this.CheckNullFields();

        private void FixedUpdate() => UpdatePositions();

        private void UpdatePositions()
        {
            var positions = zapCatcher.CatcheesPositions;
            
            if (positions.Count != 0)
            {
                positions.Insert(0, transform.position);
            }

            if (lineRenderer.positionCount != positions.Count)
            {
                lineRenderer.positionCount = positions.Count;
            }
            
            lineRenderer.SetPositions(positions.ToArray());
        }
    }
}