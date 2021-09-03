using System;
using OneLine;
using Trucker.Model.Craft;
using UnityEngine;
using UnityUtils.Attributes;
using UnityUtils.Variables;

namespace Trucker.Control.Craft
{
    public class ShipModelHandler : MonoBehaviour
    {
        [NamedArray(typeof(ShipModel))] [OneLine]
        [SerializeField] private ShipModelData[] modelData;
        
        [Header("Variables")]
        [SerializeField] private ShipModelVariable shipModelVariable;
        [SerializeField] private FloatVariable thrustMod;
        
        [Header("Set default mesh")]
        [SerializeField] private GameObject lastMesh;
        
        private void Awake()
        {
            SetShipModel(shipModelVariable);
            shipModelVariable.OnChange += SetShipModel;
        }

        private void OnDestroy()
        {
            shipModelVariable.OnChange -= SetShipModel;
        }

        private void SetShipModel(ShipModel newShipModel)
        {
            var newModelData = Data(newShipModel);
            SetMesh(newModelData.mesh);
            SetThrustMod(newModelData.thrustMod);
        }

        private void SetThrustMod(float newThrustMod)
        {
            thrustMod.Value = newThrustMod;
        }

        private void SetMesh(GameObject newMesh)
        {
            newMesh.gameObject.SetActive(true);
            lastMesh.SetActive(false);
            lastMesh = newMesh;
        }

        private ShipModelData Data(ShipModel model)
            => modelData[(int) model];
    }

    [Serializable]
    public struct ShipModelData
    {
        public GameObject mesh;
        public float thrustMod;
    }
}