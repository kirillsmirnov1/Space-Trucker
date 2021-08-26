using UnityEngine;
using UnityUtils.Events;

namespace Trucker.Model.Landmarks
{
    [CreateAssetMenu(menuName = "Events/LandmarkTypeEvent", fileName = "LandmarkTypeEvent", order = 0)]
    public class LandmarkTypeEvent : GenericGameEvent<LandmarkType>
    {
        public void Home() => Raise(LandmarkType.Home);
        public void Institute() => Raise(LandmarkType.Institute);
        public void Diner() => Raise(LandmarkType.Diner);
        public void Dump() => Raise(LandmarkType.Dump);
        public void Dorm() => Raise(LandmarkType.Dorm);
    }
}