
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonSendFunctions : UdonSharpBehaviour
    {
        /// <summary>
        /// 需要调用的UdonBehaviour
        /// </summary>
        [Header("需要调用的UdonBehaviour")]
        public UdonBehaviour[] udonBehaviours;
        /// <summary>
        /// 触发后将调用以下的函数
        /// </summary>
        [Header("触发后将调用以下的函数")]
        public string[] functionNames;
        // /// <summary>
        // /// 只允许本地玩家触发
        // /// </summary>
        // [Header("只允许本地玩家触发")]
        // [SerializeField] private bool isLocalOnly = true;
        /// <summary>
        /// 只允许触发一次
        /// </summary>
        [Header("只允许触发一次")]
        public bool isOnce = false;
        /// <summary>
        /// 是否已触发
        /// </summary>
        [NonSerialized] public bool _isSended = false;
        public virtual void SendFunctions()
        {
            if (isOnce && _isSended)
                return;
            for (int i = 0; i < udonBehaviours.Length; i++)
            {
                if (udonBehaviours[i] == null)
                    continue;
                if (i >= functionNames.Length)
                    break;
                if (string.IsNullOrEmpty(functionNames[i]))
                    continue;
                udonBehaviours[i].SendCustomEvent(functionNames[i]);
            }
            _isSended = true;
        }
    }
}
