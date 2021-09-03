using OneLine;
using Trucker.Model.Craft;
using UnityEngine;
using UnityUtils.Attributes;

namespace Trucker.Control.Craft.Movement
{
    [CreateAssetMenu(menuName = "Data/ShipModelDataArray", fileName = "ShipModelDataArray", order = 0)]
    public class ShipModelsData : ScriptableObject
    {
        [NamedArray(typeof(ShipModel))] [OneLine]
        [SerializeField] private ShipModelData[] modelData;
        
        public ShipModelData Get(ShipModel model)
            => modelData[(int) model];
    }
}