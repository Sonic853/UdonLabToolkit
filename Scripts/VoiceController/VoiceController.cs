
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class VoiceController : UdonSharpBehaviour
    {
        VRCPlayerApi localPlayerApi;

        VRCPlayerApi[] players = new VRCPlayerApi[0];

        [UdonSynced]
        float gainParam = 0f;

        [UdonSynced]
        float farParam = 0f;
        [SerializeField] float loudGainParam = 22f;
        [SerializeField] float loudFarParam = 150f;

        [UdonSynced]
        int SelectPlayerID = -1;

        [SerializeField] MeshRenderer mr;
        Material[] materials;

        void Start()
        {
            localPlayerApi = Networking.LocalPlayer;
        }


        public void VoiceSwitchON()
        {
            if (!Networking.IsOwner(Networking.LocalPlayer, this.gameObject))
            {
                Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
            }
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "GetOwnPlayer");
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "ParamSetLoud");
            RequestSerialization();
            // SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "ChangeVoiceVolume");
            ChangeVoiceVolume();
        }

        public void VoiceSwitchOff()
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "GetOwnPlayer");
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "ParamSetQuiet");
            RequestSerialization();
            // SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "ChangeVoiceVolume");
            ChangeVoiceVolume();
        }


        public void ParamSetLoud()
        {
            gainParam = loudGainParam;
            farParam = loudFarParam;
            if (mr != null) materials = mr.materials;
            if (materials != null)
            {
                foreach (var material in materials)
                {
                    material.SetColor("_EmissionColor", new Color(0f, 0.5f, 0f));
                }
            }
        }
        public void ParamSetQuiet()
        {
            gainParam = 15f;
            farParam = 25f;
            if (mr != null) materials = mr.materials;
            if (materials != null)
            {
                foreach (var material in materials)
                {
                    material.SetColor("_EmissionColor", new Color(0f, 0f, 0f));
                }
            }
        }


        public void GetOwnPlayer()
        {
            //全プレイヤーを取得
            // VRCPlayerApi.GetPlayers(players);
            players = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()];
            players = VRCPlayerApi.GetPlayers(players);

            //自身のPlayerIDを取得し、同期変数に格納
            foreach (var player in players)
            {
                if (localPlayerApi == player)
                {
                    SelectPlayerID = player.playerId;
                    break;
                }
            }
        }


        public void ChangeVoiceVolume()
        {
            var player = VRCPlayerApi.GetPlayerById(SelectPlayerID);
            player.SetVoiceGain(gainParam);
            player.SetVoiceDistanceFar(farParam);
        }
        public override void OnDeserialization()
        {
            ChangeVoiceVolume();
        }
    }
}
