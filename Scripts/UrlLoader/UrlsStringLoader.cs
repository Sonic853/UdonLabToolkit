
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.StringLoading;
using VRC.SDKBase;
using VRC.Udon;


namespace UdonLab.Toolkit
{
    public class UrlsStringLoader : UdonSharpBehaviour
    {
        public VRCUrl[] urls;
        public string[] contents;
        public bool isLoading = false;
        [NonSerialized] public bool[] isLoaded = new bool[0];
        public UdonBehaviour[] udonSendFunctions;
        public string[] sendCustomEvents;
        public string[] setVariableNames;
        void Start()
        {
            if (urls.Length > 0)
                LoadUrl();
        }
        public void LoadUrl()
        {
            if (isLoading) return;
            isLoading = true;
            VRCStringDownloader.LoadUrl(urls[0], GetComponent<UdonBehaviour>());
        }
        public void PushUrl(VRCUrl url, UdonBehaviour udonSendFunction, string sendCustomEvent, string setVariableName)
        {
            urls = UdonArrayPlus.VRCUrlsAdd(urls, url);
            contents = UdonArrayPlus.StringsAdd(contents, "");
            isLoaded = UdonArrayPlus.BoolsAdd(isLoaded, false);
            udonSendFunctions = UdonArrayPlus.UdonBehavioursAdd(udonSendFunctions, udonSendFunction);
            sendCustomEvents = UdonArrayPlus.StringsAdd(sendCustomEvents, sendCustomEvent);
            setVariableNames = UdonArrayPlus.StringsAdd(setVariableNames, setVariableName);
            if (urls.Length > 0)
                LoadUrl();
        }
        public void DelUrl()
        {
            urls = UdonArrayPlus.VRCUrlsRemoveIndex(urls, 0);
            contents = UdonArrayPlus.StringsRemoveIndex(contents, 0);
            udonSendFunctions = UdonArrayPlus.UdonBehavioursRemoveIndex(udonSendFunctions, 0);
            sendCustomEvents = UdonArrayPlus.StringsRemoveIndex(sendCustomEvents, 0);
            setVariableNames = UdonArrayPlus.StringsRemoveIndex(setVariableNames, 0);
        }
        public override void OnStringLoadSuccess(IVRCStringDownload result)
        {
            isLoading = false;
            contents[0] = result.Result;
            if (!string.IsNullOrWhiteSpace(setVariableNames[0]))
                udonSendFunctions[0].SetProgramVariable(setVariableNames[0], contents[0]);
            if (!string.IsNullOrWhiteSpace(sendCustomEvents[0]))
                udonSendFunctions[0].SendCustomEvent(sendCustomEvents[0]);
            DelUrl();
            if (urls.Length > 0)
                LoadUrl();
        }
        public override void OnStringLoadError(IVRCStringDownload result)
        {
            isLoading = false;
            Debug.LogError($"UdonLab.Toolkit.UrlsStringLoader: {result.ErrorCode} Could not load {result.Url} with error: {result.Error}");
            DelUrl();
            if (urls.Length > 0)
                LoadUrl();
        }
        public void SendFunction() => LoadUrl();
        public void SendFunctions() => LoadUrl();
    }
}
