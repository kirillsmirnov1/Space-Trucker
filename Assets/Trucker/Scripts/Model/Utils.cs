using UnityEditor;
using UnityEngine;
using UnityUtils;

namespace Trucker.Model
{
    public static class Utils
    {
#if UNITY_EDITOR
        public static T[] GetAllSoInstances<T>() where T : ScriptableObject // TODO move to UU 
        {
            var guids = AssetDatabase.FindAssets("t:" + typeof(T).Name); //FindAssets uses tags check documentation for more info
            var instances = new T[guids.Length];
            for (var i = 0; i < guids.Length; i++) //probably could get optimized 
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                instances[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }
            return instances;
        }
#endif

        public static Vector3 GetRandomPointInsideCollider(this System.Random random, BoxCollider boxCollider)
        {
            Vector3 extents = boxCollider.size / 2f;
            Vector3 point = new Vector3(
                random.NextFloat(-extents.x, extents.x),
                random.NextFloat(-extents.y, extents.y),
                random.NextFloat(-extents.z, extents.z)
            );
            return boxCollider.transform.TransformPoint(point);
        }
    }
}