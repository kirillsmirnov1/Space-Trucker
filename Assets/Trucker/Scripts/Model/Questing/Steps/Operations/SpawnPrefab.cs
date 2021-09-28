using UnityEngine;

namespace Trucker.Model.Questing.Steps.Operations
{
    [CreateAssetMenu(menuName = "Quests/Operations/SpawnPrefab", fileName = "SpawnPrefab", order = 0)]
    public class SpawnPrefab : Operation
    {
        [SerializeField] private GameObject prefab;
        
        public override void Start()
        {
            Instantiate(prefab);
            onCompleted?.Invoke();
        }
    }
}