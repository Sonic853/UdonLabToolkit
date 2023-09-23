
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonPickupObjects : UdonSharpBehaviour
    {
        [SerializeField] private GameObject[] _Objects = new GameObject[0];
        [SerializeField] private bool once = false;
        private bool Pickuped = false;
        public void OnPickup_()
        {
            if (once && !Pickuped)
            {
                Pickuped = true;
            }
            else if (once && Pickuped)
            {
                return;
            }
            for (int i = 0; i < _Objects.Length; i++)
            {
                if (_Objects[i] != null)
                    _Objects[i].SetActive(!_Objects[i].activeSelf);
            }
        }
        public override void OnPickup()
        {
            OnPickup_();
        }
    }
}
