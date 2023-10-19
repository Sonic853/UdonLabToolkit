
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonSendFunction : UdonSharpBehaviour
    {
        /// <summary>
        /// 需要调用的UdonBehaviour
        /// </summary>
        [Header("需要调用的UdonBehaviour")]
        public UdonBehaviour udonBehaviour;
        /// <summary>
        /// 需要调用的UdonBehaviour
        /// </summary>
        public UdonBehaviour[] udonBehaviours
        {
            get
            {
                if (udonBehaviour == null)
                    return null;
                return new UdonBehaviour[] { udonBehaviour };
            }
            set
            {
                Debug.LogWarning("UdonSendFunction 只能保存一个 UdonBehaviour");
                if (value == null || value.Length == 0)
                    udonBehaviour = null;
                else
                    udonBehaviour = value[0];
            }
        }
        /// <summary>
        /// 触发后将调用以下的函数
        /// </summary>
        [Header("触发后将调用以下的函数")]
        public string functionName;
        /// <summary>
        /// 触发后将调用以下的函数
        /// </summary>
        public string[] functionNames
        {
            get
            {
                return new string[] { functionName };
            }
            set
            {
                Debug.LogWarning("UdonSendFunction 只能保存一个 functionName");
                if (value == null || value.Length == 0)
                    functionName = null;
                else
                    functionName = value[0];
            }
        }
        /// <summary>
        /// 只允许触发一次
        /// </summary>
        [Header("只允许触发一次")]
        public bool isOnce = false;
        /// <summary>
        /// 是否已触发
        /// </summary>
        [NonSerialized] public bool _isSended = false;
        public virtual void SendFunction()
        {
            if (isOnce && _isSended)
                return;
            if (udonBehaviour == null)
                return;
            if (string.IsNullOrEmpty(functionName))
                return;
            udonBehaviour.SendCustomEvent(functionName);
            _isSended = true;
        }
        public virtual void SendFunctions()
        {
            SendFunction();
        }
    }
}
