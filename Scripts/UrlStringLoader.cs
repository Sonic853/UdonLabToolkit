
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
        public string sendCustomEvent = "SendFunctions";
        public string setVariableName = "content";
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
            udonSendFunction.SendCustomEvent(sendCustomEvent);
        }
        public override void OnStringLoadError(IVRCStringDownload result)
        {
            Debug.LogError($"UdonLab.Toolkit.UrlStringLoader: Could not load {result.Url} : {result.Error} ");
        }
    }
}
