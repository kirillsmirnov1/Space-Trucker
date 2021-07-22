using UnityEngine;

namespace Trucker.View.Util
{
    public abstract class ListViewEntry<T> : MonoBehaviour
    {
        public abstract void Fill(T data);
    }
}