
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonJoinFunctions : UdonSharpBehaviour
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
        /// <summary>
        /// 进入后将传入 VRCPlayerApi 到以下的值名称，留空则不传入
        /// </summary>
        [Header("进入后将传入 VRCPlayerApi 到以下的值名称，留空则不传入")]
        public string[] joinVRCPlayerApiValueName;
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
        /// 只允许触发一次
        /// </summary>
        [Header("只允许触发一次")]
        public bool isOnce = false;
        /// <summary>
        /// 是否已触发
        /// </summary>
        [NonSerialized] public bool isInteracted = false;
        [NonSerialized] public VRCPlayerApi OnPlayerJoined_VRCPlayerApi = null;
        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            OnPlayerJoined_VRCPlayerApi = player;
            OnPlayerJoined_();
            OnPlayerJoined_VRCPlayerApi = null;
        }
        public void OnPlayerJoined_()
        {
            if (isOnce && isInteracted
            || OnPlayerJoined_VRCPlayerApi == null
            || isLocalOnly && !OnPlayerJoined_VRCPlayerApi.isLocal)
                return;
            for (int i = 0; i < udonBehaviours.Length; i++)
            {
                if (udonBehaviours[i] == null)
                    continue;
                if (i < joinVRCPlayerApiValueName.Length)
                {
                    if (!string.IsNullOrEmpty(joinVRCPlayerApiValueName[i]))
                    {
                        if (isOnlyPlayerName)
                            udonBehaviours[i].SetProgramVariable(joinVRCPlayerApiValueName[i], OnPlayerJoined_VRCPlayerApi.displayName);
                        else
                            udonBehaviours[i].SetProgramVariable(joinVRCPlayerApiValueName[i], OnPlayerJoined_VRCPlayerApi);
                    }
                }
                if (i < functionNames.Length)
                {
                    if (!string.IsNullOrEmpty(functionNames[i]))
                        udonBehaviours[i].SendCustomEvent(functionNames[i]);
                }
            }
            isInteracted = true;
        }
    }
}
