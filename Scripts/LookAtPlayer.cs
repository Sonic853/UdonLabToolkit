
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class LookAtPlayer : UdonSharpBehaviour
    {
        [NonSerialized] public VRCPlayerApi localPlayer;
        /// <summary>
        /// 最大角度，超过这个角度将拉至这个角度，-1 为不限制
        /// </summary>
        [Header("最大角度，超过这个角度将拉至这个角度")]
        [SerializeField] private float _maxAngle = -1;
        // /// <summary>
        // /// 改变速度
        // /// </summary>
        // [Header("改变速度")]
        // [SerializeField] private float _speed = 10;
        // /// <summary>
        // /// 改变 Y 轴
        // /// </summary>
        // [Header("改变 Y 轴")]
        // [SerializeField] private bool _changeY = true;
        // /// <summary>
        // /// 改变 Z 轴
        // /// </summary>
        // [Header("改变 Z 轴")]
        // [SerializeField] private bool _changeZ = true;
        // /// <summary>
        // /// 改变 X 轴
        // /// </summary>
        // [Header("改变 X 轴")]
        // [SerializeField] private bool _changeX = false;
        /// <summary>
        /// 改变 Y 轴对象
        /// </summary>
        [Header("改变 Y 轴对象")]
        [SerializeField] private Transform _changeYTransform;
        /// <summary>
        /// Y 轴角度偏移
        /// </summary>
        [Header("Y 轴角度偏移")]
        [SerializeField] private float _changeYAngleOffset = 0;
        /// <summary>
        /// 改变 Y 轴旋转速度
        /// </summary>
        [Header("改变 Y 轴旋转速度")]
        [SerializeField] private float _speedY = 10;
        /// <summary>
        /// 改变 Z 轴对象
        /// </summary>
        [Header("改变 Z 轴对象")]
        [SerializeField] private Transform _changeZTransform;
        /// <summary>
        /// Z 轴角度偏移
        /// </summary>
        [Header("Z 轴角度偏移")]
        [SerializeField] private float _changeZAngleOffset = 0;
        /// <summary>
        /// 改变 Z 轴旋转速度
        /// </summary>
        [Header("改变 Z 轴旋转速度")]
        [SerializeField] private float _speedZ = 10;
        /// <summary>
        /// 改变 X 轴对象
        /// </summary>
        [Header("改变 X 轴对象")]
        [SerializeField] private Transform _changeXTransform;
        /// <summary>
        /// X 轴角度偏移
        /// </summary>
        [Header("X 轴角度偏移")]
        [SerializeField] private float _changeXAngleOffset = 0;
        /// <summary>
        /// 改变 X 轴旋转速度
        /// </summary>
        [Header("改变 X 轴旋转速度")]
        [SerializeField] private float _speedX = 10;
        void Start()
        {
            if (Networking.LocalPlayer != null) localPlayer = Networking.LocalPlayer;
        }
        void Update()
        {
            if (localPlayer == null)
            {
                localPlayer = Networking.LocalPlayer;
                return;
            }
            // 获取玩家头部位置
            var playerPosition = localPlayer.GetBonePosition(HumanBodyBones.Head);
            // Quaternion rotation = transform.rotation;
            if (_maxAngle != -1)
            {
                // if (_changeY)
                // {
                //     // 计算 物体 看向 _player.GetPosition() 所需的角度 Y
                //     float angleY = Quaternion.LookRotation(playerPosition - transform.position).eulerAngles.y;
                //     // 计算 物体 看向 _player.GetPosition() 所需的角度 Y 与 物体 Y 轴角度 的差值
                //     float angleYDelta = Mathf.DeltaAngle(rotation.eulerAngles.y, angleY);
                //     if (Mathf.Abs(angleYDelta) > _maxAngle)
                //     {
                //         // 将 物体 Y 轴角度 拉至 _player.GetPosition() 的 _maxAngle 角度
                //         rotation = Quaternion.Euler(rotation.eulerAngles.x, angleY + (_maxAngle * Mathf.Sign(angleYDelta)), rotation.eulerAngles.z);
                //     }
                // }
                // if (_changeZ)
                // {
                //     // 计算 物体 看向 _player.GetPosition() 所需的角度 Z
                //     float angleZ = Quaternion.LookRotation(playerPosition - transform.position).eulerAngles.z;
                //     // 计算 物体 看向 _player.GetPosition() 所需的角度 Z 与 物体 Z 轴角度 的差值
                //     float angleZDelta = Mathf.DeltaAngle(rotation.eulerAngles.z, angleZ);
                //     if (Mathf.Abs(angleZDelta) > _maxAngle)
                //     {
                //         // 将 物体 Z 轴角度 拉至 _player.GetPosition() 的 _maxAngle 角度
                //         rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, angleZ + (_maxAngle * Mathf.Sign(angleZDelta)));
                //     }
                // }
                // if (_changeX)
                // {
                //     // 计算 物体 看向 _player.GetPosition() 所需的角度 X
                //     float angleX = Quaternion.LookRotation(playerPosition - transform.position).eulerAngles.x;
                //     // 计算 物体 看向 _player.GetPosition() 所需的角度 X 与 物体 X 轴角度 的差值
                //     float angleXDelta = Mathf.DeltaAngle(rotation.eulerAngles.x, angleX);
                //     if (Mathf.Abs(angleXDelta) > _maxAngle)
                //     {
                //         // 将 物体 X 轴角度 拉至 _player.GetPosition() 的 _maxAngle 角度
                //         rotation = Quaternion.Euler(angleX + (_maxAngle * Mathf.Sign(angleXDelta)), rotation.eulerAngles.y, rotation.eulerAngles.z);
                //     }
                // }
                // // 旋转物体
                // transform.rotation = rotation;
                if (_changeYTransform != null)
                {
                    // 计算 物体 看向 _player.GetPosition() 所需的角度 Y
                    float angleY = Quaternion.LookRotation(playerPosition - _changeYTransform.position).eulerAngles.y;
                    // 计算 物体 看向 _player.GetPosition() 所需的角度 Y 与 物体 Y 轴角度 的差值
                    // float angleYDelta = Mathf.DeltaAngle(_changeYTransform.rotation.eulerAngles.y, angleY);
                    float angleYDelta = Mathf.DeltaAngle(_changeYTransform.rotation.eulerAngles.y, angleY + _changeYAngleOffset);
                    if (Mathf.Abs(angleYDelta) > _maxAngle)
                    {
                        // 将 物体 Y 轴角度 拉至 _player.GetPosition() 的 _maxAngle 角度
                        // _changeYTransform.rotation = Quaternion.Euler(_changeYTransform.rotation.eulerAngles.x, angleY + (_maxAngle * Mathf.Sign(angleYDelta)), _changeYTransform.rotation.eulerAngles.z);
                        _changeYTransform.rotation = Quaternion.Euler(_changeYTransform.rotation.eulerAngles.x, angleY + _changeYAngleOffset + (_maxAngle * Mathf.Sign(angleYDelta)), _changeYTransform.rotation.eulerAngles.z);
                    }
                }
                if (_changeZTransform != null)
                {
                    // 计算 物体 看向 _player.GetPosition() 所需的角度 Z
                    float angleZ = Quaternion.LookRotation(playerPosition - _changeZTransform.position).eulerAngles.z;
                    // 计算 物体 看向 _player.GetPosition() 所需的角度 Z 与 物体 Z 轴角度 的差值
                    // float angleZDelta = Mathf.DeltaAngle(_changeZTransform.rotation.eulerAngles.z, angleZ);
                    float angleZDelta = Mathf.DeltaAngle(_changeZTransform.rotation.eulerAngles.z, angleZ + _changeZAngleOffset);
                    if (Mathf.Abs(angleZDelta) > _maxAngle)
                    {
                        // 将 物体 Z 轴角度 拉至 _player.GetPosition() 的 _maxAngle 角度
                        // _changeZTransform.rotation = Quaternion.Euler(_changeZTransform.rotation.eulerAngles.x, _changeZTransform.rotation.eulerAngles.y, angleZ + (_maxAngle * Mathf.Sign(angleZDelta)));
                        _changeZTransform.rotation = Quaternion.Euler(_changeZTransform.rotation.eulerAngles.x, _changeZTransform.rotation.eulerAngles.y, angleZ + _changeZAngleOffset + (_maxAngle * Mathf.Sign(angleZDelta)));
                    }
                }
                if (_changeXTransform != null)
                {
                    // 计算 物体 看向 _player.GetPosition() 所需的角度 X
                    float angleX = Quaternion.LookRotation(playerPosition - _changeXTransform.position).eulerAngles.x;
                    // 计算 物体 看向 _player.GetPosition() 所需的角度 X 与 物体 X 轴角度 的差值
                    // float angleXDelta = Mathf.DeltaAngle(_changeXTransform.rotation.eulerAngles.x, angleX);
                    float angleXDelta = Mathf.DeltaAngle(_changeXTransform.rotation.eulerAngles.x, angleX + _changeXAngleOffset);
                    if (Mathf.Abs(angleXDelta) > _maxAngle)
                    {
                        // 将 物体 X 轴角度 拉至 _player.GetPosition() 的 _maxAngle 角度
                        // _changeXTransform.rotation = Quaternion.Euler(angleX + (_maxAngle * Mathf.Sign(angleXDelta)), _changeXTransform.rotation.eulerAngles.y, _changeXTransform.rotation.eulerAngles.z);
                        _changeXTransform.rotation = Quaternion.Euler(angleX + _changeXAngleOffset + (_maxAngle * Mathf.Sign(angleXDelta)), _changeXTransform.rotation.eulerAngles.y, _changeXTransform.rotation.eulerAngles.z);
                    }
                }
            }
            // if (_changeY)
            // {
            //     // 计算 物体 看向 _player.GetPosition() 所需的角度 Y
            //     float angleY = Quaternion.LookRotation(playerPosition - transform.position).eulerAngles.y;
            //     // 将 物体 Y 轴角度 拉至 _player.GetPosition() Lerp
            //     rotation = Quaternion.Euler(rotation.eulerAngles.x, Mathf.LerpAngle(rotation.eulerAngles.y, angleY, Time.deltaTime * _speed), rotation.eulerAngles.z);
            // }
            // if (_changeZ)
            // {
            //     // 计算 物体 看向 _player.GetPosition() 所需的角度 Z
            //     float angleZ = Quaternion.LookRotation(playerPosition - transform.position).eulerAngles.z;
            //     // 将 物体 Z 轴角度 拉至 _player.GetPosition() Lerp
            //     rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, Mathf.LerpAngle(rotation.eulerAngles.z, angleZ, Time.deltaTime * _speed));
            // }
            // if (_changeX)
            // {
            //     // 计算 物体 看向 _player.GetPosition() 所需的角度 X
            //     float angleX = Quaternion.LookRotation(playerPosition - transform.position).eulerAngles.x;
            //     // 将 物体 X 轴角度 拉至 _player.GetPosition() Lerp
            //     rotation = Quaternion.Euler(Mathf.LerpAngle(rotation.eulerAngles.x, angleX, Time.deltaTime * _speed), rotation.eulerAngles.y, rotation.eulerAngles.z);
            // }
            // // 旋转物体
            // transform.rotation = rotation;
            if (_changeYTransform != null)
            {
                // 计算 物体 看向 _player.GetPosition() 所需的角度 Y
                float angleY = Quaternion.LookRotation(playerPosition - _changeYTransform.position).eulerAngles.y;
                // 将 物体 Y 轴角度 拉至 _player.GetPosition() Lerp
                // _changeYTransform.rotation = Quaternion.Euler(_changeYTransform.rotation.eulerAngles.x, Mathf.LerpAngle(_changeYTransform.rotation.eulerAngles.y, angleY, Time.deltaTime * _speed), _changeYTransform.rotation.eulerAngles.z);
                _changeYTransform.rotation = Quaternion.Euler(_changeYTransform.rotation.eulerAngles.x, Mathf.LerpAngle(_changeYTransform.rotation.eulerAngles.y, angleY + _changeYAngleOffset, Time.deltaTime * _speedY), _changeYTransform.rotation.eulerAngles.z);
            }
            if (_changeZTransform != null)
            {
                // 计算 物体 看向 _player.GetPosition() 所需的角度 Z
                float angleZ = Quaternion.LookRotation(playerPosition - _changeZTransform.position).eulerAngles.z;
                // 将 物体 Z 轴角度 拉至 _player.GetPosition() Lerp
                // _changeZTransform.rotation = Quaternion.Euler(_changeZTransform.rotation.eulerAngles.x, _changeZTransform.rotation.eulerAngles.y, Mathf.LerpAngle(_changeZTransform.rotation.eulerAngles.z, angleZ, Time.deltaTime * _speed));
                _changeZTransform.rotation = Quaternion.Euler(_changeZTransform.rotation.eulerAngles.x, _changeZTransform.rotation.eulerAngles.y, Mathf.LerpAngle(_changeZTransform.rotation.eulerAngles.z, angleZ + _changeZAngleOffset, Time.deltaTime * _speedZ));
            }
            if (_changeXTransform != null)
            {
                // 计算 物体 看向 _player.GetPosition() 所需的角度 X
                float angleX = Quaternion.LookRotation(playerPosition - _changeXTransform.position).eulerAngles.x;
                // 将 物体 X 轴角度 拉至 _player.GetPosition() Lerp
                // _changeXTransform.rotation = Quaternion.Euler(Mathf.LerpAngle(_changeXTransform.rotation.eulerAngles.x, angleX, Time.deltaTime * _speed), _changeXTransform.rotation.eulerAngles.y, _changeXTransform.rotation.eulerAngles.z);
                _changeXTransform.rotation = Quaternion.Euler(Mathf.LerpAngle(_changeXTransform.rotation.eulerAngles.x, angleX + _changeXAngleOffset, Time.deltaTime * _speedX), _changeXTransform.rotation.eulerAngles.y, _changeXTransform.rotation.eulerAngles.z);
            }
        }
    }
}
