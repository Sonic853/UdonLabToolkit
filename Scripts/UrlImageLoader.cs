
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Image;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace UdonLab.Toolkit
{
    public class UrlImageLoader : UdonSharpBehaviour
    {
        public VRCUrl ImageUrl;
        public Texture2D texture;
        public bool LoadOnStart = true;
        public bool ImageLoaded = false;
        public UdonBehaviour[] udonSendFunctions;
        void Start()
        {
            if (LoadOnStart)
            {
                LoadImage();
            }
        }
        public void LoadImage()
        {
            ImageLoaded = false;
            VRCImageDownloader.ImageDownloader.DownloadImage(ImageUrl, null, (IUdonEventReceiver)this, null);
        }
        public override void OnImageLoadSuccess(IVRCImageDownload result)
        {
            ImageLoaded = true;
            texture = result.Result;
            foreach (var udonSendFunction in udonSendFunctions)
            {
                udonSendFunction.SendCustomEvent("SendFunctions");
            }
        }
    }
}
