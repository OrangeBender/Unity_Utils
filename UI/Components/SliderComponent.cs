using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Util.UiComponents
{
    public class SliderComponent : ComponentBase
    {
        [SerializeField] private Slider slider;

        public void SetValue(float value, float max)
        {
            slider.maxValue = max;
            slider.value = value;
        }

        public float GetValue()
        {
            return slider.value;
        }

        public void SetOnValueChanged(UnityAction<float> callBack)
        {
            slider.onValueChanged.AddListener(callBack);
        }

        private void OnDestroy()
        {
            slider.onValueChanged?.RemoveAllListeners();
        }
    }
}