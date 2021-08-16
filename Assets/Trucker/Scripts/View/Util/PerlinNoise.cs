using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PerlinNoise : MonoBehaviour
{
    [SerializeField] private Renderer rendererToBeChanged;
    [SerializeField] private Vector2Int dimensions = new Vector2Int(256, 256);
    [SerializeField] private float scale = 1f;
    [SerializeField] private Color baseColor;
    [SerializeField] private float offsetSpeed = 1f;
    
    private Vector2 _offset;
    private Texture2D _texture;
    private Action _onUpdate;

    private void Awake()
    {
        SetNewTexture();
    }

    public void SetNewTexture()
    {
        rendererToBeChanged.material.mainTexture = _texture = new Texture2D(dimensions.x, dimensions.y);
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

    private void GenerateTexture()
    {
        for (var i = 0; i < dimensions.x; i++)
        {
            for (var j = 0; j < dimensions.y; j++)
            {
                var newColor = CalculateColor(i, j);
                _texture.SetPixel(i, j, newColor);
            }
        }
        
        _texture.Apply();
    }

    private Color CalculateColor(int x, int y)
    {
        var xScaled = (float) x / dimensions.x * scale + _offset.x;
        var yScaled = (float) y / dimensions.y * scale + _offset.y;
        
        var sample = Mathf.PerlinNoise(xScaled, yScaled);
        return baseColor * sample;
    }
}
