using System;
using Koyashiro.GenericDataContainer;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Data;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class CheckModels : UdonSharpBehaviour
    {
        public Color[] colors =
        {
            Color.black,
            Color.red,     // RED
            Color.blue,  // BLUE
            Color.white,    // WHITE
            new Color(1, 0.5f, 0), // ORANGE
            Color.green,    // GREEN
            new Color(1, 0, 1),    // PURPLE
            new Color(1, 0, 0.5f),    // PINK
            new Color(1, 1, 0),    // YELLOW
            new Color(0.5f, 1, 0.5f),    // LIGHT GREEN
            new Color(0.5f, 0.5f, 1),    // LIGHT BLUE
            new Color(1, 0.5f, 0.75f),   // LIGHT PINK
            new Color(1, 0.5f, 1),   // VIOLET
            new Color(0.75f, 1, 0.5f),   // LIME
            new Color(0.5f, 1, 0.75f),   // TURQUOISE
            new Color(1, 0, 0.25f)   // HOT PINK
        };
        [UdonSynced] private int ColorNum = 1;
        // [SerializeField] UdonArrayPlus udonArrayPlus;
        VRCPlayerApi localPlayer;
        public GameObject model;
        // VRCPlayerApi[] targetPlayers = new VRCPlayerApi[0];
        DataDictionary targetPlayersl = new DataDictionary();
        DataDictionary targetPlayersr = new DataDictionary();
        // [NonSerialized] public GameObject[] instantiatedModels = new GameObject[0];
        // [VRC.Udon.Serialization.OdinSerializer.OdinSerialize] /* UdonSharp auto-upgrade: serialization */ public DataList<GameObject> instantiatedModels;
        [NonSerialized] public DataList instantiatedModels = new DataList();
        [NonSerialized] public DataList instantiatedPenLight2 = new DataList();
        public float targetDistance = 0.14708f;
        // 每 1 秒检查一次自定义模型
        public float checkInterval = 0.1f;
        float lastCheckTime = 0f;
        public bool isDebug = false;
        void Start()
        {
            if (Networking.LocalPlayer != null) localPlayer = Networking.LocalPlayer;
            // var _targetPlayers = DataDictionary<VRCPlayerApi, GameObject>.New();
            // targetPlayers = (DataDictionary)(object)_targetPlayers;
        }
        void Update()
        {
            if (lastCheckTime > checkInterval)
            {
                lastCheckTime = 0f;
                CheckSelf();
                CheckAllPlayer();
                UpdateColor();
            }
            else
            {
                lastCheckTime += Time.deltaTime;
            }
            SetAllPosition();
        }
        void SetAllPosition()
        {
            // var targetPlayers_ = (DataDictionary<VRCPlayerApi, GameObject>)(object)targetPlayers;
            // var keys = targetPlayers_.GetKeys();
            // for (int i = 0; i < keys.Count(); i++)
            // {
            //     var result = keys.TryGetValue(i, out var key);
            //     if (!result) continue;
            //     if (key == null) continue;
            //     var result2 = targetPlayers_.TryGetValue(key, out var models);
            //     if (!result2) continue;
            //     for (int j = 0; j < models.Count(); j++)
            //     {
            //         var result3 = models.TryGetValue(j, out var model);
            //         if (!result3) continue;
            //         if (model == null) continue;
            //         // j 只有 0 和 1 ，0 为左手，1 为右手
            //         if (j == 0)
            //         {
            //             model.transform.position = key.GetBonePosition(HumanBodyBones.LeftHand);
            //             model.transform.rotation = key.GetBoneRotation(HumanBodyBones.LeftHand);
            //             continue;
            //         }
            //         if (j == 1)
            //         {
            //             model.transform.position = key.GetBonePosition(HumanBodyBones.RightHand);
            //             model.transform.rotation = key.GetBoneRotation(HumanBodyBones.RightHand);
            //             continue;
            //         }
            //     }
            // }
            var targetPlayersl_ = (DataDictionary<VRCPlayerApi, GameObject>)(object)targetPlayersl;
            var targetPlayersr_ = (DataDictionary<VRCPlayerApi, GameObject>)(object)targetPlayersr;
            var keys = targetPlayersl_.GetKeys();
            for (int i = 0; i < keys.Count(); i++)
            {
                var result = keys.TryGetValue(i, out var key);
                if (!result) continue;
                if (key == null) continue;
                var result2 = targetPlayersl_.TryGetValue(key, out var model);
                if (!result2) continue;
                if (model == null) continue;
                model.transform.SetPositionAndRotation(key.GetBonePosition(HumanBodyBones.LeftHand), key.GetBoneRotation(HumanBodyBones.LeftHand));
                result2 = targetPlayersr_.TryGetValue(key, out model);
                if (!result2) continue;
                if (model == null) continue;
                model.transform.position = key.GetBonePosition(HumanBodyBones.RightHand);
                // 反转 Y 轴
                var rotation_ = key.GetBoneRotation(HumanBodyBones.RightHand);
                model.transform.rotation = new Quaternion(rotation_.x, rotation_.y, rotation_.z, rotation_.w);
            }
        }
        bool Check(VRCPlayerApi player)
        {
            if (isDebug) return true;
            // 计算距离
            var boneDistance = Vector3.Distance(player.GetBonePosition(HumanBodyBones.Chest), player.GetBonePosition(HumanBodyBones.Neck));
            // 只保留小数点后五位，不使用四舍五入
            boneDistance = Mathf.Floor(boneDistance * 100000) / 100000;
            // 如果距离不等于 targetDistance，则认为当前玩家使用的是自定义模型
            return boneDistance == targetDistance;
            // return true;
        }
        void CheckSelf()
        {
            if (localPlayer == null)
            {
                if (Networking.LocalPlayer != null) localPlayer = Networking.LocalPlayer;
                else return;
            }
            var targetPlayersl_ = (DataDictionary<VRCPlayerApi, GameObject>)(object)targetPlayersl;
            var targetPlayersr_ = (DataDictionary<VRCPlayerApi, GameObject>)(object)targetPlayersr;
            var instantiatedModels_ = (DataList<GameObject>)(object)instantiatedModels;
            var instantiatedPenLight2_ = (DataList<PenLight2>)(object)instantiatedPenLight2;
            if (Check(localPlayer))
            {
                if (!targetPlayersl_.ContainsKey(localPlayer))
                {
                    model.SetActive(true);
                    var instantiatedModelLeft = Instantiate(model);
                    instantiatedModelLeft.transform.SetPositionAndRotation(localPlayer.GetBonePosition(HumanBodyBones.LeftHand), localPlayer.GetBoneRotation(HumanBodyBones.LeftHand));
                    instantiatedModels_.Add(instantiatedModelLeft);
                    var penLight2l = (PenLight2)instantiatedModelLeft.GetComponentInChildren(typeof(UdonSharpBehaviour));
                    if (penLight2l != null) instantiatedPenLight2_.Add(penLight2l);
                    targetPlayersl_.Add(localPlayer, instantiatedModelLeft);
                }
                if (!targetPlayersr_.ContainsKey(localPlayer))
                {
                    model.SetActive(true);
                    var instantiatedModelRight = Instantiate(model);
                    instantiatedModelRight.transform.position = localPlayer.GetBonePosition(HumanBodyBones.RightHand);
                    // 反转 Y 轴
                    var rotation_ = localPlayer.GetBoneRotation(HumanBodyBones.RightHand);
                    instantiatedModelRight.transform.rotation = new Quaternion(rotation_.x, rotation_.y, rotation_.z, rotation_.w);
                    instantiatedModels_.Add(instantiatedModelRight);
                    var penLight2r = (PenLight2)instantiatedModelRight.GetComponentInChildren(typeof(UdonSharpBehaviour));
                    if (penLight2r != null) instantiatedPenLight2_.Add(penLight2r);
                    targetPlayersr_.Add(localPlayer, instantiatedModelRight);
                }
                model.SetActive(false);
                // // targetPlayers = udonArrayPlus.PlayersAdd(targetPlayers, localPlayer);
                // // targetPlayers.Add(localPlayer);
                // // var list = DataList<GameObject>.New();
                // model.SetActive(true);
                // var instantiatedModelLeft = Instantiate(model);
                // var instantiatedModelRight = Instantiate(model);
                // model.SetActive(false);
                // instantiatedModelLeft.transform.position = localPlayer.GetBonePosition(HumanBodyBones.LeftHand);
                // instantiatedModelLeft.transform.rotation = localPlayer.GetBoneRotation(HumanBodyBones.LeftHand);
                // instantiatedModelRight.transform.position = localPlayer.GetBonePosition(HumanBodyBones.RightHand);
                // // 反转 Y 轴
                // var rotation_ = localPlayer.GetBoneRotation(HumanBodyBones.RightHand);
                // instantiatedModelRight.transform.rotation = new Quaternion(rotation_.x, -rotation_.y, rotation_.z, rotation_.w);
                // // list.Add(instantiatedModelLeft);
                // // list.Add(instantiatedModelRight);
                // instantiatedModels_.Add(instantiatedModelLeft);
                // instantiatedModels_.Add(instantiatedModelRight);
                // // 获取 PenLight2
                // var penLight2l = (PenLight2)instantiatedModelLeft.GetComponentInChildren(typeof(UdonSharpBehaviour));
                // var penLight2r = (PenLight2)instantiatedModelRight.GetComponentInChildren(typeof(UdonSharpBehaviour));
                // if (penLight2l != null) instantiatedPenLight2_.Add(penLight2l);
                // if (penLight2r != null) instantiatedPenLight2_.Add(penLight2r);
                // // targetPlayers_.Add(localPlayer, list);
                // targetPlayersl_.Add(localPlayer, instantiatedModelLeft);
                // targetPlayersr_.Add(localPlayer, instantiatedModelRight);
            }
            else
            {
                if (targetPlayersl_.ContainsKey(localPlayer))
                {
                    var result = targetPlayersl_.TryGetValue(localPlayer, out var model);
                    if (result)
                    {
                        var penLight2l = (PenLight2)model.GetComponentInChildren(typeof(UdonSharpBehaviour));
                        if (penLight2l != null) instantiatedPenLight2_.Remove(penLight2l);
                        instantiatedModels_.Remove(model);
                        Destroy(model);
                    }
                    targetPlayersl_.Remove(localPlayer);
                }
                if (targetPlayersr_.ContainsKey(localPlayer))
                {
                    var result = targetPlayersr_.TryGetValue(localPlayer, out var model);
                    if (result)
                    {
                        var penLight2r = (PenLight2)model.GetComponentInChildren(typeof(UdonSharpBehaviour));
                        if (penLight2r != null) instantiatedPenLight2_.Remove(penLight2r);
                        instantiatedModels_.Remove(model);
                        Destroy(model);
                    }
                    targetPlayersr_.Remove(localPlayer);
                }
                // var result = targetPlayersl_.TryGetValue(localPlayer, out var models);
                // if (!result) return;
                // for (int i = 0; i < models.Count(); i++)
                // {
                //     var result2 = models.TryGetValue(i, out var model);
                //     if (!result2) continue;
                //     instantiatedModels_.Remove(model);
                //     Destroy(model);
                // }
                // targetPlayers_.Remove(localPlayer);
            }
            // targetPlayers = (DataDictionary)(object)targetPlayers_;
            targetPlayersl = (DataDictionary)(object)targetPlayersl_;
            targetPlayersr = (DataDictionary)(object)targetPlayersr_;
            instantiatedModels = (DataList)(object)instantiatedModels_;
            instantiatedPenLight2 = (DataList)(object)instantiatedPenLight2_;
        }
        void CheckAllPlayer()
        {
            // var targetPlayers_ = (DataDictionary<VRCPlayerApi, GameObject>)(object)targetPlayers;
            // var instantiatedModels_ = (DataList<GameObject>)(object)instantiatedModels;
            // VRCPlayerApi[] players = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()];
            // players = VRCPlayerApi.GetPlayers(players);
            // foreach (var player in players)
            // {
            //     if (Check(player))
            //     {
            //         // targetPlayers = udonArrayPlus.PlayersAdd(targetPlayers, player);
            //         // if (!targetPlayers.Contains(player))
            //         // {
            //         //     targetPlayers.Add(player);
            //         // }
            //         if (targetPlayers_.ContainsKey(player)) continue;
            //         var list = DataList<GameObject>.New();
            //         model.SetActive(true);
            //         var instantiatedModelLeft = Instantiate(model);
            //         var instantiatedModelRight = Instantiate(model);
            //         model.SetActive(false);
            //         instantiatedModelLeft.transform.position = player.GetBonePosition(HumanBodyBones.LeftHand);
            //         instantiatedModelLeft.transform.rotation = player.GetBoneRotation(HumanBodyBones.LeftHand);
            //         instantiatedModelRight.transform.position = player.GetBonePosition(HumanBodyBones.RightHand);
            //         instantiatedModelRight.transform.rotation = player.GetBoneRotation(HumanBodyBones.RightHand);
            //         list.Add(instantiatedModelLeft);
            //         list.Add(instantiatedModelRight);
            //         instantiatedModels_.Add(instantiatedModelLeft);
            //         instantiatedModels_.Add(instantiatedModelRight);
            //         targetPlayers_.Add(player, list);
            //     }
            //     else
            //     {
            //         // targetPlayers = udonArrayPlus.PlayersRemove(targetPlayers, player);
            //         // if (targetPlayers.Contains(player))
            //         // {
            //         //     targetPlayers.Remove(player);
            //         // }
            //         if (!targetPlayers_.ContainsKey(player)) continue;
            //         var result = targetPlayers_.TryGetValue(player, out var models);
            //         if (!result) continue;
            //         for (int i = 0; i < models.Count(); i++)
            //         {
            //             var result2 = models.TryGetValue(i, out var model);
            //             if (!result2) continue;
            //             instantiatedModels_.Remove(model);
            //             Destroy(model);
            //         }
            //         targetPlayers_.Remove(player);
            //     }
            // }
            // targetPlayers = (DataDictionary)(object)targetPlayers_;
            // instantiatedModels = (DataList)(object)instantiatedModels_;
            var targetPlayersl_ = (DataDictionary<VRCPlayerApi, GameObject>)(object)targetPlayersl;
            var targetPlayersr_ = (DataDictionary<VRCPlayerApi, GameObject>)(object)targetPlayersr;
            var instantiatedModels_ = (DataList<GameObject>)(object)instantiatedModels;
            var instantiatedPenLight2_ = (DataList<PenLight2>)(object)instantiatedPenLight2;
            VRCPlayerApi[] players = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()];
            players = VRCPlayerApi.GetPlayers(players);
            foreach (var player in players)
            {
                if (Check(player))
                {
                    if (!targetPlayersl_.ContainsKey(player))
                    {
                        model.SetActive(true);
                        var instantiatedModelLeft = Instantiate(model);
                        instantiatedModelLeft.transform.SetPositionAndRotation(player.GetBonePosition(HumanBodyBones.LeftHand), player.GetBoneRotation(HumanBodyBones.LeftHand));
                        instantiatedModels_.Add(instantiatedModelLeft);
                        var penLight2l = (PenLight2)instantiatedModelLeft.GetComponentInChildren(typeof(UdonSharpBehaviour));
                        if (penLight2l != null) instantiatedPenLight2_.Add(penLight2l);
                        targetPlayersl_.Add(player, instantiatedModelLeft);
                    }
                    if (!targetPlayersr_.ContainsKey(player))
                    {
                        model.SetActive(true);
                        var instantiatedModelRight = Instantiate(model);
                        instantiatedModelRight.transform.position = player.GetBonePosition(HumanBodyBones.RightHand);
                        // 反转 Y 轴
                        var rotation_ = player.GetBoneRotation(HumanBodyBones.RightHand);
                        instantiatedModelRight.transform.rotation = new Quaternion(rotation_.x, rotation_.y, rotation_.z, rotation_.w);
                        instantiatedModels_.Add(instantiatedModelRight);
                        var penLight2r = (PenLight2)instantiatedModelRight.GetComponentInChildren(typeof(UdonSharpBehaviour));
                        if (penLight2r != null) instantiatedPenLight2_.Add(penLight2r);
                        targetPlayersr_.Add(player, instantiatedModelRight);
                    }
                    model.SetActive(false);
                    // // var list = DataList<GameObject>.New();
                    // model.SetActive(true);
                    // var instantiatedModelLeft = Instantiate(model);
                    // var instantiatedModelRight = Instantiate(model);
                    // model.SetActive(false);
                    // instantiatedModelLeft.transform.position = player.GetBonePosition(HumanBodyBones.LeftHand);
                    // instantiatedModelLeft.transform.rotation = player.GetBoneRotation(HumanBodyBones.LeftHand);
                    // instantiatedModelRight.transform.position = player.GetBonePosition(HumanBodyBones.RightHand);
                    // // 反转 Y 轴
                    // var rotation_ = player.GetBoneRotation(HumanBodyBones.RightHand);
                    // instantiatedModelRight.transform.rotation = new Quaternion(rotation_.x, -rotation_.y, rotation_.z, rotation_.w);
                    // // list.Add(instantiatedModelLeft);
                    // // list.Add(instantiatedModelRight);
                    // instantiatedModels_.Add(instantiatedModelLeft);
                    // instantiatedModels_.Add(instantiatedModelRight);
                    // // targetPlayers_.Add(player, list);
                    // targetPlayersl_.Add(player, instantiatedModelLeft);
                    // targetPlayersr_.Add(player, instantiatedModelRight);
                }
                else
                {
                    if (targetPlayersl_.ContainsKey(player))
                    {
                        var result = targetPlayersl_.TryGetValue(player, out var model);
                        if (result)
                        {
                            var penLight2l = (PenLight2)model.GetComponentInChildren(typeof(UdonSharpBehaviour));
                            if (penLight2l != null) instantiatedPenLight2_.Remove(penLight2l);
                            instantiatedModels_.Remove(model);
                            Destroy(model);
                        }
                        targetPlayersl_.Remove(player);
                    }
                    if (!targetPlayersr_.ContainsKey(player))
                    {
                        var result = targetPlayersr_.TryGetValue(player, out var model);
                        if (result)
                        {
                            var penLight2r = (PenLight2)model.GetComponentInChildren(typeof(UdonSharpBehaviour));
                            if (penLight2r != null) instantiatedPenLight2_.Remove(penLight2r);
                            instantiatedModels_.Remove(model);
                            Destroy(model);
                        }
                        targetPlayersr_.Remove(player);
                    }
                    // var result = targetPlayersl_.TryGetValue(player, out var model);
                    // if (!result) continue;
                    // instantiatedModels_.Remove(model);
                    // Destroy(model);
                    // targetPlayersl_.Remove(player);
                    // result = targetPlayersr_.TryGetValue(player, out model);
                    // if (!result) continue;
                    // instantiatedModels_.Remove(model);
                    // Destroy(model);
                    // targetPlayersr_.Remove(player);
                }
            }
            // targetPlayers = (DataDictionary)(object)targetPlayers_;
            targetPlayersl = (DataDictionary)(object)targetPlayersl_;
            targetPlayersr = (DataDictionary)(object)targetPlayersr_;
            instantiatedModels = (DataList)(object)instantiatedModels_;
            instantiatedPenLight2 = (DataList)(object)instantiatedPenLight2_;
        }
        public void UpdateColor()
        {
            var instantiatedPenLight2_ = (DataList<PenLight2>)(object)instantiatedPenLight2;
            for (int i = 0; i < instantiatedPenLight2_.Count(); i++)
            {
                var result = instantiatedPenLight2_.TryGetValue(i, out var penLight2);
                if (!result) continue;
                if (penLight2 == null) continue;
                penLight2.UpdateMaterials(colors[ColorNum]);
            }
        }
        public void toRequestSerialization()
        {
            if (localPlayer == null) localPlayer = Networking.LocalPlayer;
            if (localPlayer == null) return;
            if (!Networking.IsOwner(gameObject)) Networking.SetOwner(localPlayer, gameObject);
            RequestSerialization();
        }
        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            CheckAllPlayer();
        }
        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            // 从 targetPlayers 中移除离开的玩家
            // var targetPlayers_ = (DataDictionary<VRCPlayerApi, GameObject>)(object)targetPlayers;
            var targetPlayersl_ = (DataDictionary<VRCPlayerApi, GameObject>)(object)targetPlayersl;
            var targetPlayersr_ = (DataDictionary<VRCPlayerApi, GameObject>)(object)targetPlayersr;
            var instantiatedModels_ = (DataList<GameObject>)(object)instantiatedModels;
            var instantiatedPenLight2_ = (DataList<PenLight2>)(object)instantiatedPenLight2;
            if (targetPlayersl_.ContainsKey(player))
            {
                var result = targetPlayersl_.TryGetValue(player, out var model);
                if (result)
                {
                    var penLight2l = (PenLight2)model.GetComponentInChildren(typeof(UdonSharpBehaviour));
                    if (penLight2l != null) instantiatedPenLight2_.Remove(penLight2l);
                    instantiatedModels_.Remove(model);
                    Destroy(model);
                }
                targetPlayersl_.Remove(player);
            }
            if (targetPlayersr_.ContainsKey(player))
            {
                var result = targetPlayersr_.TryGetValue(player, out var model);
                if (result)
                {
                    var penLight2r = (PenLight2)model.GetComponentInChildren(typeof(UdonSharpBehaviour));
                    if (penLight2r != null) instantiatedPenLight2_.Remove(penLight2r);
                    instantiatedModels_.Remove(model);
                    Destroy(model);
                }
                targetPlayersr_.Remove(player);
            }
            // var keys = targetPlayersl_.GetKeys();
            // for (int i = 0; i < keys.Count(); i++)
            // {
            //     var result = keys.TryGetValue(i, out var key);
            //     if (!result) continue;
            //     if (key != player) continue;
            //     // var result2 = targetPlayers_.TryGetValue(key, out var models);
            //     // if (!result2) continue;
            //     // for (int j = 0; j < models.Count(); j++)
            //     // {
            //     //     var result3 = models.TryGetValue(j, out var model);
            //     //     if (!result3) continue;
            //     //     instantiatedModels_.Remove(model);
            //     //     Destroy(model);
            //     // }
            //     // targetPlayers_.Remove(key);
            //     var result2 = targetPlayersl_.TryGetValue(key, out var model);
            //     if (!result2) continue;
            //     instantiatedModels_.Remove(model);
            //     Destroy(model);
            //     targetPlayersl_.Remove(key);
            //     result2 = targetPlayersr_.TryGetValue(key, out model);
            //     if (!result2) continue;
            //     instantiatedModels_.Remove(model);
            //     Destroy(model);
            //     targetPlayersr_.Remove(key);
            // }
            // targetPlayers = (DataDictionary)(object)targetPlayers_;
            targetPlayersl = (DataDictionary)(object)targetPlayersl_;
            targetPlayersr = (DataDictionary)(object)targetPlayersr_;
            instantiatedModels = (DataList)(object)instantiatedModels_;
            instantiatedPenLight2 = (DataList)(object)instantiatedPenLight2_;
        }
    }
}