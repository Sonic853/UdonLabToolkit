
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class VoiceChatSwitchPickup : UdonSharpBehaviour
    {
        public VoiceController voiceController;
        public override void OnPickup()
        {
            voiceController.VoiceSwitchON();
        }
        public override void OnDrop()
        {
            voiceController.VoiceSwitchOff();
        }
    }
}
