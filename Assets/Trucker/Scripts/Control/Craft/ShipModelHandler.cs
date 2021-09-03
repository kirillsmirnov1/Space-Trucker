using Trucker.Control.Craft.Movement;
using Trucker.Model.Craft;
using UnityEngine;

namespace Trucker.Control.Craft
{
    public class ShipModelHandler : MonoBehaviour
    {
        [SerializeField] private ShipModelsData modelData;  
        
        [Header("Variables")]
        [SerializeField] private ShipModelVariable shipModelVariable;
        [SerializeField] private ShipModelParamsVariable shipModelParamsVariable;

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
            var newModelData = modelData.Get(newShipModel);
            SetMesh(newModelData.mesh);
            shipModelParamsVariable.Value = newModelData;
        }

        private void SetMesh(GameObject newMesh)
        {
            var currentMesh = shipMeshHolder.GetChild(0).gameObject;
            
            if (currentMesh.name == newMesh.name) return;
            
            Destroy(currentMesh);
            Instantiate(newMesh, shipMeshHolder);
        }
    }
}