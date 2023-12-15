
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.StringLoading;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UrlStringLoader : UdonSharpBehaviour
    {
        public VRCUrl url;
        public string content;
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
            content = result.Result;
            if (!string.IsNullOrWhiteSpace(setVariableName))
                udonSendFunction.SetProgramVariable(setVariableName, content);
            if (!string.IsNullOrWhiteSpace(sendCustomEvent))
                udonSendFunction.SendCustomEvent(sendCustomEvent);
        }
        public override void OnStringLoadError(IVRCStringDownload result)
        {
            isLoaded = true;
            Debug.LogError($"UdonLab.Toolkit.UrlStringLoader: {result.ErrorCode} Could not load {result.Url} with error: {result.Error}");
        }
        public void SendFunction() => LoadUrl();
        public void SendFunctions() => LoadUrl();
    }
}
