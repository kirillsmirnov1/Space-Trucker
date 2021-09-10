using Trucker.Control.Zap;
using Trucker.Model.Entities;
using Trucker.Model.Zap;
using UnityEngine;

namespace Trucker.Model.Questing.Steps.Operations
{
    [CreateAssetMenu(fileName = "Consequence_DestroyCatchedObjects", menuName = "Quests/Operations/Destroy Catched", order = 0)]
    public class DestroyCatchedObjects : Operation
    {
        [SerializeField] private EntityType[] typesToDestroy;
        [SerializeField] private int numberOfObjectsToDestroy;
        [SerializeField] private ZapCatcherVariable zapCatcherVariable;

        private ZapCatcher Catcher => zapCatcherVariable.Value;
        
        public override void Start()
        {
            var catchees = Catcher.TryFree(typesToDestroy, numberOfObjectsToDestroy);
            foreach (var catchee in catchees)
            {
                Destroy(catchee.gameObject);
            }
            onCompleted?.Invoke();
        }
    }
}