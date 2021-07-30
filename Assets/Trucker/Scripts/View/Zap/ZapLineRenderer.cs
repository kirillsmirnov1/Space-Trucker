using System.Collections.Generic;
using System.Linq;
using Trucker.Control.Zap;
using Trucker.Control.Zap.Catchee.States;
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
        private List<Transform> _catcheesInProgress;
        
        private void OnValidate() => this.CheckNullFields();

        private void Awake()
        {
            _catcheesInProgress = new List<Transform>();
            _lineMaterial = lineRenderer.material;
            
            Catching.OnCatchingStarted += OnCatchingStarted;
            Catching.OnCatchingFinished += OnCatchingFinished;
        }

        private void OnDestroy()
        {
            Catching.OnCatchingStarted -= OnCatchingStarted;
            Catching.OnCatchingFinished -= OnCatchingFinished;
        }

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
            
            positions.InsertRange(0, _catcheesInProgress.Select(x => x.position));
            
            if (positions.Count != 0)
            {
                positions.Insert(_catcheesInProgress.Count, transform.position);
            }

            if (lineRenderer.positionCount != positions.Count)
            {
                lineRenderer.positionCount = positions.Count;
            }
            
            lineRenderer.SetPositions(positions.ToArray());
        }

        private void OnCatchingStarted(Transform catchee) 
            => _catcheesInProgress.Add(catchee);

        private void OnCatchingFinished(Transform catchee) 
            => _catcheesInProgress.Remove(catchee);
    }
}