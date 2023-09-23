
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class VoiceChatSwitchUseDown : UdonSharpBehaviour
    {
        public VoiceController voiceController;
        public override void OnPickupUseDown()
        {
            voiceController.VoiceSwitchON();
        }
        public override void OnPickupUseUp()
        {
            voiceController.VoiceSwitchOff();
        }
    }
}
