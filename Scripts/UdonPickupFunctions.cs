
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonPickupFunctions : UdonSharpBehaviour
    {
        // OnPickup
        /// <summary>
        /// 当玩家拾取时调用的UdonBehaviour
        /// </summary>
        [Header("当玩家拾取时调用的UdonBehaviour")]
        [SerializeField] private UdonBehaviour[] udonBehavioursOnPickup;
        /// <summary>
        /// 拾取时将调用以下的函数
        /// </summary>
        [Header("拾取时将调用以下的函数")]
        [SerializeField] private string[] onPickupFunctionNames;
        /// <summary>
        /// 拾取只允许触发一次
        /// </summary>
        [Header("拾取只允许触发一次")]
        [SerializeField] private bool onPickupIsOnce = false;
        /// <summary>
        /// 拾取是否已触发
        /// </summary>
        private bool onPickupIsTriggered = false;
        // OnDrop
        /// <summary>
        /// 当玩家丢弃时调用的UdonBehaviour
        /// </summary>
        [Header("当玩家丢弃时调用的UdonBehaviour")]
        [SerializeField] private UdonBehaviour[] udonBehavioursOnDrop;
        /// <summary>
        /// 丢弃时将调用以下的函数
        /// </summary>
        [Header("丢弃时将调用以下的函数")]
        [SerializeField] private string[] onDropFunctionNames;
        /// <summary>
        /// 丢弃只允许触发一次
        /// </summary>
        [Header("丢弃只允许触发一次")]
        [SerializeField] private bool onDropIsOnce = false;
        /// <summary>
        /// 丢弃是否已触发
        /// </summary>
        private bool onDropIsTriggered = false;
        // OnPickupUseDown
        /// <summary>
        /// 当玩家按下使用时调用的UdonBehaviour
        /// </summary>
        [Header("当玩家按下使用时调用的UdonBehaviour")]
        [SerializeField] private UdonBehaviour[] udonBehavioursOnPickupUseDown;
        /// <summary>
        /// 按下使用时将调用以下的函数
        /// </summary>
        [Header("按下使用时将调用以下的函数")]
        [SerializeField] private string[] onPickupUseDownFunctionNames;
        /// <summary>
        /// 按下使用只允许触发一次
        /// </summary>
        [Header("按下使用只允许触发一次")]
        [SerializeField] private bool onPickupUseDownIsOnce = false;
        /// <summary>
        /// 按下使用是否已触发
        /// </summary>
        private bool onPickupUseDownIsTriggered = false;
        // OnPickupUseUp
        /// <summary>
        /// 当玩家松开使用时调用的UdonBehaviour
        /// </summary>
        [Header("当玩家松开使用时调用的UdonBehaviour")]
        [SerializeField] private UdonBehaviour[] udonBehavioursOnPickupUseUp;
        /// <summary>
        /// 松开使用时将调用以下的函数
        /// </summary>
        [Header("松开使用时将调用以下的函数")]
        [SerializeField] private string[] onPickupUseUpFunctionNames;
        /// <summary>
        /// 松开使用只允许触发一次
        /// </summary>
        [Header("松开使用只允许触发一次")]
        [SerializeField] private bool onPickupUseUpIsOnce = false;
        /// <summary>
        /// 松开使用是否已触发
        /// </summary>
        private bool onPickupUseUpIsTriggered = false;
        /// <summary>
        /// 只允许本地玩家触发
        /// </summary>
        [Header("只允许本地玩家触发")]
        [SerializeField] private bool isLocalOnly = true;
        public void OnPickup_()
        {
            if (onPickupIsOnce && onPickupIsTriggered)
                return;
            for (int i = 0; i < udonBehavioursOnPickup.Length; i++)
            {
                if (udonBehavioursOnPickup[i] == null)
                    continue;
                if (i >= onPickupFunctionNames.Length)
                    break;
                if (string.IsNullOrEmpty(onPickupFunctionNames[i]))
                    continue;
                udonBehavioursOnPickup[i].SendCustomEvent(onPickupFunctionNames[i]);
            }
            onPickupIsTriggered = true;
        }
        public void OnDrop_()
        {
            if (onDropIsOnce && onDropIsTriggered)
                return;
            for (int i = 0; i < udonBehavioursOnDrop.Length; i++)
            {
                if (udonBehavioursOnDrop[i] == null)
                    continue;
                if (i >= onDropFunctionNames.Length)
                    break;
                if (string.IsNullOrEmpty(onDropFunctionNames[i]))
                    continue;
                udonBehavioursOnDrop[i].SendCustomEvent(onDropFunctionNames[i]);
            }
            onDropIsTriggered = true;
        }
        public void OnPickupUseDown_()
        {
            if (onPickupUseDownIsOnce && onPickupUseDownIsTriggered)
                return;
            for (int i = 0; i < udonBehavioursOnPickupUseDown.Length; i++)
            {
                if (udonBehavioursOnPickupUseDown[i] == null)
                    continue;
                if (i >= onPickupUseDownFunctionNames.Length)
                    break;
                if (string.IsNullOrEmpty(onPickupUseDownFunctionNames[i]))
                    continue;
                udonBehavioursOnPickupUseDown[i].SendCustomEvent(onPickupUseDownFunctionNames[i]);
            }
            onPickupUseDownIsTriggered = true;
        }
        public void OnPickupUseUp_()
        {
            if (onPickupUseUpIsOnce && onPickupUseUpIsTriggered)
                return;
            for (int i = 0; i < udonBehavioursOnPickupUseUp.Length; i++)
            {
                if (udonBehavioursOnPickupUseUp[i] == null)
                    continue;
                if (i >= onPickupUseUpFunctionNames.Length)
                    break;
                if (string.IsNullOrEmpty(onPickupUseUpFunctionNames[i]))
                    continue;
                udonBehavioursOnPickupUseUp[i].SendCustomEvent(onPickupUseUpFunctionNames[i]);
            }
            onPickupUseUpIsTriggered = true;
        }
        public override void OnPickup()
        {
            if (isLocalOnly)
            {
                OnPickup_();
            }
            else
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "OnPickup_");
            }
        }
        public override void OnDrop()
        {
            if (isLocalOnly)
            {
                OnDrop_();
            }
            else
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "OnDrop_");
            }
        }
        public override void OnPickupUseDown()
        {
            if (isLocalOnly)
            {
                OnPickupUseDown_();
            }
            else
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "OnPickupUseDown_");
            }
        }
        public override void OnPickupUseUp()
        {
            if (isLocalOnly)
            {
                OnPickupUseUp_();
            }
            else
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "OnPickupUseUp_");
            }
        }
    }
}
