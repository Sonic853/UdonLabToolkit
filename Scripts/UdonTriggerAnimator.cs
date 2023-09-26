
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class UdonTriggerAnimator : UdonSharpBehaviour
    {
        // /// <summary>
        // /// Udon Array Plus
        // /// </summary>
        // [Header("UdonArrayPlus")]
        // [SerializeField] private UdonArrayPlus udonArrayPlus;
        /// <summary>
        /// 动画器
        /// </summary>
        [Header("动画器")]
        [SerializeField] private Animator animator;
        [SerializeField] private bool isLocalOnly = true;
        [UdonSynced][NonSerialized] private bool _havePlayerEnter = false;
        public bool havePlayerEnter
        {
            get
            {
                if (animator == null) return _havePlayerEnter;
                return animator.GetBool("Enter");
            }
            set
            {
                if (animator == null) _havePlayerEnter = value;
                else animator.SetBool("Enter", value);
            }
        }
        [UdonSynced][NonSerialized] private bool _isEntered = false;
        public bool isEntered
        {
            get
            {
                if (animator == null) return _isEntered;
                return animator.GetBool("Entered");
            }
            set
            {
                if (animator == null || !isLocalOnly) _isEntered = value;
                if (animator != null) animator.SetBool("Entered", value);
            }
        }
        [UdonSynced][NonSerialized] private bool _isExited = false;
        [UdonSynced][NonSerialized] private string[] _playersName = new string[0];
        public bool isExited
        {
            get
            {
                if (animator == null) return _isExited;
                return animator.GetBool("Exited");
            }
            set
            {
                if (animator == null || !isLocalOnly) _isExited = value;
                if (animator != null) animator.SetBool("Exited", value);
            }
        }
        public void SetEnter()
        {
            havePlayerEnter = true;
            isEntered = true;
        }
        [NonSerialized] public VRCPlayerApi OnPlayerTriggerEnter_VRCPlayerApi_ = null;
        public void OnPlayerTriggerEnter_()
        {
            var player = OnPlayerTriggerEnter_VRCPlayerApi_;
            if (isLocalOnly && !player.isLocal)
                return;
            _playersName = UdonArrayPlus.StringsAdd2(_playersName, player.displayName);
            if (isLocalOnly)
            {
                SetEnter();
            }
            else if (player.isLocal)
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SetEnter");
            }
        }
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            OnPlayerTriggerEnter_VRCPlayerApi_ = player;
            OnPlayerTriggerEnter_();
            OnPlayerTriggerEnter_VRCPlayerApi_ = null;
        }
        public void SetExit()
        {
            if (_playersName.Length == 0) havePlayerEnter = false;
            isExited = true;
        }
        [NonSerialized] public VRCPlayerApi OnPlayerTriggerExit_VRCPlayerApi_ = null;
        public void OnPlayerTriggerExit_()
        {
            var player = OnPlayerTriggerExit_VRCPlayerApi_;
            if (isLocalOnly && !player.isLocal)
                return;
            _playersName = UdonArrayPlus.StringsRemove(_playersName, player.displayName);
            if (isLocalOnly)
            {
                SetExit();
            }
            else if (player.isLocal)
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SetExit");
            }
        }
        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            OnPlayerTriggerExit_VRCPlayerApi_ = player;
            OnPlayerTriggerExit_();
            OnPlayerTriggerExit_VRCPlayerApi_ = null;
        }
        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            _playersName = UdonArrayPlus.StringsRemove(_playersName, player.displayName);
        }
        public override void OnDeserialization()
        {
            if (animator == null || isLocalOnly) return;
            animator.SetBool("Enter", _havePlayerEnter);
            animator.SetBool("Entered", _isEntered);
            animator.SetBool("Exited", _isExited);
        }
    }
}
