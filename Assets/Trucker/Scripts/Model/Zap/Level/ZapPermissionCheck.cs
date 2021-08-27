using UnityEngine;
using UnityUtils.Saves;
using UnityUtils.Variables;

namespace Trucker.Model.Zap.Level
{
    [CreateAssetMenu(menuName = "Logic/ZapPermissionCheck", fileName = "ZapPermissionCheck", order = 0)]
    public class ZapPermissionCheck : InitiatedScriptableObject
    {
        [Header("Data")]
        [SerializeField] private ZapLevelVariable levelVariable;
        [SerializeField] private IntVariable catchedObjectsCount;

        [Header("Variables")]
        [SerializeField] private BoolVariable hasSpaceForNewCatch;
        [SerializeField] private BoolVariable protectionEnabled;

        private bool HasSpaceForNewCatch() 
            => catchedObjectsCount.Value < (int) levelVariable.Value;

        private static bool ProtectionEnabled(ZapLevel level)
            => level == ZapLevel.Plus;

        public override void Init()
        {
            SetValues(levelVariable);
            levelVariable.OnChange += SetValues;
            catchedObjectsCount.OnChange += CheckForSpace;
        }

        private void SetValues(ZapLevel newLevel)
        {
            CheckForSpace(catchedObjectsCount);
            protectionEnabled.Value = ProtectionEnabled(newLevel);
        }

        private void CheckForSpace(int catchedObjects)
        {
            hasSpaceForNewCatch.Value = HasSpaceForNewCatch();
        }
    }
}