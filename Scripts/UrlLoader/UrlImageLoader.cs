﻿
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Image;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UrlImageLoader : UdonSharpBehaviour
    {
        public VRCUrl url;
        public Texture2D content;
        public bool loadOnStart = true;
        [NonSerialized] public bool isLoaded = false;
        public UdonBehaviour udonSendFunction;
        public string sendCustomEvent = "SendFunction";
        public string setVariableName = "value";
        VRCImageDownloader _imageDownloader;
        void Start()
        {
            _imageDownloader = new VRCImageDownloader();
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
            _imageDownloader.DownloadImage(url, null, GetComponent<UdonBehaviour>(), null);
        }
        public override void OnImageLoadSuccess(IVRCImageDownload result)
        {
            isLoaded = true;
            content = result.Result;
            if (!string.IsNullOrWhiteSpace(setVariableName))
                udonSendFunction.SetProgramVariable(setVariableName, content);
            if (!string.IsNullOrWhiteSpace(sendCustomEvent))
                udonSendFunction.SendCustomEvent(sendCustomEvent);
        }
        public override void OnImageLoadError(IVRCImageDownload result)
        {
            isLoaded = true;
            Debug.LogError($"UdonLab.Toolkit.UrlImageLoader: {result.Error} Could not load {result.Url} : {result.ErrorMessage} ");
        }
        public void SendFunction() => LoadUrl();
        public void SendFunctions() => LoadUrl();
    }
}
