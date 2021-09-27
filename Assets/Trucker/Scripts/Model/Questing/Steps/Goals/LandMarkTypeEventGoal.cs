using Trucker.Model.Landmarks;
using UnityEngine;

namespace Trucker.Model.Questing.Steps.Goals
{
    [CreateAssetMenu(menuName = "Quests/Goals/LandMarkTypeEventGoal", fileName = "LandMarkTypeEventGoal", order = 0)]
    public class LandMarkTypeEventGoal : Goal
    {
        [SerializeField] private LandmarkTypeEvent landmarkTypeEvent;
        [SerializeField] private LandmarkType requiredType;

        public override void Reset() { /* Can't be reset */ }

        public override void Start()
        {
            base.Start();
            landmarkTypeEvent.RegisterAction(OnEvent);
        }

        public override void Stop()
        {
            base.Stop();
            landmarkTypeEvent.UnregisterAction(OnEvent);
        }

        private void OnEvent(LandmarkType eventType)
        {
            if (eventType == requiredType)
            {
                Complete();
            }
        }
    }
}