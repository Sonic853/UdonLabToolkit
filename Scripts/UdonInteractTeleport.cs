using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonInteractTeleport : UdonSharpBehaviour
    {
        [Header("点击后将玩家传送到以下的位置")]
        [Header("The player will be teleported to the following location after clicking")]
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
        public void Interact_()
        {
            if (isOnce && isTriggered)
                return;
            if (posTransform != null)
                Networking.LocalPlayer.TeleportTo(posTransform.position, posTransform.rotation);
            isTriggered = true;
        }
        public override void Interact()
        {
            Interact_();
        }
    }
}
