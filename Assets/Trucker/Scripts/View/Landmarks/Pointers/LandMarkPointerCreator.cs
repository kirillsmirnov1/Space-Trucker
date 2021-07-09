using UnityEngine;
using UnityUtils;
using UnityUtils.Variables;

namespace Trucker.View.Landmarks.Pointers
{
    public class LandMarkPointerCreator : MonoBehaviour
    {
        [SerializeField] private GameObject landmarkPointerPrefab;
        [SerializeField] private CanvasVariable canvasForPointersVariable;
        [SerializeField] private Sprite pointerSprite;
        [SerializeField] private Landmark landmark;

        private void OnValidate() => this.CheckNullFields();

        private void Awake()
        {
            Instantiate(landmarkPointerPrefab, canvasForPointersVariable.Value.transform)
                .GetComponent<LandmarkPointer>()
                .Init(pointerSprite, landmark);    
        }
    }
}