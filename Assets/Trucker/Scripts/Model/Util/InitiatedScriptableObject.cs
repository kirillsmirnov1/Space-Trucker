using UnityEngine;

namespace Trucker.Model.Util
{
    public abstract class InitiatedScriptableObject : ScriptableObject // TODO move to UU 
    {
        public abstract void Init();
    }
}