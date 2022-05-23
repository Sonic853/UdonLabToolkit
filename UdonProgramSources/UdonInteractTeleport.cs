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
        public override void Interact()
        {
            if (posTransform != null)
                Networking.LocalPlayer.TeleportTo(posTransform.position, posTransform.rotation);
        }
    }
}
