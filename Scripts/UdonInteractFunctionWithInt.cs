
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonInteractFunctionWithInt : UdonSharpBehaviour
    {
        /// <summary>
        /// 需要调用的UdonBehaviour
        /// </summary>
        [Header("需要调用的UdonBehaviour")]
        [SerializeField] public UdonBehaviour[] udonBehaviours;
        /// <summary>
        /// 进入后将调用以下的函数
        /// </summary>
        [Header("进入后将调用以下的函数")]
        [SerializeField] public string functionName;
        /// <summary>
        /// 需要调整参数的变量名
        /// </summary>
        [Header("需要调整参数的变量名")]
        [SerializeField] public string setIntValue;
        /// <summary>
        /// 只允许本地玩家触发
        /// </summary>
        [Header("只允许本地玩家触发")]
        [SerializeField] private bool isLocalOnly = true;
        /// <summary>
        /// 需要调整参数的值
        /// </summary>
        [Header("需要调整参数的值")]
        [SerializeField] public int value;
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
            foreach (var udonBehaviour in udonBehaviours)
            {
                if (udonBehaviour == null)
                    continue;
                if (string.IsNullOrEmpty(functionName))
                    continue;
                if (!string.IsNullOrEmpty(setIntValue))
                {
                    udonBehaviour.SetProgramVariable(setIntValue, value);
                }
                udonBehaviour.SendCustomEvent(functionName);
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
