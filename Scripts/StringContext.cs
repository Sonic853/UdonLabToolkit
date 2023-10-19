
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class StringContext : UdonSharpBehaviour
{
    public string stringContext;
    public Text text;
    public bool LoadString()
    {
        if (text == null)
            return false;
        text.text = stringContext;
        return true;
    }
}
