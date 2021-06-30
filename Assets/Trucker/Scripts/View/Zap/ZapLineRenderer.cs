using Trucker.Control.Zap;
using UnityEngine;
using UnityUtils;
using UnityUtils.Variables;

namespace Trucker.View.Zap
{
    public class ZapLineRenderer : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private ZapCatcher zapCatcher;
        [SerializeField] private FloatVariable lineMoveSpeed;
        
        private Material _lineMaterial;
        
        private void OnValidate() => this.CheckNullFields();

        private void Awake() 
            => _lineMaterial = lineRenderer.material;

        private void Update() 
            => AnimateLine();

        private void FixedUpdate() 
            => UpdatePositions();

        private void AnimateLine()
        {
            var textureShift = Vector2.right * Time.deltaTime * lineMoveSpeed;
            _lineMaterial.mainTextureOffset += textureShift;
        }

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