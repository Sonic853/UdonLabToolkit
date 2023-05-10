
using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class PinChecker : UdonSharpBehaviour
    {
        // 由 Host 指定一位玩家做为 Target，计算 Target 和 Host 之间的时间差
        // [SerializeField] private UdonArrayPlus udonArrayPlus;
        // private VRCPlayerApi[] players;
        [SerializeField] private Text pingText;
        [SerializeField] private Text hostText;
        [SerializeField] private Text targetText;
        private bool allSynced = false;
        [UdonSynced] private string host_;
        public string host
        {
            get => host_;
            set
            {
                host_ = value;
                if (hostText != null) hostText.text = $"Host: {value}";
            }
        }
        [UdonSynced] private string target_ = "";
        public string target
        {
            get => target_;
            set
            {
                target_ = value;
                if (targetText != null) targetText.text = $"Target: {value}";
            }
        }
        [UdonSynced] public long time;
        /// <summary>
        /// 偏移，以秒为单位
        /// </summary>
        /// <returns></returns>
        [UdonSynced] public float offset = 0f;
        void Start()
        {
            if (Networking.LocalPlayer != null && Networking.LocalPlayer.isMaster)
            {
                host = Networking.LocalPlayer.displayName;
            }
        }
        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            // if (Networking.LocalPlayer != null && Networking.LocalPlayer.isMaster)
            // {
            //     host = Networking.LocalPlayer.displayName;
            // }
            // players = udonArrayPlus.Players();
        }
        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            if (Networking.LocalPlayer != null && Networking.LocalPlayer.isMaster && player.displayName == host)
            {
                host = Networking.LocalPlayer.displayName;
            }
            if (player.displayName == target)
            {
                target = "";
                allSynced = false;
            }
            // players = udonArrayPlus.Players();
        }
        public string SetHost_targetName = "";
        public bool SetHost()
        {
            string hostName = SetHost_targetName;
            SetHost_targetName = "";
            if (Networking.LocalPlayer != null && Networking.LocalPlayer.displayName == hostName)
            {
                if (Networking.GetOwner(gameObject) != Networking.LocalPlayer)
                {
                    Networking.SetOwner(Networking.LocalPlayer, gameObject);
                }
                host = hostName;
                allSynced = false;
                // time = DateTime.UtcNow.ToUniversalTime().Ticks;
                time = Networking.GetNetworkDateTime().ToUniversalTime().Ticks;
                RequestSerialization();
                return true;
            }
            return false;
        }
        public string SetTarget_targetName = "";
        public bool SetTarget()
        {
            string targetName = SetTarget_targetName;
            SetTarget_targetName = "";
            if (Networking.LocalPlayer.displayName == host && Networking.LocalPlayer.displayName == targetName)
            {
                return false;
            }
            // if (Networking.LocalPlayer != null && Networking.LocalPlayer.isMaster)
            // {
            //     host = Networking.LocalPlayer.displayName;
            // }
            if (Networking.LocalPlayer != null && Networking.LocalPlayer.displayName == host && !string.IsNullOrEmpty(targetName))
            {
                target = targetName;
                allSynced = false;
                // time = DateTime.UtcNow.ToUniversalTime().Ticks;
                time = Networking.GetNetworkDateTime().ToUniversalTime().Ticks;
                RequestSerialization();
                return true;
            }
            return false;
        }
        public void SyncEd()
        {
            // if (Networking.LocalPlayer != null && Networking.LocalPlayer.isMaster)
            // {
            //     host = Networking.LocalPlayer.displayName;
            // }
            if (Networking.LocalPlayer.displayName == host)
            {
                // offset = ((DateTime.UtcNow.ToUniversalTime().Ticks - time) / 10000000f) / 2f;
                offset = ((Networking.GetNetworkDateTime().ToUniversalTime().Ticks - time) / 10000000f) / 2f;
                allSynced = true;
                if (pingText != null) pingText.text = $"Ping: {offset.ToString()}";
            }
        }
        // 发送 Ping
        public void SendPing()
        {
            // if (Networking.LocalPlayer != null && Networking.LocalPlayer.isMaster)
            // {
            //     host = Networking.LocalPlayer.displayName;
            // }
            if (Networking.LocalPlayer != null && Networking.LocalPlayer.displayName == host && !string.IsNullOrEmpty(target))
            {
                // time = DateTime.UtcNow.ToUniversalTime().Ticks;
                time = Networking.GetNetworkDateTime().ToUniversalTime().Ticks;
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "ReceivePing");
            }
        }
        public void ReceivePing()
        {
            // if (Networking.LocalPlayer != null && Networking.LocalPlayer.isMaster)
            // {
            //     host = Networking.LocalPlayer.displayName;
            // }
            if (Networking.LocalPlayer.displayName == target)
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "ReturnPing");
            }
        }
        public void ReturnPing()
        {
            // if (Networking.LocalPlayer != null && Networking.LocalPlayer.isMaster)
            // {
            //     host = Networking.LocalPlayer.displayName;
            // }
            if (Networking.LocalPlayer.displayName == host && allSynced)
            {
                // offset = ((DateTime.UtcNow.ToUniversalTime().Ticks - time) / 10000000f) / 2f;
                offset = ((Networking.GetNetworkDateTime().ToUniversalTime().Ticks - time) / 10000000f) / 2f;
                if (pingText != null) pingText.text = $"Ping: {offset.ToString()}";
            }
        }
        // 收到同步请求
        public override void OnDeserialization()
        {
            // if (Networking.LocalPlayer != null && Networking.LocalPlayer.isMaster)
            // {
            //     host = Networking.LocalPlayer.displayName;
            // }
            host = host_;
            target = target_;
            // if (Networking.LocalPlayer.displayName == host && Networking.GetOwner(gameObject) != Networking.LocalPlayer)
            // {
            //     Networking.SetOwner(Networking.LocalPlayer, gameObject);
            // }
            if (!string.IsNullOrEmpty(target) && Networking.LocalPlayer.displayName == target)
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SyncEd");
            }
            if (Networking.LocalPlayer.displayName != host)
            {
                // var _offset = (DateTime.UtcNow.ToUniversalTime().Ticks - time) / 10000000f;
                var _offset = (Networking.GetNetworkDateTime().ToUniversalTime().Ticks - time) / 10000000f;
                if (pingText != null) pingText.text = $"Ping: {_offset.ToString()}";
                offset = _offset;
            }
        }
    }
}