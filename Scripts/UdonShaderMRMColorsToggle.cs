
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonShaderMRMColorsToggle : UdonSharpBehaviour
    {
        [SerializeField] MeshRenderer _meshRenderer;
        Material[] materials = new Material[0];
        [Range(0, 1)]
        [SerializeField] float _value;
        [SerializeField] string _name;
        [SerializeField] bool _isOn;
        /// <summary>
        /// 过渡时间
        /// </summary>
        [Range(0.001f, 100)]
        [SerializeField] float _lerpTime;
        void Start()
        {
            if (_meshRenderer != null) materials = _meshRenderer.materials;
            var _v = _isOn ? _value : 0;
            foreach (var material in materials)
            {
                material.SetColor(_name, new Color(_v, _v, _v));
            }
        }
        void Update()
        {
            foreach (var material in materials)
            {
                var _v = material.GetColor(_name).r;
                if (_isOn ? _v < _value : _v > 0)
                {
                    // 将 _v 线性切换到 _value
                    _v = Mathf.Lerp(_v, _isOn ? _value : 0, Time.deltaTime / _lerpTime);
                    material.SetColor(_name, new Color(_v, _v, _v));
                }
            }
        }
        public void Toggle()
        {
            _isOn = !_isOn;
        }
        public void SetOn()
        {
            _isOn = true;
        }
        public void SetOff()
        {
            _isOn = false;
        }
    }
}
