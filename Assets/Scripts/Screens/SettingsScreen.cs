using UnityEngine.UI;
using UnityEngine;
using System;

namespace Pong.Screens
{
    public sealed class SettingsScreen : Screen
    {
        [SerializeField] private Material _ballMaterial = default;
        [SerializeField] private Material _saturationMaterial = default;

        [SerializeField] private Slider _colorSlider = default;
        [SerializeField] private Slider _saturationSlider = default;
        [SerializeField] private Slider _brightnessSlider = default;

        private float _h = 1f;
        private float _s = 1f;
        private float _b = 1f;
        private Color _color = default;

        private void Awake()
        {
            _colorSlider.onValueChanged.AddListener(HandleHueValue);
            _brightnessSlider.onValueChanged.AddListener(HandleBrightnessValue);
            _saturationSlider.onValueChanged.AddListener(HandleSaturationValue);
        }

        public override void Open(Action onDone)
        {
            var hsb = Data.DataManager.Instance.GetBallHSB();

            _h = hsb.x; _colorSlider.value = _h;
            _s = hsb.y; _saturationSlider.value = _s;
            _b = hsb.z; _brightnessSlider.value = _b;
            UpdateColor();

            base.Open(onDone);
        }

        public override void Close()
        {
            Data.DataManager.Instance.SetBallHSB(_h, _s, _b);
            base.Close();
        }

        private void HandleHueValue(float value)
        {
            _h = value;
            UpdateColor();
        }

        private void HandleSaturationValue(float value)
        {
            _s = value;
            UpdateColor();
        }

        private void HandleBrightnessValue(float value)
        {
            _b = value/2 + 0.5f;
            UpdateColor();
        }

        private void UpdateColor()
        {
            _color = ColorUtility.HSVtoRGB(_h, 1, 1);
            _saturationMaterial.SetColor("_Color", _color);
            _color = ColorUtility.HSVtoRGB(_h, _s, _b);
            _ballMaterial.SetColor("_Color", _color);
        }


        private void OnDestroy()
        {
            _colorSlider.onValueChanged.RemoveAllListeners();
        }
    }
}