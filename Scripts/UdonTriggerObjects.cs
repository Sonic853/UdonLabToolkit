using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonTriggerObjects : UdonSharpBehaviour
    {
        [SerializeField] private GameObject[] _Objects = new GameObject[0];
        private bool[] _objActive = new bool[0];
        [SerializeField] private bool reverse;
        [SerializeField] private bool once;
        void Start()
        {
            _objActive = new bool[_Objects.Length];
            for (int i = 0; i < _Objects.Length; i++)
            {
                if (_Objects[i] != null)
                {
                    _objActive[i] = _Objects[i].activeSelf;
                }
                else
                {
                    _objActive[i] = false;
                }
            }
        }
        [NonSerialized] public VRCPlayerApi OnPlayerTriggerEnter_VRCPlayerApi_ = null;
        public void OnPlayerTriggerEnter_()
        {
            var player = OnPlayerTriggerEnter_VRCPlayerApi_;
            if (player.isLocal)
            {
                for (int i = 0; i < _Objects.Length; i++)
                {
                    if (_Objects[i] != null)
                        _Objects[i].SetActive(reverse ? _objActive[i] : !_objActive[i]);
                }
            }
        }
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            OnPlayerTriggerEnter_VRCPlayerApi_ = player;
            OnPlayerTriggerEnter_();
            OnPlayerTriggerEnter_VRCPlayerApi_ = null;
        }
        [NonSerialized] public VRCPlayerApi OnPlayerTriggerExit_VRCPlayerApi_ = null;
        public void OnPlayerTriggerExit_()
        {
            if (once) return;
            var player = OnPlayerTriggerExit_VRCPlayerApi_;
            if (player.isLocal)
            {
                for (int i = 0; i < _Objects.Length; i++)
                {
                    if (_Objects[i] != null)
                        _Objects[i].SetActive(reverse ? !_objActive[i] : _objActive[i]);
                }
            }
        }
        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            OnPlayerTriggerExit_VRCPlayerApi_ = player;
            OnPlayerTriggerExit_();
            OnPlayerTriggerExit_VRCPlayerApi_ = null;
        }
    }
}
