using UnityEngine;
using UnityUtils.Events;
using UnityUtils.Variables;

namespace Trucker.Control.Spawn
{
    [CreateAssetMenu(menuName = "Logic/UraniumSpawnCount", fileName = "UraniumSpawnCount", order = 0)]
    public class UraniumSpawnCount : ScriptableObject
    {
        [SerializeField] private GameEvent respawnUraniumOnOrbit;
        [SerializeField] private GameEvent respawnUraniumInDump;

        [SerializeField] private IntVariable baseUraniumCount;
        [SerializeField] private IntVariable orbitUraniumCount;
        [SerializeField] private IntVariable dumpUraniumCount;


        public void SetOrbitCount(int value)
        {
            orbitUraniumCount.Value = value;
            dumpUraniumCount.Value = baseUraniumCount.Value - value;
            respawnUraniumOnOrbit.Raise();
            respawnUraniumInDump.Raise();
        }
    }
}