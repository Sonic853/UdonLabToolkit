
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
        public UdonBehaviour[] udonBehavioursEnter;
        /// <summary>
        /// 进入后将调用以下的函数，留空则不传入
        /// </summary>
        [Header("进入后将调用以下的函数，留空则不传入")]
        public string[] enterFunctionNames;
        /// <summary>
        /// 进入后将传入 VRCPlayerApi 到以下的值名称，留空则不传入
        /// </summary>
        [Header("进入后将传入 VRCPlayerApi 到以下的值名称，留空则不传入")]
        public string[] enterVRCPlayerApiValueName;
        /// <summary>
        /// 退出后需要调用的UdonBehaviour
        /// </summary>
        [Header("退出后需要调用的UdonBehaviour")]
        public UdonBehaviour[] udonBehavioursExit;
        /// <summary>
        /// 退出后将调用以下的函数，留空则不传入
        /// </summary>
        [Header("退出后将调用以下的函数，留空则不传入")]
        public string[] exitFunctionNames;
        /// <summary>
        /// 退出后将传入 VRCPlayerApi 到以下的值名称，留空则不传入
        /// </summary>
        [Header("退出后将传入 VRCPlayerApi 到以下的值名称，留空则不传入")]
        public string[] exitVRCPlayerApiValueName;
        /// <summary>
        /// 只传入玩家名称
        /// </summary>
        [Header("只传入玩家名称")]
        public bool isOnlyPlayerName = false;
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
        public int isOnce = 3;
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
        [NonSerialized] private VRCPlayerApi OnPlayerTriggerEnter_VRCPlayerApi = null;
        [NonSerialized] private VRCPlayerApi OnPlayerTriggerExit_VRCPlayerApi = null;
        public void OnPlayerTriggerEnter_()
        {
            // if (isOnce && isEntered
            if (isOnce == 1 && isEntered
            || isOnce == 3 && isEntered
            || OnPlayerTriggerEnter_VRCPlayerApi == null
            || isLocalOnly && !OnPlayerTriggerEnter_VRCPlayerApi.isLocal)
                return;
            for (int i = 0; i < udonBehavioursEnter.Length; i++)
            {
                if (udonBehavioursEnter[i] == null)
                    continue;
                if (i < enterVRCPlayerApiValueName.Length
                && !string.IsNullOrEmpty(enterVRCPlayerApiValueName[i]))
                {
                    if (isOnlyPlayerName)
                        udonBehavioursEnter[i].SetProgramVariable(enterVRCPlayerApiValueName[i], OnPlayerTriggerEnter_VRCPlayerApi.displayName);
                    else
                        udonBehavioursEnter[i].SetProgramVariable(enterVRCPlayerApiValueName[i], OnPlayerTriggerEnter_VRCPlayerApi);
                }
                if (i < enterFunctionNames.Length)
                {
                    if (!string.IsNullOrEmpty(enterFunctionNames[i]))
                        udonBehavioursEnter[i].SendCustomEvent(enterFunctionNames[i]);
                }
            }
            isEntered = true;
        }
        public void OnPlayerTriggerExit_()
        {
            // if (isOnce && isExited
            if (isOnce == 2 && isExited
            || isOnce == 3 && isExited
            || OnPlayerTriggerExit_VRCPlayerApi == null
            || isLocalOnly && !OnPlayerTriggerExit_VRCPlayerApi.isLocal)
                return;
            for (int i = 0; i < udonBehavioursEnter.Length; i++)
            {
                if (udonBehavioursExit[i] == null)
                    continue;
                if (i < exitVRCPlayerApiValueName.Length
                && !string.IsNullOrEmpty(exitVRCPlayerApiValueName[i]))
                {
                    if (isOnlyPlayerName)
                        udonBehavioursExit[i].SetProgramVariable(exitVRCPlayerApiValueName[i], OnPlayerTriggerExit_VRCPlayerApi.displayName);
                    else
                        udonBehavioursExit[i].SetProgramVariable(exitVRCPlayerApiValueName[i], OnPlayerTriggerExit_VRCPlayerApi);
                }
                if (i < exitFunctionNames.Length)
                {
                    if (!string.IsNullOrEmpty(exitFunctionNames[i]))
                        udonBehavioursExit[i].SendCustomEvent(exitFunctionNames[i]);
                }
            }
            isExited = true;
        }
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (triggerType == 2)
                return;
            OnPlayerTriggerEnter_VRCPlayerApi = player;
            OnPlayerTriggerEnter_();
            OnPlayerTriggerEnter_VRCPlayerApi = null;
        }
        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            if (triggerType == 1)
                return;
            OnPlayerTriggerExit_VRCPlayerApi = player;
            OnPlayerTriggerExit_();
            OnPlayerTriggerExit_VRCPlayerApi = null;
        }
    }
}
