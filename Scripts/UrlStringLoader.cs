﻿
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
        public bool useBigImg = false;
        public Texture2D bigImgContent;
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
            if (useBigImg)
            {
                bigImgContent = new Texture2D(2, 2);
                var bytes = Convert.FromBase64String(content);
                bigImgContent.LoadRawTextureData(bytes);
                if (!string.IsNullOrWhiteSpace(setVariableName))
                    udonSendFunction.SetProgramVariable(setVariableName, bigImgContent);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(setVariableName))
                    udonSendFunction.SetProgramVariable(setVariableName, content);
            }
            udonSendFunction.SendCustomEvent(sendCustomEvent);
        }
        public override void OnStringLoadError(IVRCStringDownload result)
        {
            isLoaded = true;
            Debug.LogError($"UdonLab.Toolkit.UrlStringLoader: Could not load {result.Url} : {result.Error} ");
        }
        public void SendFunction() => LoadUrl();
        public void SendFunctions() => LoadUrl();
    }
}
