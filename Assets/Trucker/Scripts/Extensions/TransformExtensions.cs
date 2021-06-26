using UnityEngine;

namespace Trucker.Extensions
{
    public static class TransformExtensions // IMPR extract to UU 
    {
        public static bool UpsideDown(this Transform transform) => transform.up.y < 0;
    }
}