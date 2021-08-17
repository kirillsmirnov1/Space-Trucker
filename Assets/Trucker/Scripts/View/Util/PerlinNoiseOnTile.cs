using UnityEngine;

namespace Trucker.View.Util
{
    public class PerlinNoiseOnTile : PerlinNoise // IMPR turn into shader 
    {
        [Header("Tile modulation")]
        [SerializeField] private Sprite tile;
        [SerializeField] private Color tileOffset = new Color(0.1f, 0.1f, 0.1f);
        [SerializeField] private float tileScale = 1f;
        
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
            var flatCoord = i * dimensions.x + j;
            var scaledCoord = (int)((flatCoord * tileScale) % _tileColors.Length);
            var tileColor = _tileColors[scaledCoord] + tileOffset;
            var tiledColor = newColor * tileColor;
            Texture.SetPixel(i, j, tiledColor);
        }
    }
}