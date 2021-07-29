using UnityEngine;

namespace Trucker.Model.Questing.Consequences
{
    // [CreateAssetMenu(fileName = "Consequence", menuName = "Quests/Consequences/Consequence", order = 0)]
    public abstract class Consequence : ScriptableObject
    {
        public abstract void Invoke();
    }
}