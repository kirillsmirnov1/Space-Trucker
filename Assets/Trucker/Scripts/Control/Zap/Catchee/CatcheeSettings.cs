using Trucker.Model.Zap;
using UnityEngine;

namespace Trucker.Control.Zap.Catchee
{
    [CreateAssetMenu(menuName = "Data/CatcheeSettings", fileName = "CatcheeSettings", order = 0)]
    public class CatcheeSettings : ScriptableObject
    {
        [SerializeField] public float catchingDuration;
        [SerializeField] public float approachSpeed;
        [SerializeField] public ZapCatcherVariable zapCatcherVariable;
    }
}