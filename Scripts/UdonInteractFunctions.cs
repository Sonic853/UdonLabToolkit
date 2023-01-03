
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonInteractFunctions : UdonSharpBehaviour
    {
        [SerializeField] private UdonBehaviour[] udonBehaviours;
        [SerializeField] private string[] functionNames;
        public void _Interact()
        {
            foreach (var udonBehaviour in udonBehaviours)
            {
                if (udonBehaviour == null)
                    continue;
                foreach (var functionName in functionNames)
                {
                    if (string.IsNullOrEmpty(functionName))
                        continue;
                    udonBehaviour.SendCustomEvent(functionName);
                }
            }
        }
        public override void Interact()
        {
            _Interact();
        }
    }
}
