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
        [NonSerialized] public VRCPlayerApi _OnPlayerTriggerEnter_VRCPlayerApi = null;
        public void _OnPlayerTriggerEnter()
        {
            var player = _OnPlayerTriggerEnter_VRCPlayerApi;
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
            _OnPlayerTriggerEnter_VRCPlayerApi = player;
            _OnPlayerTriggerEnter();
            _OnPlayerTriggerEnter_VRCPlayerApi = null;
        }
        [NonSerialized] public VRCPlayerApi _OnPlayerTriggerExit_VRCPlayerApi = null;
        public void _OnPlayerTriggerExit()
        {
            var player = _OnPlayerTriggerExit_VRCPlayerApi;
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
            _OnPlayerTriggerExit_VRCPlayerApi = player;
            _OnPlayerTriggerExit();
            _OnPlayerTriggerExit_VRCPlayerApi = null;
        }
    }
}
