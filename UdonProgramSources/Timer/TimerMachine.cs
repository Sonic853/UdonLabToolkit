using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class TimerMachine : UdonSharpBehaviour
    {
        [SerializeField] private TimeMachine _timeMachine;
        private DateTime startTime = DateTime.Now;
        private DateTime endTime = DateTime.Now;
        private TimeSpan timeSpan;
        public bool showTimer = false;
        public bool enableTimer = false;
        public string timerText = "";
        [SerializeField] private Text _timerText;
        public void Update()
        {
            if (showTimer && enableTimer)
            {
                DateTime timeNow;
                if (_timeMachine == null)
                {
                    timeNow = DateTime.Now;
                }
                else
                {
                    timeNow = _timeMachine.Now;
                }
                timeSpan = timeNow - startTime;
                // timerText = timeSpan.ToString("hh:mm:ss.fff");
                timerText = timeSpan.Hours.ToString("00") + ":" + timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00") + "." + timeSpan.Milliseconds.ToString("000");
                if (_timerText != null)
                {
                    _timerText.text = timerText;
                }
            }
        }
        public void ResetTimer()
        {
            enableTimer = false;
            if (_timeMachine == null)
            {
                startTime = DateTime.Now;
                endTime = DateTime.Now;
            }
            else
            {
                startTime = _timeMachine.Now;
                endTime = _timeMachine.Now;
            }
            timeSpan = TimeSpan.Zero;
            timerText = timeSpan.Hours.ToString("00") + ":" + timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00") + "." + timeSpan.Milliseconds.ToString("000");
            if (_timerText != null)
            {
                _timerText.text = timerText;
            }
        }
        public void StartTimer()
        {
            ResetTimer();
            enableTimer = true;
        }
        public void StopTimer()
        {
            if (!enableTimer) return;
            enableTimer = false;
            if (_timeMachine == null)
            {
                endTime = DateTime.Now;
            }
            else
            {
                endTime = _timeMachine.Now;
            }
            timeSpan = endTime - startTime;
            timerText = timeSpan.Hours.ToString("00") + ":" + timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00") + "." + timeSpan.Milliseconds.ToString("000");
            if (_timerText != null)
            {
                _timerText.text = timerText;
            }
        }
    }
}