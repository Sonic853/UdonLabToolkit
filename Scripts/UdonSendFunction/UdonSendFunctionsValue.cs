
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonSendFunctionsValue : UdonSendFunctions
    {
        /// <summary>
        /// 需要调整参数的变量名
        /// </summary>
        [Header("需要调整参数的变量名")]
        public string[] valueNames;
        /// <summary>
        /// 需要调整参数的变量名
        /// </summary>
        public string valueName
        {
            get
            {
                if (valueNames == null || valueNames.Length == 0)
                    return null;
                return valueNames[0];
            }
            set
            {
                if (valueNames == null || valueNames.Length == 0)
                    valueNames = new string[1];
                for (int i = 0; i < valueNames.Length; i++)
                {
                    valueNames[i] = value;
                }
            }
        }
    }
}
