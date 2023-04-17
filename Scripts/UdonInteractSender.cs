
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonInteractSender : UdonSharpBehaviour
    {
        /// <summary>
        /// 需要调用的 Udon Send Functions
        /// </summary>
        [Header("需要调用的 Udon Send")]
        [SerializeField] private UdonBehaviour[] udonSender;
        /// <summary>
        /// 只允许本地玩家触发
        /// </summary>
        [Header("只允许本地玩家触发")]
        [SerializeField] private bool isLocalOnly = true;
        /// <summary>
        /// 只允许触发一次
        /// </summary>
        [Header("只允许触发一次")]
        [SerializeField] private bool isOnce = false;
        /// <summary>
        /// 是否已触发
        /// </summary>
        [NonSerialized] private bool _isInteracted = false;
        public void Interact_()
        {
            if (isOnce && _isInteracted)
                return;
            for (int i = 0; i < udonSender.Length; i++)
            {
                if (udonSender[i] == null)
                    continue;
                udonSender[i].SendCustomEvent("SendFunctions");
            }
            _isInteracted = true;
        }
        public override void Interact()
        {
            if (isLocalOnly)
            {
                Interact_();
            }
            else
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Interact_");
            }
        }
    }
}
