
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;


namespace UdonLab.Toolkit
{
    public class UdonSendFunctionValue : UdonSendFunction
    {
        /// <summary>
        /// 需要调整参数的变量名
        /// </summary>
        [Header("需要调整参数的变量名")]
        public string valueName;
        /// <summary>
        /// 需要调整参数的变量名
        /// </summary>
        public string[] valueNames
        {
            get
            {
                return new string[] { valueName };
            }
            set
            {
                Debug.LogWarning("UdonSendFunction 只能保存一个 valueName");
                if (value == null || value.Length == 0)
                    valueName = null;
                else
                    valueName = value[0];
            }
        }
    }
}
