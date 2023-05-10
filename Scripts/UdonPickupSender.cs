
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class UdonPickupSender : UdonSharpBehaviour
{
    // OnPickup
    /// <summary>
    /// 当玩家拾取时调用的UdonBehaviour
    /// </summary>
    [Header("当玩家拾取时调用的UdonBehaviour")]
    [SerializeField] private UdonBehaviour[] udonSenderOnPickup;
    /// <summary>
    /// 拾取只允许触发一次
    /// </summary>
    [Header("拾取只允许触发一次")]
    [SerializeField] private bool onPickupIsOnce = false;
    /// <summary>
    /// 拾取是否已触发
    /// </summary>
    private bool onPickupIsTriggered = false;
    // OnDrop
    /// <summary>
    /// 当玩家丢弃时调用的UdonBehaviour
    /// </summary>
    [Header("当玩家丢弃时调用的UdonBehaviour")]
    [SerializeField] private UdonBehaviour[] udonSenderOnDrop;
    /// <summary>
    /// 丢弃只允许触发一次
    /// </summary>
    [Header("丢弃只允许触发一次")]
    [SerializeField] private bool onDropIsOnce = false;
    /// <summary>
    /// 丢弃是否已触发
    /// </summary>
    private bool onDropIsTriggered = false;
    // OnPickupUseDown
    /// <summary>
    /// 当玩家按下使用时调用的UdonBehaviour
    /// </summary>
    [Header("当玩家按下使用时调用的UdonBehaviour")]
    [SerializeField] private UdonBehaviour[] udonSenderOnPickupUseDown;
    /// <summary>
    /// 按下使用只允许触发一次
    /// </summary>
    [Header("按下使用只允许触发一次")]
    [SerializeField] private bool onPickupUseDownIsOnce = false;
    /// <summary>
    /// 按下使用是否已触发
    /// </summary>
    private bool onPickupUseDownIsTriggered = false;
    // OnPickupUseUp
    /// <summary>
    /// 当玩家松开使用时调用的UdonBehaviour
    /// </summary>
    [Header("当玩家松开使用时调用的UdonBehaviour")]
    [SerializeField] private UdonBehaviour[] udonSenderOnPickupUseUp;
    /// <summary>
    /// 松开使用只允许触发一次
    /// </summary>
    [Header("松开使用只允许触发一次")]
    [SerializeField] private bool onPickupUseUpIsOnce = false;
    /// <summary>
    /// 松开使用是否已触发
    /// </summary>
    private bool onPickupUseUpIsTriggered = false;
    /// <summary>
    /// 只允许本地玩家触发
    /// </summary>
    [Header("只允许本地玩家触发")]
    [SerializeField] private bool isLocalOnly = true;
    public void OnPickup_()
    {
        if (onPickupIsOnce && onPickupIsTriggered)
            return;
        foreach (UdonBehaviour udon in udonSenderOnPickup)
        {
            udon.SendCustomEvent("SendFunctions");
        }
        onPickupIsTriggered = true;
    }
    public void OnDrop_()
    {
        if (onDropIsOnce && onDropIsTriggered)
            return;
        foreach (UdonBehaviour udon in udonSenderOnDrop)
        {
            udon.SendCustomEvent("SendFunctions");
        }
        onDropIsTriggered = true;
    }
    public void OnPickupUseDown_()
    {
        if (onPickupUseDownIsOnce && onPickupUseDownIsTriggered)
            return;
        foreach (UdonBehaviour udon in udonSenderOnPickupUseDown)
        {
            udon.SendCustomEvent("SendFunctions");
        }
        onPickupUseDownIsTriggered = true;
    }
    public void OnPickupUseUp_()
    {
        if (onPickupUseUpIsOnce && onPickupUseUpIsTriggered)
            return;
        foreach (UdonBehaviour udon in udonSenderOnPickupUseUp)
        {
            udon.SendCustomEvent("SendFunctions");
        }
        onPickupUseUpIsTriggered = true;
    }
    public override void OnPickup()
    {
        if (isLocalOnly)
        {
            OnPickup_();
        }
        else
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "OnPickup_");
        }
    }
    public override void OnDrop()
    {
        if (isLocalOnly)
        {
            OnDrop_();
        }
        else
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "OnDrop_");
        }
    }
    public override void OnPickupUseDown()
    {
        if (isLocalOnly)
        {
            OnPickupUseDown_();
        }
        else
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "OnPickupUseDown_");
        }
    }
    public override void OnPickupUseUp()
    {
        if (isLocalOnly)
        {
            OnPickupUseUp_();
        }
        else
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "OnPickupUseUp_");
        }
    }
}
