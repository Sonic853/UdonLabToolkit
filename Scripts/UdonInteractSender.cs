
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
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
        /// 放入玩家名字（UdonSendFunctionsWithString Only）
        /// </summary>
        [Header("放入玩家名字")]
        [Tooltip("（UdonSendFunctionsWithString Only）")]
        [SerializeField] private bool sendPlayerName = false;
        /// <summary>
        /// 只允许触发一次
        /// </summary>
        [Header("只允许触发一次")]
        [SerializeField] private bool isOnce = false;
        /// <summary>
        /// 是否已触发
        /// </summary>
        [NonSerialized] private bool _isInteracted = false;
        [UdonSynced] private string _playerName = "";
        VRCPlayerApi localPlayer;
        private void Start()
        {
            if (Networking.LocalPlayer != null) localPlayer = Networking.LocalPlayer;
        }
        public void Interact_()
        {
            if (isOnce && _isInteracted)
                return;
            for (int i = 0; i < udonSender.Length; i++)
            {
                if (udonSender[i] == null)
                    continue;
                if (sendPlayerName
                && !string.IsNullOrEmpty(_playerName))
                {
                    var udonSenderValues = (string[])udonSender[i].GetProgramVariable("values");
                    if (udonSenderValues != null) for (int j = 0; j < udonSenderValues.Length; j++)
                        {
                            udonSenderValues[j] = _playerName;
                        }
                    udonSender[i].SetProgramVariable("values", udonSenderValues);
                }
                udonSender[i].SendCustomEvent("SendFunctions");
            }
            _isInteracted = true;
            _playerName = "";
        }
        public override void Interact()
        {
            if (isLocalOnly)
            {
                if (sendPlayerName
                && localPlayer != null)
                    _playerName = localPlayer.displayName;
                Interact_();
            }
            else if (sendPlayerName
                && localPlayer != null)
            {
                Networking.SetOwner(Networking.LocalPlayer, gameObject);
                _playerName = localPlayer.displayName;
                RequestSerialization();
                OnDeserialization();
            }
            else
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Interact_");
            }
        }
        public override void OnDeserialization()
        {
            if (isLocalOnly)
                return;
            if (sendPlayerName
            && !string.IsNullOrEmpty(_playerName))
            {
                Interact_();
            }
        }
    }
}
