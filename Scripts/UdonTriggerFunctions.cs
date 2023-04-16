
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonTriggerFunctions : UdonSharpBehaviour
    {
        /// <summary>
        /// 进入后需要调用的UdonBehaviour
        /// </summary>
        [Header("进入后需要调用的UdonBehaviour")]
        [SerializeField] private UdonBehaviour[] udonBehavioursEnter;
        /// <summary>
        /// 进入后将调用以下的函数
        /// </summary>
        [Header("进入后将调用以下的函数")]
        [SerializeField] private string[] enterFunctionNames;
        /// <summary>
        /// 退出后需要调用的UdonBehaviour
        /// </summary>
        [Header("退出后需要调用的UdonBehaviour")]
        [SerializeField] private UdonBehaviour[] udonBehavioursExit;
        /// <summary>
        /// 退出后将调用以下的函数
        /// </summary>
        [Header("退出后将调用以下的函数")]
        [SerializeField] private string[] exitFunctionNames;
        /// <summary>
        /// 只允许本地玩家触发
        /// </summary>
        [Header("只允许本地玩家触发")]
        [SerializeField] private bool isLocalOnly = true;
        /// <summary>
        /// 只允许触发一次：0：禁用 1：进入 2：退出 3：都只允许触发一次
        /// </summary>
        [Header("只允许触发一次：0：禁用 1：进入 2：退出 3：都只允许触发一次")]
        [Range(0, 3)]
        [SerializeField] private int isOnce = 3;
        /// <summary>
        /// 是否已触发
        /// </summary>
        [NonSerialized] private bool _isEntered = false;
        /// <summary>
        /// 是否已触发
        /// </summary>
        [NonSerialized] private bool _isExited = false;
        /// <summary>
        /// 触发器启用：0：都启用 1：进入 2：退出
        /// </summary>
        [Header("触发器启用：0：都启用 1：进入 2：退出")]
        [Range(0, 2)]
        [SerializeField] private int triggerType = 2;
        [NonSerialized] private VRCPlayerApi OnPlayerTriggerEnter_VRCPlayerApi_ = null;
        [NonSerialized] private VRCPlayerApi OnPlayerTriggerExit_VRCPlayerApi_ = null;
        public void OnPlayerTriggerEnter_()
        {
            // if (isOnce && _isEntered
            if (isOnce == 0 && _isEntered
            || isOnce == 2 && _isEntered
            || OnPlayerTriggerEnter_VRCPlayerApi_ == null
            || isLocalOnly && !OnPlayerTriggerEnter_VRCPlayerApi_.isLocal)
                return;
            foreach (var udonBehaviour in udonBehavioursEnter)
            {
                if (udonBehaviour == null)
                    continue;
                foreach (var functionName in enterFunctionNames)
                {
                    if (string.IsNullOrEmpty(functionName))
                        continue;
                    udonBehaviour.SendCustomEvent(functionName);
                }
            }
            _isEntered = true;
        }
        public void OnPlayerTriggerExit_()
        {
            // if (isOnce && _isExited
            if (isOnce == 1 && _isExited
            || isOnce == 2 && _isExited
            || OnPlayerTriggerExit_VRCPlayerApi_ == null
            || isLocalOnly && !OnPlayerTriggerExit_VRCPlayerApi_.isLocal)
                return;
            foreach (var udonBehaviour in udonBehavioursExit)
            {
                if (udonBehaviour == null)
                    continue;
                foreach (var functionName in exitFunctionNames)
                {
                    if (string.IsNullOrEmpty(functionName))
                        continue;
                    udonBehaviour.SendCustomEvent(functionName);
                }
            }
            _isExited = true;
        }
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (triggerType == 1)
                return;
            OnPlayerTriggerEnter_VRCPlayerApi_ = player;
            OnPlayerTriggerEnter_();
            OnPlayerTriggerEnter_VRCPlayerApi_ = null;
        }
        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            if (triggerType == 0)
                return;
            OnPlayerTriggerExit_VRCPlayerApi_ = player;
            OnPlayerTriggerExit_();
            OnPlayerTriggerExit_VRCPlayerApi_ = null;
        }
    }
}
