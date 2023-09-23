
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class LightController : UdonSharpBehaviour
    {
        [SerializeField] private Light[] lights;
        [SerializeField] private GameObject point;
        [SerializeField] private GameObject target;
        private Vector3 _targetPosition;
        private Vector3 _targetRotation;
        [SerializeField] private GameObject range;
        [SerializeField] private float rangeMin = 1;
        [SerializeField] private float rangeMax = 20;
        private Vector3 _rangePosition;
        private Vector3 _rangeRotation;
        [SerializeField] private GameObject angle;
        [SerializeField] private float angleMin = 1;
        [SerializeField] private float angleMax = 179;
        private Vector3 _anglePosition;
        private Vector3 _angleRotation;
        [SerializeField] private GameObject intensity;
        [SerializeField] private float intensityMin = 0;
        [SerializeField] private float intensityMax = 30;
        private Vector3 _intensityPosition;
        private Vector3 _intensityRotation;
        float rangeValue = 0;
        float angleValue = 0;
        float intensityValue = 0;
        void Start()
        {
            if (target != null) {
                _targetRotation = point.transform.localPosition;
                _targetRotation = point.transform.localEulerAngles;
            }
            if (range != null) {
                _rangePosition = range.transform.localPosition;
                _rangeRotation = range.transform.localEulerAngles;
            }
            if (angle != null) {
                _anglePosition = angle.transform.localPosition;
                _angleRotation = angle.transform.localEulerAngles;
            }
            if (intensity != null) {
                _intensityPosition = intensity.transform.localPosition;
                _intensityRotation = intensity.transform.localEulerAngles;
            }
        }
        void Update()
        {
            if (point != null && target != null)
            {
                // point 到 target 的距离如果大于 1 ，则移动 target 回到 point 的 1 米处
                if (Vector3.Distance(point.transform.position, target.transform.position) > 1)
                {
                    target.transform.position = Vector3.MoveTowards(target.transform.position, point.transform.position, Vector3.Distance(point.transform.position, target.transform.position) - 1);
                }
                // point 看向 target 的角度为 90 0 0 ，移动 target 时，point 会更新看向 target 的角度
                point.transform.LookAt(target.transform);
                foreach (var light in lights)
                {
                    // 角度渐变到目标角度
                    light.transform.rotation = Quaternion.Slerp(light.transform.rotation, point.transform.rotation, Time.deltaTime * 5);
                }
            }
            if (range != null)
            {
                SetTransform(range, _rangePosition, _rangeRotation);
                rangeValue = Mathf.Lerp(rangeMin, rangeMax, Mathf.InverseLerp(_rangePosition.y, _rangePosition.y - 1, range.transform.localPosition.y));
                foreach (var light in lights)
                {
                    light.range = rangeValue;
                }
            }
            if (angle != null)
            {
                SetTransform(angle, _anglePosition, _angleRotation);
                angleValue = Mathf.Lerp(angleMin, angleMax, Mathf.InverseLerp(_anglePosition.y, _anglePosition.y - 1, angle.transform.localPosition.y));
                foreach (var light in lights)
                {
                    light.spotAngle = angleValue;
                }
            }
            if (intensity != null)
            {
                SetTransform(intensity, _intensityPosition, _intensityRotation);
                intensityValue = Mathf.Lerp(intensityMin, intensityMax, Mathf.InverseLerp(_intensityPosition.y, _intensityPosition.y - 1, intensity.transform.localPosition.y));
                foreach (var light in lights)
                {
                    light.intensity = intensityValue;
                }
            }
        }
        // 复位 point
        public void ResetTarget()
        {
            if (target != null)
            {
                target.transform.localPosition = _targetPosition;
                target.transform.localEulerAngles = _targetRotation;
            }
        }
        void SetTransform(GameObject obj, Vector3 position, Vector3 rotation)
        {
            obj.transform.rotation = Quaternion.Euler(rotation);
            if (obj.transform.localPosition.y < position.y - 1)
            {
                obj.transform.localPosition = new Vector3(position.x, position.y - 1, position.z);
            }
            if (obj.transform.localPosition.y > position.y)
            {
                obj.transform.localPosition = new Vector3(position.x, position.y, position.z);
            }
            obj.transform.localPosition = new Vector3(position.x, obj.transform.localPosition.y, position.z);
        }
    }
}
