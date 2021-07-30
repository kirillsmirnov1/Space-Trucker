using System;
using Trucker.Control.Zap;
using Trucker.Model.Entities;
using Trucker.Model.Zap;
using UnityEngine;

namespace Trucker.Model.Questing.Consequences
{
    [CreateAssetMenu(fileName = "Consequence_DestroyCatchedObjects", menuName = "Quests/Consequences/Destroy Catched", order = 0)]
    public class DestroyCatchedObjects : Consequence
    {
        public static event Action<EntityType, int> OnObjectsDestroyed; 
        
        [SerializeField] private EntityType typeToDestroy;
        [SerializeField] private int numberOfObjectsToDestroy;
        [SerializeField] private ZapCatcherVariable zapCatcherVariable;

        private ZapCatcher Catcher => zapCatcherVariable.Value;
        
        public override void Invoke()
        {
            for (var i = 0; i < numberOfObjectsToDestroy; i++)
            {
                var catchee = Catcher.TryFree(typeToDestroy);
                Destroy(catchee.gameObject);
            }
            
            OnObjectsDestroyed?.Invoke(typeToDestroy, numberOfObjectsToDestroy);
        }
    }
}