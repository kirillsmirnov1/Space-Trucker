using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Control.Zap
{
    public class ZapCatcheeCrosshair : MonoBehaviour
    {
        [SerializeField] private TransformVariable catcher;
        [SerializeField] private Transform crosshairHolder;
        
        private void Update()
        {
            var lookPos = catcher.Value.position - crosshairHolder.position;
            var rotation = Quaternion.LookRotation(lookPos);
            crosshairHolder.rotation = rotation;
        }
    }
}