
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class VRCUrlChanger : UdonSharpBehaviour
    {
        /// <summary>
        /// 默认 Url
        /// </summary>
        [Header("默认 Url")]
        public VRCUrl defaultUrl;
        /// <summary>
        /// 玩家名称
        /// </summary>
        [Header("玩家名称")]
        public string[] names;
        /// <summary>
        /// Url 列表
        /// </summary>
        [Header("Url 列表")]
        public VRCUrl[] urls;
        /// <summary>
        /// Url 加载器
        /// </summary>
        [Header("Url 加载器")]
        public UrlsStringLoader urlsStringLoader;
        /// <summary>
        /// 需要调用的UdonBehaviour
        /// </summary>
        [Header("需要调用的UdonBehaviour")]
        public UdonBehaviour udonSendFunction;
        /// <summary>
        /// 触发后将调用以下的函数
        /// </summary>
        [Header("触发后将调用以下的函数")]
        public string sendCustomEvent = "LoadString";
        /// <summary>
        /// 需要调整参数的变量名
        /// </summary>
        [Header("需要调整参数的变量名")]
        public string setVariableName = "stringContext";
        VRCPlayerApi player = null;
        void Start()
        {
            LoadUrl();
        }
        public void LoadUrl()
        {
            if (player != null) PushUrl();
            if (names.Length == 0 || urls.Length == 0 || names.Length < urls.Length)
            {
                Debug.LogError("names or urls is 0 or names.Length != urls.Length");
                PushUrl();
                return;
            }
            player = Networking.LocalPlayer;
            if (player == null)
            {
                Debug.LogError("player is null");
                PushUrl();
                return;
            }
            var name = player.displayName;
            for (int i = 0; i < names.Length; i++)
            {
                if (name == names[i])
                {
                    defaultUrl = urls[i];
                    break;
                }
            }
            PushUrl();
        }
        public void PushUrl()
        {
            if (urlsStringLoader == null)
            {
                Debug.LogError("urlsStringLoader is null");
                return;
            }
            urlsStringLoader.PushUrl(defaultUrl, udonSendFunction, sendCustomEvent, setVariableName);
        }
        public void SendFunction() => LoadUrl();
        public void SendFunctions() => LoadUrl();
    }
}
