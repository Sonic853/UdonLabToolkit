
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonPlayerSelector : UdonSharpBehaviour
    {
        // [SerializeField] private UdonArrayPlus udonArrayPlus;
        VRCPlayerApi[] players = new VRCPlayerApi[0];
        [SerializeField] private GameObject content;
        [SerializeField] private GameObject prefab;
        [SerializeField] private UdonBehaviour[] udonBehaviours;
        [SerializeField] private string[] functionNames;
        [SerializeField] private string[] valueNames;
        void OnEnable()
        {
            ResetList();
        }
        // 当玩家进入时
        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            if (player.isLocal)
                return;
            ResetList();
        }
        // 当玩家离开时
        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            if (player.isLocal)
                return;
            ResetList();
        }
        void ResetList()
        {
            var players = UdonArrayPlus.Players();
            // 清空 content
            for (int i = 0; i < content.transform.childCount; i++)
            {
                Destroy(content.transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < players.Length; i++)
            {
                InsertPlayer(players[i].displayName);
            }
        }
        void InsertPlayer(string displayName)
        {
            var values = new string[udonBehaviours.Length];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = displayName;
            }
            var item = Instantiate(prefab, content.transform);
            var text = (Text)item.GetComponentInChildren(typeof(Text));
            text.text = displayName;
            var _udonSharpBehaviour = (UdonSharpBehaviour)item.GetComponent(typeof(UdonSharpBehaviour));
            if (_udonSharpBehaviour != null
             && _udonSharpBehaviour.GetUdonTypeName() == "UdonLab.Toolkit.UdonSendFunctionsWithString")
            {
                var _udonInteractFunctionWithString = (UdonLab.Toolkit.UdonSendFunctionsWithString)_udonSharpBehaviour;
                _udonInteractFunctionWithString.udonBehaviours = udonBehaviours;
                _udonInteractFunctionWithString.functionNames = functionNames;
                _udonInteractFunctionWithString.valueNames = valueNames;
                _udonInteractFunctionWithString.values = values;
            }
        }
        void RemovePlayer(string displayName)
        {
            for (int i = 0; i < content.transform.childCount; i++)
            {
                var item = content.transform.GetChild(i);
                var text = (Text)item.GetComponentInChildren(typeof(Text));
                if (text.text == displayName)
                {
                    Destroy(item.gameObject);
                    break;
                }
            }
        }
    }
}
