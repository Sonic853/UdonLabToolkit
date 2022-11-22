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
        [NonSerialized] public DateTime Now;
        [NonSerialized] public int Year;
        /// <summary>
        /// 0 - 11
        /// </summary>
        [NonSerialized] public int month;
        /// <summary>
        /// 1 - 12
        /// </summary>
        [NonSerialized] public int Month;
        [NonSerialized] public int Day;
        [NonSerialized] public int Hour;
        [NonSerialized] public int Minute;
        [NonSerialized] public int Second;
        [NonSerialized] public int Millisecond;
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
