using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonTriggerTimer : UdonSharpBehaviour
    {
        [SerializeField] private TimerMachine _timerMachine;
        [SerializeField] private bool isStart;
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (player.isLocal)
            {
                if (isStart)
                    _timerMachine.ResetTimer();
                else
                    _timerMachine.StopTimer();
            }
        }
        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            if (player.isLocal && isStart)
            {
                _timerMachine.StartTimer();
            }
        }
    }
}
