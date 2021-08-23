using UnityEngine;
using UnityEngine.UI;
using UnityUtils.Variables.Display;

namespace Trucker.View.Zap
{
    public class ZapCounterColor : XVariableDisplay<bool>
    {
        [SerializeField] private Image counterSprite;
        [SerializeField] private Color hasSpaceColor;
        [SerializeField] private Color hasNoSpaceColor;
    
        private new void Awake() => variable.OnChange += OnChange;

        protected override void SetText(bool value) 
            => counterSprite.color = value ? hasSpaceColor : hasNoSpaceColor;
    }
}
