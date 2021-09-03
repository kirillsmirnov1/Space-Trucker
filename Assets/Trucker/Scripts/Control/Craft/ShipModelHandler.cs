using System;
using OneLine;
using Trucker.Model.Craft;
using UnityEngine;
using UnityUtils.Attributes;

namespace Trucker.Control.Craft
{
    public class ShipModelHandler : MonoBehaviour
    {
        [SerializeField] private ShipModelVariable shipModelVariable;
        [NamedArray(typeof(ShipModel))] [OneLine]
        [SerializeField] private ShipModelData[] modelData;
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
    }
}