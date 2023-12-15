
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.StringLoading;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UrlBigImageLoader : UdonSharpBehaviour
    {
        public VRCUrl url;
        public Texture2D content;
        public int width = 4096;
        public int height = 4096;
        public bool loadOnStart = true;
        [NonSerialized] public bool isLoaded = false;
        public UdonBehaviour udonSendFunction;
        public string sendCustomEvent = "SendFunction";
        public string setVariableName = "value";
        void Start()
        {
            if (loadOnStart)
            {
                LoadUrl();
            }
        }
        public void LoadUrl()
        {
            if (string.IsNullOrEmpty(url.ToString()))
                return;
            isLoaded = false;
            VRCStringDownloader.LoadUrl(url, GetComponent<UdonBehaviour>());
        }
        public override void OnStringLoadSuccess(IVRCStringDownload result)
        {
            isLoaded = true;
            if (string.IsNullOrWhiteSpace(result.Result))
            {
                Debug.LogWarning($"UdonLab.Toolkit.UrlBigImageLoader: result is empty.");
                return;
            }
            content = new Texture2D(width, width);
            var bytes = Convert.FromBase64String(result.Result);
            content.LoadRawTextureData(bytes);
            if (!string.IsNullOrWhiteSpace(setVariableName))
                udonSendFunction.SetProgramVariable(setVariableName, content);
            if (!string.IsNullOrWhiteSpace(sendCustomEvent))
                udonSendFunction.SendCustomEvent(sendCustomEvent);
        }
        public override void OnStringLoadError(IVRCStringDownload result)
        {
            isLoaded = true;
            Debug.LogError($"UdonLab.Toolkit.UrlBigImageLoader: {result.ErrorCode} Could not load {result.Url} : {result.Error}");
        }
        public void SendFunction() => LoadUrl();
        public void SendFunctions() => LoadUrl();
    }
}
