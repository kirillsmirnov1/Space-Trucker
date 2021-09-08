using Trucker.Control.Zap.Catchee;
using Trucker.Control.Zap.Catchee.States;
using Trucker.Model.Entities;
using Trucker.Model.Zap;
using UnityEngine;

namespace Trucker.Model.Questing.Steps.Operations
{
    [CreateAssetMenu(menuName = "Quests/Operations/GuaranteeCatcherHasObjects", fileName = "GuaranteeCatcherHasObjects", order = 0)]
    public class GuaranteeCatcherHasObjects : Operation
    {
        [SerializeField] private ZapCatcherVariable zapCatcherVariable;
        [SerializeField] private TypesOfCatchedObjects catchedByTypes;

        [SerializeField] private EntityType typeToCheck;
        [SerializeField] private GameObject[] prefabs;
        
        public override void Start()
        {
            var needToSpawn = !CatcheeHasRequiredObjects();
            if (needToSpawn) Spawn();
            onCompleted?.Invoke();
        }

        private bool CatcheeHasRequiredObjects() 
            => catchedByTypes.Count(new[] {typeToCheck}) == prefabs.Length;

        private void Spawn()
        {
            var zapCatcher = zapCatcherVariable.Value;
            var pos = zapCatcher.transform.position;
            foreach (var prefab in prefabs)
            {
                var newGo = Instantiate(prefab, pos, Quaternion.identity);
                var catchee = newGo.GetComponent<ZapCatchee>();
                catchee.SetState(new Catched(catchee));
            }
        }
    }
}