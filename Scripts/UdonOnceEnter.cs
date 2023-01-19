
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonOnceEnter : UdonSharpBehaviour
    {
        [Header("UdonSharpBehaviour")]
        [SerializeField] private UdonBehaviour[] behaviours;
        [SerializeField] private bool isLocalOnly = true;
        [NonSerialized] public VRCPlayerApi OnPlayerTriggerEnter_VRCPlayerApi_ = null;
        public void OnPlayerTriggerEnter_()
        {
            var player = OnPlayerTriggerEnter_VRCPlayerApi_;
            if (isLocalOnly && !player.isLocal)
                return;
            for (int i = 0; i < behaviours.Length; i++)
            {
                if (behaviours[i] == null) continue;
                behaviours[i].SetProgramVariable("OnPlayerTriggerEnter_VRCPlayerApi_", player);
                behaviours[i].SendCustomEvent("OnPlayerTriggerEnter_");
            }
            gameObject.SetActive(false);
        }
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            OnPlayerTriggerEnter_VRCPlayerApi_ = player;
            OnPlayerTriggerEnter_();
            OnPlayerTriggerEnter_VRCPlayerApi_ = null;
        }
    }
}