
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonOnceEnter : UdonSharpBehaviour
    {
        [Header("UdonSharpBehaviour")]
        [SerializeField] private UdonSharpBehaviour behaviour;
        [SerializeField] private bool isLocalOnly = true;
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (isLocalOnly && !player.isLocal)
                return;
            behaviour.SetProgramVariable("_OnPlayerTriggerEnter_VRCPlayerApi", player);
            behaviour.SendCustomEvent("_OnPlayerTriggerEnter");
            gameObject.SetActive(false);
        }
    }
}