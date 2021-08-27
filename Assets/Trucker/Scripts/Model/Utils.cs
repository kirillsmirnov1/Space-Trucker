using UnityEditor;
using UnityEngine;

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
    }
}