using System.Collections;
using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Model.Questing.Steps.Operations
{
    [CreateAssetMenu(menuName = "Quests/Operations/SpawnGrowingPrefabAtTransform", fileName = "SpawnGrowingPrefabAtTransform", order = 0)]
    public class SpawnGrowingPrefabAtTransform : Operation
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private TransformVariable targetTransform;
        [SerializeField] private float growthRate = 1.01f;
        
        public override void Start()
        {
            var obj = Instantiate(prefab, targetTransform);
            FindObjectOfType<MonoBehaviour>().StartCoroutine(Growing(obj.transform));
            onCompleted?.Invoke();
        }

        private IEnumerator Growing(Transform obj)
        {
            while (true)
            {
                obj.localScale *= growthRate;
                yield return null;
            }
        }
    }
}