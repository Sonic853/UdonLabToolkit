
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    // 手动同步
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class UdonGlobalAudioPlayer : UdonSharpBehaviour
    {
        [UdonSynced] public bool _isPlaying = false;
        [SerializeField] GameObject[] Buttons;
        [SerializeField] AudioSource _audioSource;
        bool _isInit = false;
        void Update()
        {
            if (!_isInit && Networking.IsOwner(gameObject))
            {
                _isInit = true;
            }
            if (!_isInit) return;
            if (_audioSource.isPlaying)
            {
                if (Buttons[0] != null && Buttons[0].activeSelf == true)
                    Buttons[0].SetActive(false);
                if (Buttons[1] != null && Buttons[1].activeSelf == false)
                    Buttons[1].SetActive(true);
            }
            else
            {
                if (Buttons[0] != null && Buttons[0].activeSelf == false)
                    Buttons[0].SetActive(true);
                if (Buttons[1] != null && Buttons[1].activeSelf == true)
                    Buttons[1].SetActive(false);
            }
        }
        public void Play()
        {
            _isPlaying = true;
            if (!_audioSource.enabled) _audioSource.enabled = true;
            if (!_audioSource.isPlaying) _audioSource.Play();
            if (Buttons[0] != null) Buttons[0].SetActive(false);
            if (Buttons[1] != null) Buttons[1].SetActive(true);
            if (Networking.IsOwner(gameObject))
            {
                RequestSerialization();
            }
        }
        public void Stop()
        {
            _isPlaying = false;
            if (_audioSource.isPlaying) _audioSource.Stop();
            if (Buttons[0] != null) Buttons[0].SetActive(true);
            if (Buttons[1] != null) Buttons[1].SetActive(false);
            if (Networking.IsOwner(gameObject))
            {
                RequestSerialization();
            }
        }
        public void Toggle()
        {
            if (_audioSource.isPlaying)
            {
                Stop();
            }
            else
            {
                Play();
            }
        }
        public override void OnDeserialization()
        {
            _isInit = true;
            if (!_audioSource.enabled) _audioSource.enabled = true;
            if (_isPlaying)
            {
                if (!_audioSource.isPlaying) _audioSource.Play();
            }
            else
            {
                if (_audioSource.isPlaying) _audioSource.Stop();
            }
        }
    }
}