using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonTriggerTeleport : UdonSharpBehaviour
    {
        /// <summary>
        /// 进入后将玩家传送到以下的位置
        /// </summary>
        [Header("进入后将玩家传送到以下的位置")]
        [Header("The player will be teleported to the specified location after entering")]
        [SerializeField] private Transform posTransform;
        /// <summary>
        /// 只允许触发一次
        /// </summary>
        [Header("只允许触发一次")]
        [SerializeField] private bool isOnce = false;
        /// <summary>
        /// 是否已触发
        /// </summary>
        private bool isTriggered = false;
        [NonSerialized] public VRCPlayerApi OnPlayerTriggerEnter_VRCPlayerApi_ = null;
        public void OnPlayerTriggerEnter_()
        {
            if (isOnce && isTriggered)
                return;
            var player = OnPlayerTriggerEnter_VRCPlayerApi_;
            if (player.isLocal && posTransform != null)
                Networking.LocalPlayer.TeleportTo(posTransform.position, posTransform.rotation);
            isTriggered = true;
        }
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            OnPlayerTriggerEnter_VRCPlayerApi_ = player;
            OnPlayerTriggerEnter_();
            OnPlayerTriggerEnter_VRCPlayerApi_ = null;
        }
    }
}
