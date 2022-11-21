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
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (player.isLocal && posTransform != null)
                Networking.LocalPlayer.TeleportTo(posTransform.position, posTransform.rotation);
        }
    }
}
