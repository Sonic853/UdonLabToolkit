
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace UdonLab.Toolkit
{
    public class ChangeAvatar : UdonSharpBehaviour
    {
        [SerializeField] private VRC_AvatarPedestal _avatarPedestal;
        public string avatarID = "avtr_11451419-1981-0114-5141-919810114514";
        public void Change()
        {
            _avatarPedestal.blueprintId = avatarID;
            _avatarPedestal.ChangeAvatarsOnUse = true;
            _avatarPedestal.SetAvatarUse(Networking.LocalPlayer);
        }
    }
}