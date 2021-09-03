using System;
using UnityEngine;

namespace Trucker.Control.Craft
{
    [Serializable]
    public class ShipModelData 
    {
        public GameObject mesh; 
        public float thrustMod;
        public float maxSpeed;
    }
}