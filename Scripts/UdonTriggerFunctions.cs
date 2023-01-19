
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
        /// 需要调用的UdonBehaviour
        /// </summary>
        [Header("需要调用的UdonBehaviour")]
        [SerializeField] private UdonBehaviour[] udonBehaviours;
        /// <summary>
        /// 进入后将调用以下的函数
        /// </summary>
        [Header("进入后将调用以下的函数")]
        [SerializeField] private string[] functionNames;
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
        [NonSerialized] private bool _isEntered = false;
        /// <summary>
        /// 0：进入 1：退出 2：都触发
        /// </summary>
        [Header("0：进入 1：退出 2：都触发")]
        [Range(0, 2)]
        [SerializeField] private int triggerType = 0;
        [NonSerialized] public VRCPlayerApi OnPlayerTrigger_VRCPlayerApi_ = null;
        public void OnPlayerTrigger_()
        {
            if (isOnce && _isEntered
            || isLocalOnly && !OnPlayerTrigger_VRCPlayerApi_.isLocal)
                return;
            foreach (var udonBehaviour in udonBehaviours)
            {
                if (udonBehaviour == null)
                    continue;
                foreach (var functionName in functionNames)
                {
                    if (string.IsNullOrEmpty(functionName))
                        continue;
                    udonBehaviour.SendCustomEvent(functionName);
                }
            }
            _isEntered = true;
        }
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (triggerType == 1)
                return;
            OnPlayerTrigger_VRCPlayerApi_ = player;
            OnPlayerTrigger_();
            OnPlayerTrigger_VRCPlayerApi_ = null;
        }
        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            if (triggerType == 0)
                return;
            OnPlayerTrigger_VRCPlayerApi_ = player;
            OnPlayerTrigger_();
            OnPlayerTrigger_VRCPlayerApi_ = null;
        }
    }
}
