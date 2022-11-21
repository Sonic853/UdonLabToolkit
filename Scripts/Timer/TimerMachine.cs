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
        string originalTimerText = "";
        [SerializeField] private Text _timerText;
        void Start()
        {
            originalTimerText = timerText;
        }
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
                timerText = GetTimerText(timeSpan);
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
            timerText = GetTimerText(timeSpan);
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
            timerText = GetTimerText(timeSpan);
            if (_timerText != null)
            {
                _timerText.text = timerText;
            }
        }
        string GetTimerText(TimeSpan timeSpan)
        {
            if (originalTimerText == "")
                return timeSpan.Hours.ToString("00") + ":" + timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00") + "." + timeSpan.Milliseconds.ToString("000");
            // 转换里面的h、m、s、f格式
            return originalTimerText.Replace("hh", timeSpan.Hours.ToString("00"))
                .Replace("mm", timeSpan.Minutes.ToString("00"))
                .Replace("ss", timeSpan.Seconds.ToString("00"))
                .Replace("fff", timeSpan.Milliseconds.ToString("000"));
        }
    }
}