using UnityEngine;
using UnityEngine.UI;
using UnityUtils.Variables;

namespace Trucker.View.Ui
{
    public class FloatSlider : MonoBehaviour
    {
        [SerializeField] private FloatVariable variable;
        [SerializeField] private Slider slider;

        private void OnEnable()
        {
            slider.value = variable;
        }

        public void SetValue(float value)
        {
            variable.Value = value;
        }
    }
}