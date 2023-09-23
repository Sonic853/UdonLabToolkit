
using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class CheckModel : UdonSharpBehaviour
    {
        VRCPlayerApi localPlayer;
        [NonSerialized] public bool isTargetModel = false;
        public float targetDistance = 0.14708f;
        public Text debugtext;
        void Start()
        {
            if (Networking.LocalPlayer != null) localPlayer = Networking.LocalPlayer;
        }
        void Update()
        {
            if (localPlayer == null)
            {
                if (Networking.LocalPlayer != null) localPlayer = Networking.LocalPlayer;
                return;
            }
            // 计算距离
            var boneDistance = Vector3.Distance(localPlayer.GetBonePosition(HumanBodyBones.Chest), localPlayer.GetBonePosition(HumanBodyBones.Neck));
            // 只保留小数点后五位，不使用四舍五入
            boneDistance = Mathf.Floor(boneDistance * 100000) / 100000;
            if (debugtext != null) debugtext.text = boneDistance.ToString();
            // 如果距离不等于 targetDistance，则认为当前玩家使用的是自定义模型
            isTargetModel = boneDistance == targetDistance;
        }
    }
}