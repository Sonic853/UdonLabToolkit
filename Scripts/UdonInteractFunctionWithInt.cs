
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonInteractFunctionWithInt : UdonSharpBehaviour
    {
        [SerializeField] public UdonBehaviour[] udonBehaviours;
        [SerializeField] public string functionName;
        [SerializeField] public string setIntValue;
        [SerializeField] public int value;
        public void _Interact()
        {
            foreach (var udonBehaviour in udonBehaviours)
            {
                if (udonBehaviour == null)
                    continue;
                if (string.IsNullOrEmpty(functionName))
                    continue;
                if (!string.IsNullOrEmpty(setIntValue))
                {
                    udonBehaviour.SetProgramVariable(setIntValue, value);
                }
                udonBehaviour.SendCustomEvent(functionName);
            }
        }
        public override void Interact()
        {
            _Interact();
        }
    }
}
