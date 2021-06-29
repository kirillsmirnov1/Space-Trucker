using UnityEngine;

namespace Trucker.Model.Zap
{
    [CreateAssetMenu(fileName = "SpringJointSettings", menuName = "Data/SpringJointSettings", order = 0)]
    public class SpringJointSettings : ScriptableObject
    {
        public bool autoConfigureConnectedAnchor = false;
        public float minDistance = 1f;
        public float maxDistance = 2f;
        public float spring = 3f;
        public float damper = 2f;
    }
}