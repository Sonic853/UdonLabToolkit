
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
        [NonSerialized] public VRCPlayerApi _OnPlayerTriggerEnter_VRCPlayerApi = null;
        public void _OnPlayerTriggerEnter()
        {
            var player = _OnPlayerTriggerEnter_VRCPlayerApi;
            if (isLocalOnly && !player.isLocal)
                return;
            for (int i = 0; i < behaviours.Length; i++)
            {
                if (behaviours[i] == null) continue;
                behaviours[i].SetProgramVariable("_OnPlayerTriggerEnter_VRCPlayerApi", player);
                behaviours[i].SendCustomEvent("_OnPlayerTriggerEnter");
            }
            gameObject.SetActive(false);
        }
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            _OnPlayerTriggerEnter_VRCPlayerApi = player;
            _OnPlayerTriggerEnter();
            _OnPlayerTriggerEnter_VRCPlayerApi = null;
        }
    }
}