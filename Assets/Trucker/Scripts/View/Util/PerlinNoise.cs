using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Trucker.View.Util
{
    public class PerlinNoise : MonoBehaviour
    {
        [SerializeField] protected Renderer rendererToBeChanged;
        [SerializeField] protected Vector2Int dimensions = new Vector2Int(256, 256);
        [SerializeField] private float scale = 1f;
        [SerializeField] private Color baseColor;
        [SerializeField] private float offsetSpeed = 1f;
    
        private Vector2 _offset;
        protected Texture2D Texture;
        private Action _onUpdate;

        protected virtual void Awake()
        {
            SetNewTexture();
        }

        public void SetNewTexture()
        {
            rendererToBeChanged.material.mainTexture = Texture = new Texture2D(dimensions.x, dimensions.y);
        }

        private void OnBecameVisible() => _onUpdate = RegenerateTexture;

        private void OnBecameInvisible() => _onUpdate = null;

        private void Update() => _onUpdate?.Invoke();

        public void RegenerateTexture()
        {
            RandomizeOffset();
            GenerateTexture();
        }

        private void RandomizeOffset()
        {
            _offset.x += Random.Range(0, offsetSpeed) * Time.deltaTime;
            _offset.y += Random.Range(0, offsetSpeed) * Time.deltaTime;
        }

        protected virtual void GenerateTexture()
        {
            for (var i = 0; i < dimensions.x; i++)
            {
                for (var j = 0; j < dimensions.y; j++)
                {
                    var newColor = CalculateNoiseForPixel(i, j) * baseColor;
                    SetTexturePixel(i, j, newColor);
                }
            }
        
            Texture.Apply();
        }

        private float CalculateNoiseForPixel(int x, int y)
        {
            var xScaled = (float) x / dimensions.x * scale + _offset.x;
            var yScaled = (float) y / dimensions.y * scale + _offset.y;
        
            var sample = Mathf.PerlinNoise(xScaled, yScaled);
            return sample;
        }

        protected virtual void SetTexturePixel(int i, int j, Color newColor)
        {
            Texture.SetPixel(i, j, newColor);
        }
    }
}
