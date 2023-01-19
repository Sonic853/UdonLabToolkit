using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonTriggerTeleport : UdonSharpBehaviour
    {
        [Header("进入后将玩家传送到以下的位置")]
        [Header("The player will be teleported to the specified location after entering")]
        [SerializeField] private Transform posTransform;
        [NonSerialized] public VRCPlayerApi OnPlayerTriggerEnter_VRCPlayerApi_ = null;
        public void OnPlayerTriggerEnter_()
        {
            var player = OnPlayerTriggerEnter_VRCPlayerApi_;
            if (player.isLocal && posTransform != null)
                Networking.LocalPlayer.TeleportTo(posTransform.position, posTransform.rotation);
        }
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            OnPlayerTriggerEnter_VRCPlayerApi_ = player;
            OnPlayerTriggerEnter_();
            OnPlayerTriggerEnter_VRCPlayerApi_ = null;
        }
    }
}
