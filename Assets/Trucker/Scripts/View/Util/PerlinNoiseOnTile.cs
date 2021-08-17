using UnityEngine;

namespace Trucker.View.Util
{
    public class PerlinNoiseOnTile : PerlinNoise
    {
        [Header("Tile modulation")]
        [SerializeField] private Sprite tile;
        [SerializeField] private Color tileOffset = new Color(0.1f, 0.1f, 0.1f);
        
        private Color[] _tileColors;

        private void OnValidate()
        {
            dimensions = new Vector2Int((int) tile.rect.size.x, (int) tile.rect.size.y);
        }

        protected override void Awake()
        {
            base.Awake();
            _tileColors = tile.texture.GetPixels();
        }

        protected override void SetTexturePixel(int i, int j, Color newColor)
        {
            
            var tileColor = _tileColors[i * dimensions.x + j] + tileOffset;
            var tiledColor = newColor * tileColor;
            Texture.SetPixel(i, j, tiledColor);
        }
    }
}