using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonInteractTimer : UdonSharpBehaviour
    {
        [SerializeField] private TimerMachine _timerMachine;
        [SerializeField] private bool isStart;
        public override void Interact()
        {
            if(isStart)
                _timerMachine.StartTimer();
            else
                _timerMachine.StopTimer();
        }
    }
}
