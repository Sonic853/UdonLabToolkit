
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonTriggerSender : UdonSharpBehaviour
    {
        /// <summary>
        /// 进入后需要调用的 Udon Send Functions
        /// </summary>
        [Header("进入后需要调用的 Udon Send")]
        public UdonBehaviour[] udonSenderEnter;
        /// <summary>
        /// 退出后需要调用的 Udon Send Functions
        /// </summary>
        [Header("退出后需要调用的 Udon Send")]
        public UdonBehaviour[] udonSenderExit;
        /// <summary>
        /// 只允许本地玩家触发
        /// </summary>
        [Header("只允许本地玩家触发")]
        public bool isLocalOnly = true;
        /// <summary>
        /// 只允许触发一次：0：禁用 1：进入 2：退出 3：都只允许触发一次
        /// </summary>
        [Header("只允许触发一次：0：禁用 1：进入 2：退出 3：都只允许触发一次")]
        [Range(0, 3)]
        public int isOnce = 0;
        /// <summary>
        /// 放入玩家名字：0：禁用 1：进入 2：退出 3：都放入玩家名字（UdonSendFunctionsWithString Only）
        /// </summary>
        [Header("放入玩家名字：0：禁用 1：进入 2：退出 3：都放入玩家名字")]
        [Tooltip("（UdonSendFunctionsWithString Only）")]
        [Range(0, 3)]
        public int sendPlayerName = 0;
        /// <summary>
        /// 是否已触发
        /// </summary>
        [NonSerialized] public bool isEntered = false;
        /// <summary>
        /// 是否已触发
        /// </summary>
        [NonSerialized] public bool isExited = false;
        /// <summary>
        /// 触发器启用：0：都启用 1：进入 2：退出
        /// </summary>
        [Header("触发器启用：0：都启用 1：进入 2：退出")]
        [Range(0, 2)]
        public int triggerType = 0;
        [NonSerialized] private VRCPlayerApi OnPlayerTriggerEnter_VRCPlayerApi_ = null;
        [NonSerialized] private VRCPlayerApi OnPlayerTriggerExit_VRCPlayerApi_ = null;
        public void OnPlayerTriggerEnter_()
        {
            if (isOnce == 1 && isEntered
            || isOnce == 3 && isEntered
            || OnPlayerTriggerEnter_VRCPlayerApi_ == null
            || isLocalOnly && !OnPlayerTriggerEnter_VRCPlayerApi_.isLocal)
                return;
            for (int i = 0; i < udonSenderEnter.Length; i++)
            {
                if (udonSenderEnter[i] == null)
                    continue;
                if (sendPlayerName == 1 || sendPlayerName == 3)
                {
                    var udonSenderValues = (string[])udonSenderEnter[i].GetProgramVariable("values");
                    if (udonSenderValues != null) for (int j = 0; j < udonSenderValues.Length; j++)
                        {
                            udonSenderValues[j] = OnPlayerTriggerEnter_VRCPlayerApi_.displayName;
                        }
                    udonSenderEnter[i].SetProgramVariable("values", udonSenderValues);
                }
                udonSenderEnter[i].SendCustomEvent("SendFunctions");
            }
            isEntered = true;
        }
        public void OnPlayerTriggerExit_()
        {
            if (isOnce == 2 && isExited
            || isOnce == 3 && isExited
            || OnPlayerTriggerExit_VRCPlayerApi_ == null
            || isLocalOnly && !OnPlayerTriggerExit_VRCPlayerApi_.isLocal)
                return;
            for (int i = 0; i < udonSenderExit.Length; i++)
            {
                if (udonSenderExit[i] == null)
                    continue;
                if (sendPlayerName == 2 || sendPlayerName == 3)
                {
                    var udonSenderValues = (string[])udonSenderExit[i].GetProgramVariable("values");
                    if (udonSenderValues != null) for (int j = 0; j < udonSenderValues.Length; j++)
                        {
                            udonSenderValues[j] = OnPlayerTriggerExit_VRCPlayerApi_.displayName;
                        }
                    udonSenderExit[i].SetProgramVariable("values", udonSenderValues);
                }
                udonSenderExit[i].SendCustomEvent("SendFunctions");
            }
            isExited = true;
        }
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (triggerType == 2)
                return;
            OnPlayerTriggerEnter_VRCPlayerApi_ = player;
            OnPlayerTriggerEnter_();
            OnPlayerTriggerEnter_VRCPlayerApi_ = null;
        }
        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            if (triggerType == 1)
                return;
            OnPlayerTriggerExit_VRCPlayerApi_ = player;
            OnPlayerTriggerExit_();
            OnPlayerTriggerExit_VRCPlayerApi_ = null;
        }
    }
}
