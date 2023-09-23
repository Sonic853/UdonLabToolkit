
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;

namespace UdonLab.Toolkit
{
    public class MusicPlayerUI : UdonSharpBehaviour
    {
        [SerializeField] private UdonGlobalMusicPlayer _musicPlayer;
        [SerializeField] private TextMeshProUGUI[] titleTexts;
        public void UpdateUI()
        {
            if (_musicPlayer == null) return;
            foreach (var titleText in titleTexts)
            {
                if (titleText == null) continue;
                titleText.text = _musicPlayer._audioSource.clip.name;
            }
        }
    }
}