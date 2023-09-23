
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;

namespace UdonLab.Toolkit
{
    // 手动同步
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class UdonGlobalMusicPlayer : UdonSharpBehaviour
    {
        /// <summary>
        /// 播放状态：0: 停止, 1: 播放, 2: 暂停
        /// </summary>
        [NonSerialized][UdonSynced] public int _playingStatus = 0; // 0: stop, 1: play, 2: pause
        /// <summary>
        /// 播放时间
        /// </summary>
        [NonSerialized][UdonSynced] public float _unixtime = 0f;
        /// <summary>
        /// 播放索引
        /// </summary>
        [NonSerialized][UdonSynced] public int _playIndex = -1;
        /// <summary>
        /// 是否循环 0: 不循环，1: 循环当曲，2: 循环列表
        /// </summary>
        [UdonSynced] public int _isLoop = 2;
        /// <summary>
        /// 偏移
        /// </summary>
        public float offset = 0f;
        /// <summary>
        /// 是否自动播放
        /// </summary>
        public bool _isAutoPlay = false;
        /// <summary>
        /// 是否已初始化
        /// </summary>
        [NonSerialized] public bool _isInit = false;
        /// <summary>
        /// 音频源
        /// </summary>
        public AudioSource _audioSource;
        /// <summary>
        /// 音频列表
        /// </summary>
        public AudioClip[] _audioClips;
        public UdonBehaviour[] _uiSenders;
        void Start()
        {
            if (!_isInit && Networking.IsOwner(gameObject))
            {
                if (_audioSource == null)
                {
                    _audioSource = GetComponent<AudioSource>();
                }
                if (_isAutoPlay)
                {
                    _playingStatus = 1;
                    _unixtime = GetUnixTime();
                    _playIndex = 0;
                    // _isLoop = false;
                    _audioSource.loop = _isLoop == 1;
                    _audioSource.clip = _audioClips[_playIndex];
                    _audioSource.Play();
                    foreach (var uiSender in _uiSenders)
                    {
                        uiSender.SendCustomEvent("UpdateUI");
                    }
                }
            }
            _isInit = true;
        }
        void Update()
        {
            if (_isLoop == 2)
            {
                if (_audioSource.isPlaying && _audioSource.time >= _audioSource.clip.length - 0.1f)
                {
                    // _playIndex++;
                    // if (_playIndex >= _audioClips.Length)
                    // {
                    //     _playIndex = 0;
                    // }
                    // _audioSource.clip = _audioClips[_playIndex];
                    // _audioSource.time = 0f;
                    // _audioSource.Play();
                    if (Networking.IsOwner(gameObject))
                    {
                        Next();
                        foreach (var uiSender in _uiSenders)
                        {
                            uiSender.SendCustomEvent("UpdateUI");
                        }
                    }
                }
            }
        }
        public void Play()
        {
            if (Networking.IsOwner(gameObject))
            {
                if (_playingStatus != 1)
                {
                    _playingStatus = 1;
                }
                // 判断 _audioSource 是否播放完毕
                if (_audioSource.time >= _audioSource.clip.length)
                {
                    _audioSource.clip = _audioClips[_playIndex];
                    _audioSource.time = 0f;
                }
                _unixtime = GetUnixTime() - _audioSource.time;
                _audioSource.Play();
                RequestSerialization();
                foreach (var uiSender in _uiSenders)
                {
                    uiSender.SendCustomEvent("UpdateUI");
                }
            }
            else
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "Play");
            }
        }
        public void Stop()
        {
            if (Networking.IsOwner(gameObject))
            {
                if (_playingStatus == 0)
                {
                    return;
                }
                _playingStatus = 0;
                _unixtime = 0f;
                _playIndex = -1;
                // _isLoop = false;
                _audioSource.Stop();
                RequestSerialization();
                foreach (var uiSender in _uiSenders)
                {
                    uiSender.SendCustomEvent("UpdateUI");
                }
            }
            else
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "Stop");
            }
        }
        public void Pause()
        {
            if (Networking.IsOwner(gameObject))
            {
                if (_playingStatus != 2)
                {
                    _playingStatus = 2;
                }
                // _unixtime = GetUnixTime();
                _audioSource.Pause();
                RequestSerialization();
                foreach (var uiSender in _uiSenders)
                {
                    uiSender.SendCustomEvent("UpdateUI");
                }
            }
            else
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "Pause");
            }
        }
        public void UnPause()
        {
            if (Networking.IsOwner(gameObject))
            {
                if (_playingStatus != 1)
                {
                    _playingStatus = 1;
                }
                _unixtime = GetUnixTime() - _audioSource.time;
                _audioSource.UnPause();
                RequestSerialization();
                foreach (var uiSender in _uiSenders)
                {
                    uiSender.SendCustomEvent("UpdateUI");
                }
            }
            else
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "UnPause");
            }
        }
        public void TogglePlay()
        {
            if (Networking.IsOwner(gameObject))
            {
                if (_playingStatus == 1)
                {
                    Pause();
                }
                else
                {
                    UnPause();
                }
            }
            else
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "TogglePlay");
            }
        }
        public void Next()
        {
            if (Networking.IsOwner(gameObject))
            {
                if (_playIndex + 1 >= _audioClips.Length)
                {
                    // if (!_isLoop)
                    //     return;
                    _playIndex = 0;
                }
                else
                {
                    _playIndex++;
                }
                _playingStatus = 1;
                _unixtime = GetUnixTime();
                _audioSource.clip = _audioClips[_playIndex];
                _audioSource.time = 0f;
                _audioSource.Play();
                RequestSerialization();
                foreach (var uiSender in _uiSenders)
                {
                    uiSender.SendCustomEvent("UpdateUI");
                }
            }
            else
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "Next");
            }
        }
        public void Prev()
        {
            if (Networking.IsOwner(gameObject))
            {
                if (_playIndex - 1 < 0)
                {
                    // if (!_isLoop)
                    //     return;
                    _playIndex = _audioClips.Length - 1;
                }
                else
                {
                    _playIndex--;
                }
                _playingStatus = 1;
                _unixtime = GetUnixTime();
                _audioSource.clip = _audioClips[_playIndex];
                _audioSource.Play();
                RequestSerialization();
                foreach (var uiSender in _uiSenders)
                {
                    uiSender.SendCustomEvent("UpdateUI");
                }
            }
            else
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "Prev");
            }
        }
        public void LoopOn()
        {
            if (Networking.IsOwner(gameObject))
            {
                _audioSource.loop = _isLoop == 1;
                RequestSerialization();
                foreach (var uiSender in _uiSenders)
                {
                    uiSender.SendCustomEvent("UpdateUI");
                }
            }
            else
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "LoopOn");
            }
        }
        public void LoopOff()
        {
            if (Networking.IsOwner(gameObject))
            {
                _audioSource.loop = _isLoop == 1;
                RequestSerialization();
                foreach (var uiSender in _uiSenders)
                {
                    uiSender.SendCustomEvent("UpdateUI");
                }
            }
            else
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "LoopOff");
            }
        }
        public float GetLength()
        {
            if (_audioSource.clip == null)
            {
                return 0f;
            }
            return _audioSource.clip.length;
        }
        float GetUnixTime()
        {
            var datetime = Networking.GetNetworkDateTime();
            // 加上 8 小时，转换为北京时间，保留小数点后 2 位
            return (float)System.Math.Round((double)(datetime.AddHours(8).Ticks - 621355968000000000) / 10000000, 2);
        }
        public override void OnDeserialization(DeserializationResult result)
        {
            // if (_audioSource == null)
            // {
            //     return;
            // }
            _audioSource.loop = _isLoop == 1;
            if (_audioSource.clip != _audioClips[_playIndex])
            {
                _audioSource.clip = _audioClips[_playIndex];
            }
            // if (_playingStatus == 1)
            // {
            //     var time = (float)(GetUnixTime() - _unixtime) + offset;
            //     if (time > _audioSource.clip.length)
            //     {
            //         time = time % _audioSource.clip.length;
            //     }
            //     _audioSource.time = time;
            //     if (!_audioSource.isPlaying)
            //     {
            //         _audioSource.Play();
            //     }
            // }
            // else if (_playingStatus == 2)
            // {
            //     if (_audioSource.isPlaying)
            //     {
            //         _audioSource.Pause();
            //     }
            // }
            // else if (_playingStatus == 0)
            // {
            //     if (_audioSource.isPlaying)
            //     {
            //         _audioSource.Stop();
            //     }
            // }
            switch (_playingStatus)
            {
                case 0:
                    if (_audioSource.isPlaying)
                    {
                        _audioSource.Stop();
                    }
                    break;
                case 1:
                    {
                        var time = (float)(GetUnixTime() - _unixtime) + offset;
                        if (time > _audioSource.clip.length)
                        {
                            time = time % _audioSource.clip.length;
                        }
                        _audioSource.time = time;
                        if (!_audioSource.isPlaying)
                        {
                            _audioSource.Play();
                        }
                    }
                    break;
                case 2:
                    if (_audioSource.isPlaying)
                    {
                        _audioSource.Pause();
                    }
                    break;
            }
            foreach (var uiSender in _uiSenders)
            {
                uiSender.SendCustomEvent("UpdateUI");
            }
        }
    }
}
