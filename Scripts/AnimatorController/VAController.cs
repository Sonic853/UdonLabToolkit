
using System;
using HoshinoLabs.IwaSync3.Udon;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Video.Components.Base;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class VAController : VideoCoreEventListener
    {
        [SerializeField] private UdonAnimatorController udonAnimatorController;
        [SerializeField] private VideoCore videoCore;
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private BaseVRCVideoPlayer bvvp;
        [SerializeField] private UdonArrayPlus udonArrayPlus;
        [SerializeField] private GameObject content;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Text status;
        [SerializeField] private Text offsetui;
        [SerializeField] private InputField offsetif;
        [SerializeField] private float offset = 0.3f;
        private UdonBehaviour[] udonBehaviours;
        private string[] functionNames = new string[] { "SelectState" };
        private string[] valueNames = new string[] { "SelectState_Name" };
        [UdonSynced] private string[] offsetPlayers = new string[0];
        VRCPlayerApi localPlayer;
        [UdonSynced] bool playing = false;
        [UdonSynced] bool paused = false;
        void Start()
        {
            if (Networking.LocalPlayer != null) localPlayer = Networking.LocalPlayer;
            udonBehaviours = new UdonBehaviour[] { (UdonBehaviour)GetComponent(typeof(UdonBehaviour)) };
            if (videoCore != null) videoCore.AddListener(this);
            if (udonAnimatorController != null
            && udonAnimatorController.GetAnimator() != null
            && content != null
            && prefab != null)
            {
                if (Networking.IsOwner(gameObject))
                    PlayState_Name = udonAnimatorController.GetCurrentStateName();
                UpdateUI();
                var stateNames = udonAnimatorController.GetStateNames();
                foreach (var stateName in stateNames)
                {
                    InsertState(stateName);
                }
            }
            if (offsetui != null) offsetui.text = $"偏移：{offset.ToString()}s";
            if (offsetif != null) offsetif.text = offset.ToString();
        }
        // void Update()
        // {
        //     // Debug.Log($"videoCore.isPlaying: {videoCore.isPlaying}");
        //     // Debug.Log($"videoCore.paused: {videoCore.paused}");
        //     // if (videoCore.isPlaying && !videoCore.paused)
        //     // {
        //     // Debug.Log($"isPlaying: {videoCore.time} / {videoCore.duration}");
        //     // }
        // }
        [HideInInspector][UdonSynced] public string PlayState_Name = "";
        public void PlayState()
        {
            if (!playing || paused
            || string.IsNullOrEmpty(PlayState_Name)
            || videoCore.duration == 0)
                return;
            var videoCoretime = videoCore.time;
            videoCoretime += offset;
            videoCoretime = videoCoretime < 0 ? 0 : videoCoretime;
            udonAnimatorController.PlayState(PlayState_Name, Mathf.Clamp(videoCoretime / videoCore.duration, 0f, 1f));
        }
        public void PlayStateTime(float time)
        {
            if (!playing || paused
            || string.IsNullOrEmpty(PlayState_Name)
            || videoCore.duration == 0)
                return;
            udonAnimatorController.PlayState(PlayState_Name, Mathf.Clamp(time, 0f, 1f));
        }
        [NonSerialized] public string SelectState_Name = "";
        public void SelectState()
        {
            if (!Networking.IsOwner(gameObject))
                Networking.SetOwner(Networking.LocalPlayer, gameObject);
            if (string.IsNullOrEmpty(SelectState_Name))
                return;
            PlayState_Name = SelectState_Name;
            SelectState_Name = "";
            RequestSerialization();
            OnDeserialization();
        }
        [NonSerialized] public string JoinOffset_Name = "";
        public void JoinOffset()
        {
            if (string.IsNullOrEmpty(JoinOffset_Name))
                return;
            if (!Networking.IsOwner(gameObject))
                Networking.SetOwner(Networking.LocalPlayer, gameObject);
            var _offsetPlayers = udonArrayPlus.stringsAdd2(offsetPlayers, JoinOffset_Name);
            if (offsetPlayers.Length != _offsetPlayers.Length
            && videoCore.isPlaying && !videoCore.paused
            && bvvp != null)
            {
                var nowtime = bvvp.GetTime();
                nowtime += offset;
                nowtime = nowtime < 0 ? 0 : nowtime;
                bvvp.SetTime(nowtime);
            }
            offsetPlayers = _offsetPlayers;
            JoinOffset_Name = "";
            RequestSerialization();
            OnDeserialization();
        }
        [NonSerialized] public string LeaveOffset_Name = "";
        public void LeaveOffset()
        {
            if (string.IsNullOrEmpty(LeaveOffset_Name))
                return;
            if (!Networking.IsOwner(gameObject))
                Networking.SetOwner(Networking.LocalPlayer, gameObject);
            var _offsetPlayers = udonArrayPlus.stringsRemove(offsetPlayers, LeaveOffset_Name);
            if (offsetPlayers.Length != _offsetPlayers.Length
            && videoCore.isPlaying && !videoCore.paused
            && bvvp != null)
            {
                var nowtime = bvvp.GetTime();
                nowtime -= offset;
                nowtime = nowtime < 0 ? 0 : nowtime;
                bvvp.SetTime(nowtime);
            }
            offsetPlayers = _offsetPlayers;
            LeaveOffset_Name = "";
            RequestSerialization();
            OnDeserialization();
        }
        public void UpdateOffset()
        {
            // 将 offsetui.text 应用到 offset
            if (float.TryParse(offsetif.text, out var _offset))
            {
                offset = _offset;
            }
            if (offsetui != null) offsetui.text = $"偏移：{offset.ToString()}s";
            offsetif.text = offset.ToString();
        }
        public void Pause()
        {
            udonAnimatorController.Pause();
        }
        public void PauseOn()
        {
            udonAnimatorController.PauseOn();
        }
        public void PauseOff()
        {
            udonAnimatorController.PauseOff();
            PlayState();
        }
        public void Stop()
        {
            udonAnimatorController.Stop();
        }
        public void InsertState(string stateName)
        {
            var item = Instantiate(prefab, content.transform);
            var text = (Text)item.GetComponentInChildren(typeof(Text));
            text.text = stateName;
            var _udonSharpBehaviour = (UdonSharpBehaviour)item.GetComponent(typeof(UdonSharpBehaviour));
            if (_udonSharpBehaviour != null
            && _udonSharpBehaviour.GetUdonTypeName() == "UdonLab.Toolkit.UdonSendFunctionWithString")
            {
                var udonSendFunctionWithString = (UdonSendFunctionWithString)_udonSharpBehaviour;
                udonSendFunctionWithString.udonBehaviours = udonBehaviours;
                udonSendFunctionWithString.functionNames = functionNames;
                udonSendFunctionWithString.valueNames = valueNames;
                udonSendFunctionWithString.values = new string[] { stateName };
            }
        }
        public override void OnVideoStart()
        {
            if (localPlayer == null)
            {
                if (Networking.LocalPlayer != null) localPlayer = Networking.LocalPlayer;
                else return;
            }
            if (udonArrayPlus.stringsIndex(offsetPlayers, localPlayer.displayName) != -1
            && bvvp != null)
            {
                var nowtime = bvvp.GetTime();
                nowtime += offset;
                nowtime = nowtime < 0 ? 0 : nowtime;
                bvvp.SetTime(nowtime);
            }
            playing = true;
            PlayState();
        }
        public override void OnVideoEnd()
        {
            playing = false;
            Stop();
        }
        public override void OnPlayerPlay()
        {
            paused = false;
            PauseOff();
        }
        public override void OnPlayerPause()
        {
            paused = true;
            PauseOn();
        }
        public override void OnPlayerStop()
        {
            paused = false;
            playing = false;
            Stop();
        }
        public void UpdateUI()
        {
            if (localPlayer == null)
            {
                if (Networking.LocalPlayer != null) localPlayer = Networking.LocalPlayer;
                else return;
            }
            var isJoinOffset = udonArrayPlus.stringsIndex(offsetPlayers, localPlayer.displayName) != -1;
            if (status != null)
                status.text = $"动画：{PlayState_Name} 已加入偏移：{isJoinOffset.ToString()}";
        }
        public void OnProgressChanged()
        {
            if (playing && !paused)
            {
                if (_progressSlider != null) PlayStateTime(_progressSlider.value);
            }
            RequestSerialization();
        }
        public override void OnDeserialization()
        {
            playing = videoCore.isPlaying;
            paused = videoCore.paused;
            UpdateUI();
            PlayState();
        }
    }
}