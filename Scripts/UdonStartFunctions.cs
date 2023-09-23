
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonStartFunctions : UdonSharpBehaviour
    {
        /// <summary>
        /// 需要调用的UdonBehaviour
        /// </summary>
        [Header("需要调用的UdonBehaviour")]
        [SerializeField] private UdonBehaviour[] udonBehaviours;
        /// <summary>
        /// 触发后将调用以下的函数
        /// </summary>
        [Header("触发后将调用以下的函数")]
        [SerializeField] private string[] functionNames;
        /// <summary>
        /// 只允许触发一次
        /// </summary>
        [Header("只允许触发一次")]
        [SerializeField] private bool isOnce = false;
        /// <summary>
        /// 是否已触发
        /// </summary>
        [NonSerialized] private bool _isInteracted = false;
        void Start()
        {
            if (isOnce && _isInteracted)
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
        }
    }
}
