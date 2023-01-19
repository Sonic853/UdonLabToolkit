using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonInteractObjects : UdonSharpBehaviour
    {
        [SerializeField] private GameObject[] _Objects = new GameObject[0];
        public void Interact_()
        {
            for (int i = 0; i < _Objects.Length; i++)
            {
                if (_Objects[i] != null)
                    _Objects[i].SetActive(!_Objects[i].activeSelf);
            }
        }
        public override void Interact()
        {
            Interact_();
        }
    }
}
