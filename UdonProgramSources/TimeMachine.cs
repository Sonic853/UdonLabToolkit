using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class TimeMachine : UdonSharpBehaviour
    {
        #region
        public DateTime Now;
        [HideInInspector] public int Year;
        /// <summary>
        /// 0 - 11
        /// </summary>
        [HideInInspector] public int month;
        /// <summary>
        /// 1 - 12
        /// </summary>
        [HideInInspector] public int Month;
        [HideInInspector] public int Day;
        [HideInInspector] public int Hour;
        [HideInInspector] public int Minute;
        [HideInInspector] public int Second;
        [HideInInspector] public int Millisecond;
        public bool enableTime = true;
        #endregion
        void Start()
        {
            UpdateTime();
        }
        void Update()
        {
            UpdateTime();
        }
        void UpdateTime()
        {
            if (enableTime)
            {
                Now = System.DateTime.Now;
                Year = Now.Year;
                month = Now.Month - 1;
                Month = Now.Month;
                Day = Now.Day;
                Hour = Now.Hour;
                Minute = Now.Minute;
                Second = Now.Second;
                Millisecond = Now.Millisecond;
            }
        }
    }
}
