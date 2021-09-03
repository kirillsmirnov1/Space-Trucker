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
        [SerializeField] private ShipModelData[] modelData; // IMPR to SO, pass instances 
        
        [Header("Variables")]
        [SerializeField] private ShipModelVariable shipModelVariable;
        [SerializeField] private FloatVariable thrustMod;
        [SerializeField] private FloatVariable maxSpeed;
        
        [Header("Components")]
        [SerializeField] private Transform shipMeshHolder;
        
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
            thrustMod.Value = newModelData.thrustMod;
            maxSpeed.Value = newModelData.maxSpeed;
        }

        private void SetMesh(GameObject newMesh)
        {
            var currentMesh = shipMeshHolder.GetChild(0).gameObject;
            
            if (currentMesh.name == newMesh.name) return;
            
            Destroy(currentMesh);
            Instantiate(newMesh, shipMeshHolder);
        }

        private ShipModelData Data(ShipModel model)
            => modelData[(int) model];
    }

    [Serializable]
    public struct ShipModelData // IMPR turn to class 
    {
        public GameObject mesh; 
        public float thrustMod;
        public float maxSpeed;
    }
}