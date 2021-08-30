using Trucker.Control.Spawn;
using UnityEngine;

namespace Trucker.Model.Questing.Steps.Operations
{
    [CreateAssetMenu(menuName = "Quests/Operations/SetUraniumSpawnCount", fileName = "SetUraniumSpawnCount", order = 0)]
    public class SetUraniumSpawnCount : Operation
    {
        [SerializeField] private UraniumSpawnCount uraniumSpawnCount;
        [SerializeField] private int newOrbitCount;
        
        public override void Start()
        {
            uraniumSpawnCount.SetOrbitCount(newOrbitCount);
            onCompleted?.Invoke();
        }
    }
}