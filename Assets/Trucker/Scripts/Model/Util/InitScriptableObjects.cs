using System.Collections.Generic;
using UnityEngine;

namespace Trucker.Model.Util
{
    public class InitScriptableObjects : MonoBehaviour
    {
        [SerializeField] private List<InitiatedScriptableObject> objects;
        
        private void Awake()
        {
            foreach (var obj in objects)
            {
                obj.Init();
            }
        }
    }
}