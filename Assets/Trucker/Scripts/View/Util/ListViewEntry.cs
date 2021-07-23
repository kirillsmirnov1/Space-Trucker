using UnityEngine;

namespace Trucker.View.Util
{
    public abstract class ListViewEntry<T> : MonoBehaviour
    {
        public virtual void Fill(T data)
        {
            gameObject.SetActive(true);
        }
    }
}