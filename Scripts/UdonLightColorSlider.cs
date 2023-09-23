
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonLightColorSlider : UdonSharpBehaviour
    {
        [SerializeField] Light _light;
        [SerializeField] Slider _slider;
        [Range(0, 1)]
        [SerializeField] float _value;
        [SerializeField] string _name;
        /// <summary>
        /// 过渡时间
        /// </summary>
        [Range(0.001f, 100)]
        [SerializeField] float _lerpTime;
        void Start()
        {
            _value = _slider.value;
            _light.color = new Color(_value, _value, _value);
        }
        void Update()
        {
            _value = _slider.value;
            var _v = _light.color.r;
            if (_v != _value)
            {
                // 将 _v 线性切换到 _value
                _v = Mathf.Lerp(_v, _value, Time.deltaTime / _lerpTime);
                _light.color = new Color(_v, _v, _v);
            }
        }
    }
}
