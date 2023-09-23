
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    // [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class UdonShaderColorSlider : UdonSharpBehaviour
    {
        [SerializeField] Material[] materials = new Material[0];
        [SerializeField] Slider _slider;
        [Range(0, 1)]
        // [SerializeField][UdonSynced] float _value;
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
            foreach (var material in materials)
            {
                material.SetColor(_name, new Color(_value, _value, _value));
            }
        }
        void Update()
        {
            _value = _slider.value;
            foreach (var material in materials)
            {
                var _v = material.GetColor(_name).r;
                if (_v != _value)
                {
                    // 将 _v 线性切换到 _value
                    _v = Mathf.Lerp(_v, _value, Time.deltaTime / _lerpTime);
                    material.SetColor(_name, new Color(_v, _v, _v));
                }
            }
        }
    }
}
