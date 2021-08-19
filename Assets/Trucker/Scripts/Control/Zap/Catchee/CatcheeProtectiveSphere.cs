using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Control.Zap.Catchee
{
    public class CatcheeProtectiveSphere : MonoBehaviour
    {
        [SerializeField] private ZapCatchee zapCatchee;
        [SerializeField] private BoolVariable zapProtectionEnabled;
        [SerializeField] private GameObject protectiveSphere;

        private void Awake()
        {
            zapCatchee.OnCatched += OnCatched;
            zapCatchee.OnFreed += OnFreed;
            zapProtectionEnabled.OnChange += OnZapProtectionStatusChange;
        }

        private void OnDestroy()
        {
            zapCatchee.OnCatched -= OnCatched;
            zapCatchee.OnFreed -= OnFreed;
            zapProtectionEnabled.OnChange -= OnZapProtectionStatusChange;
        }

        private void OnZapProtectionStatusChange(bool protectionEnabled)
        {
            if (zapCatchee.Catched)
            {
                OnCatched();
            }
        }

        private void OnCatched() 
            => protectiveSphere.SetActive(zapProtectionEnabled);

        private void OnFreed() 
            => protectiveSphere.SetActive(false);
    }
}
