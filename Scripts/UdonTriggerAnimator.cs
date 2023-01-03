
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
        /// <summary>
        /// Udon Array Plus
        /// </summary>
        [Header("UdonArrayPlus")]
        [SerializeField] private UdonArrayPlus udonArrayPlus;
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
        [NonSerialized] public VRCPlayerApi _OnPlayerTriggerEnter_VRCPlayerApi = null;
        public void _OnPlayerTriggerEnter()
        {
            var player = _OnPlayerTriggerEnter_VRCPlayerApi;
            if (isLocalOnly && !player.isLocal)
                return;
            _playersName = udonArrayPlus.stringsAdd2(_playersName, player.displayName);
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
            _OnPlayerTriggerEnter_VRCPlayerApi = player;
            _OnPlayerTriggerEnter();
            _OnPlayerTriggerEnter_VRCPlayerApi = null;
        }
        public void SetExit()
        {
            if (_playersName.Length == 0) havePlayerEnter = false;
            isExited = true;
        }
        [NonSerialized] public VRCPlayerApi _OnPlayerTriggerExit_VRCPlayerApi = null;
        public void _OnPlayerTriggerExit()
        {
            var player = _OnPlayerTriggerExit_VRCPlayerApi;
            if (isLocalOnly && !player.isLocal)
                return;
            _playersName = udonArrayPlus.stringsRemove(_playersName, player.displayName);
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
            _OnPlayerTriggerExit_VRCPlayerApi = player;
            _OnPlayerTriggerExit();
            _OnPlayerTriggerExit_VRCPlayerApi = null;
        }
        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            _playersName = udonArrayPlus.stringsRemove(_playersName, player.displayName);
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
