
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class FollowPlayer : UdonSharpBehaviour
    {
        private VRCPlayerApi _player;
        /// <summary>
        /// 最长距离，超过这个距离将拉至这个距离，-1 为不限制
        /// </summary>
        [Header("最长距离，超过这个距离将拉至这个距离")]
        [SerializeField] private float _maxDistance = 1;
        /// <summary>
        /// 改变速度
        /// </summary>
        [Header("改变速度")]
        [SerializeField] private float _speed = 10;
        void LateUpdate()
        {
            if (_player == null)
            {
                _player = Networking.LocalPlayer;
                return;
            }
            var playerPosition = _player.GetPosition();
            if (_maxDistance != -1)
            {
                // 限制 transform.position 到 _player.GetPosition() 的距离，超过 _maxDistance 将拉至 _maxDistance
                // 计算 transform.position 到 _player.GetPosition() 的距离
                float distance = Vector3.Distance(transform.position, playerPosition);
                if (distance > _maxDistance)
                {
                    // 将 transform.position 拉至 _player.GetPosition() 的 _maxDistance 距离
                    transform.position = Vector3.MoveTowards(transform.position, playerPosition, distance - _maxDistance);
                }
            }
            transform.position = Vector3.Lerp(transform.position, playerPosition, Time.deltaTime * _speed);
            // 只改变 Y 轴 Lerp
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, _player.GetRotation().eulerAngles.y, 0), Time.deltaTime * _speed);
        }
    }
}
