using UnityEngine;
using UnityUtils.Saves;
using UnityUtils.Variables;

namespace Trucker.Model.Zap.Level
{
    [CreateAssetMenu(menuName = "Data/ZapPermissionCheck", fileName = "ZapPermissionCheck", order = 0)]
    public class ZapPermissionCheck : InitiatedScriptableObject
    {
        [Header("Data")]
        [SerializeField] private ZapLevelVariable levelVariable;
        [SerializeField] private IntVariable catchedObjectsCount;

        [Header("Variables")]
        [SerializeField] private BoolVariable hasSpaceForNewCatch;
        [SerializeField] private BoolVariable protectionEnabled;

        private bool HasSpaceForNewCatch(ZapLevel level) 
            => catchedObjectsCount.Value < (int) level;

        private static bool ProtectionEnabled(ZapLevel level)
            => level == ZapLevel.Plus;

        public override void Init()
        {
            SetValues(levelVariable);
            levelVariable.OnChange += SetValues;
        }

        private void SetValues(ZapLevel newLevel)
        {
            hasSpaceForNewCatch.Value = HasSpaceForNewCatch(newLevel);
            protectionEnabled.Value = ProtectionEnabled(newLevel);
        }
    }
}