
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonSendFunctionsWithInt : UdonSendFunctionsValue
    {
        /// <summary>
        /// 需要调整参数的值
        /// </summary>
        [Header("需要调整参数的值")]
        public int[] values;
        /// <summary>
        /// 需要调整参数的值
        /// </summary>
        public int value
        {
            get
            {
                if (values == null || values.Length == 0)
                    return 0;
                return values[0];
            }
            set
            {
                if (values == null || values.Length == 0)
                    values = new int[1];
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = value;
                }
            }
        }
        // /// <summary>
        // /// 只允许本地玩家触发
        // /// </summary>
        // [Header("只允许本地玩家触发")]
        // [SerializeField] private bool isLocalOnly = true;
        public override void SendFunctions()
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
                if (i < valueNames.Length && !string.IsNullOrEmpty(valueNames[i]))
                {
                    udonBehaviours[i].SetProgramVariable(valueNames[i], values[i]);
                }
                udonBehaviours[i].SendCustomEvent(functionNames[i]);
            }
            _isSended = true;
        }
    }
}
