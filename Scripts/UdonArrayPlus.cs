using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonArrayPlus : UdonSharpBehaviour
    {
        // String
        public static int StringsFind(string[] _array, string _value)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                    return i;
            }
            return -1;
        }
        public static int[] StringsFindAll(string[] _array, string _value)
        {
            int[] _index = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                {
                    _index = IntsAdd(_index, i);
                }
            }
            return _index;
        }
        public static int StringsIndex(string[] _array, string _value)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                    return i;
            }
            return -1;
        }
        public static string[] StringsAdd(string[] _array, string _value)
        {
            string[] _newArray = new string[_array.Length + 1];
            // for (int i = 0; i < _array.Length; i++)
            // {
            //     _newArray[i] = _array[i];
            // }
            Array.Copy(_array, _newArray, _array.Length);
            _newArray[_array.Length] = _value;
            return _newArray;
        }
        public static string[] StringsAdd2(string[] _array, string _value)
        {
            int index = StringsIndex(_array, _value);
            if (index == -1)
            {
                return StringsAdd(_array, _value);
            }
            else
            {
                return _array;
            }
        }
        public static string[] StringsRemoveIndex(string[] _array, int index)
        {
            if (index < 0 || index >= _array.Length)
            {
                return _array;
            }
            else
            {
                string[] _newArray = new string[_array.Length - 1];
                for (int i = 0; i < index; i++)
                {
                    _newArray[i] = _array[i];
                }
                for (int i = index; i < _array.Length - 1; i++)
                {
                    _newArray[i] = _array[i + 1];
                }
                return _newArray;
            }
        }
        public static string[] StringsRemove(string[] _array, string _value)
        {
            int index = StringsIndex(_array, _value);
            if (index == -1)
            {
                return _array;
            }
            else
            {
                return StringsRemoveIndex(_array, index);
            }
        }
        // Int
        public static int IntsFind(int[] _array, int _value)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                    return i;
            }
            return -1;
        }
        public static int[] IntsFindAll(int[] _array, int _value)
        {
            int[] _index = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                {
                    _index = IntsAdd(_index, i);
                }
            }
            return _index;
        }
        public static int IntsIndex(int[] _array, int _value)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                    return i;
            }
            return -1;
        }
        public static int[] IntsAdd(int[] _array, int _value)
        {
            int[] _newArray = new int[_array.Length + 1];
            // for (int i = 0; i < _array.Length; i++)
            // {
            //     _newArray[i] = _array[i];
            // }
            Array.Copy(_array, _newArray, _array.Length);
            _newArray[_array.Length] = _value;
            return _newArray;
        }
        public static int[] IntsAdd2(int[] _array, int _value)
        {
            int index = IntsIndex(_array, _value);
            if (index == -1)
            {
                return IntsAdd(_array, _value);
            }
            else
            {
                return _array;
            }
        }
        public static int[] IntsRemoveIndex(int[] _array, int index)
        {
            if (index < 0 || index >= _array.Length)
            {
                return _array;
            }
            else
            {
                int[] _newArray = new int[_array.Length - 1];
                for (int i = 0; i < index; i++)
                {
                    _newArray[i] = _array[i];
                }
                for (int i = index; i < _array.Length - 1; i++)
                {
                    _newArray[i] = _array[i + 1];
                }
                return _newArray;
            }
        }
        public static int[] IntsRemove(int[] _array, int _value)
        {
            int index = IntsIndex(_array, _value);
            if (index == -1)
            {
                return _array;
            }
            else
            {
                return IntsRemoveIndex(_array, index);
            }
        }
        public static int[] IntsSort(int[] _array, bool reverse)
        {
            int[] _newArray = new int[_array.Length];
            _array.CopyTo(_newArray, 0);
            for (int i = 0; i < _newArray.Length - 1; i++)
            {
                for (int j = i + 1; j < _newArray.Length; j++)
                {
                    if (_newArray[i] > _newArray[j])
                    {
                        (_newArray[j], _newArray[i]) = (_newArray[i], _newArray[j]);
                    }
                }
            }
            if (reverse)
            {
                Array.Reverse(_newArray);
            }
            return _newArray;
        }
        // Float
        public static int FloatsFind(float[] _array, float _value)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                    return i;
            }
            return -1;
        }
        public static int[] FloatsFindAll(float[] _array, float _value)
        {
            int[] _index = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                {
                    _index = IntsAdd(_index, i);
                }
            }
            return _index;
        }
        public static int FloatsIndex(float[] _array, float _value)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                    return i;
            }
            return -1;
        }
        public static float[] FloatsAdd(float[] _array, float _value)
        {
            float[] _newArray = new float[_array.Length + 1];
            // for (int i = 0; i < _array.Length; i++)
            // {
            //     _newArray[i] = _array[i];
            // }
            Array.Copy(_array, _newArray, _array.Length);
            _newArray[_array.Length] = _value;
            return _newArray;
        }
        public static float[] FloatsAdd2(float[] _array, float _value)
        {
            int index = FloatsIndex(_array, _value);
            if (index == -1)
            {
                return FloatsAdd(_array, _value);
            }
            else
            {
                return _array;
            }
        }
        public static float[] FloatsRemoveIndex(float[] _array, int index)
        {
            if (index < 0 || index >= _array.Length)
            {
                return _array;
            }
            else
            {
                float[] _newArray = new float[_array.Length - 1];
                for (int i = 0; i < index; i++)
                {
                    _newArray[i] = _array[i];
                }
                for (int i = index; i < _array.Length - 1; i++)
                {
                    _newArray[i] = _array[i + 1];
                }
                return _newArray;
            }
        }
        public static float[] FloatsRemove(float[] _array, float _value)
        {
            int index = FloatsIndex(_array, _value);
            if (index == -1)
            {
                return _array;
            }
            else
            {
                return FloatsRemoveIndex(_array, index);
            }
        }
        public static float[] FloatsSort(float[] _array, bool reverse)
        {
            float[] _newArray = new float[_array.Length];
            _array.CopyTo(_newArray, 0);
            for (int i = 0; i < _newArray.Length - 1; i++)
            {
                for (int j = i + 1; j < _newArray.Length; j++)
                {
                    if (_newArray[i] > _newArray[j])
                    {
                        (_newArray[j], _newArray[i]) = (_newArray[i], _newArray[j]);
                    }
                }
            }
            if (reverse)
            {
                Array.Reverse(_newArray);
            }
            return _newArray;
        }
        // VRCPlayerApi
        public static VRCPlayerApi[] Players()
        {
            VRCPlayerApi[] players = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()];
            players = VRCPlayerApi.GetPlayers(players);
            return players;
        }
        public static VRCPlayerApi PlayersFindID(VRCPlayerApi[] players, int _id)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].playerId == _id)
                    return players[i];
            }
            return null;
        }
        public static VRCPlayerApi PlayersFindName(VRCPlayerApi[] players, string _name)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].displayName == _name)
                    return players[i];
            }
            return null;
        }
        public static int PlayersIndex(VRCPlayerApi[] players, VRCPlayerApi _player)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] == _player)
                    return i;
            }
            return -1;
        }
        public static int PlayersIndexID(VRCPlayerApi[] players, int _id)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].playerId == _id)
                    return i;
            }
            return -1;
        }
        public static int PlayersIndexName(VRCPlayerApi[] players, string _name)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].displayName == _name)
                    return i;
            }
            return -1;
        }
        public static VRCPlayerApi[] PlayersAdd(VRCPlayerApi[] players, VRCPlayerApi _player)
        {
            int index = PlayersIndex(players, _player);
            if (index == -1)
            {
                VRCPlayerApi[] newPlayers = new VRCPlayerApi[players.Length + 1];
                // for (int i = 0; i < players.Length; i++)
                // {
                //     newPlayers[i] = players[i];
                // }
                Array.Copy(players, newPlayers, players.Length);
                newPlayers[players.Length] = _player;
                return newPlayers;
            }
            else
            {
                return players;
            }
        }
        public static VRCPlayerApi[] PlayersRemove(VRCPlayerApi[] players, VRCPlayerApi _player)
        {
            int index = PlayersIndex(players, _player);
            if (index == -1)
            {
                return players;
            }
            else
            {
                VRCPlayerApi[] newPlayers = new VRCPlayerApi[players.Length - 1];
                for (int i = 0; i < index; i++)
                {
                    newPlayers[i] = players[i];
                }
                for (int i = index; i < players.Length - 1; i++)
                {
                    newPlayers[i] = players[i + 1];
                }
                return newPlayers;
            }
        }
        public static VRCPlayerApi PlayersFindID(int _id)
        {
            VRCPlayerApi[] players = Players();
            return PlayersFindID(players, _id);
        }
        public static VRCPlayerApi PlayersFindName(string _name)
        {
            VRCPlayerApi[] players = Players();
            return PlayersFindName(players, _name);
        }
        // gameobject
        public static GameObject GameObjectsFindName(GameObject[] _array, string _name)
        {
            var index = GameObjectsIndexName(_array, _name);
            return index == -1 ? null : _array[index];
        }
        public static GameObject[] GameObjectsFindNameAll(GameObject[] _array, string _name)
        {
            GameObject[] _newArray = new GameObject[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].name == _name)
                {
                    _newArray = GameObjectsAdd(_newArray, _array[i]);
                }
            }
            return _newArray;
        }
        public static int GameObjectsIndex(GameObject[] _array, GameObject _value)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                    return i;
            }
            return -1;
        }
        public static int GameObjectsIndexName(GameObject[] _array, string _name)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].name == _name)
                    return i;
            }
            return -1;
        }
        public static int[] GameObjectsIndexNameAll(GameObject[] _array, string _name)
        {
            int[] _newArray = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].name == _name)
                {
                    _newArray = IntsAdd(_newArray, i);
                }
            }
            return _newArray;
        }
        public static GameObject[] GameObjectsAdd(GameObject[] _array, GameObject _value)
        {
            GameObject[] _newArray = new GameObject[_array.Length + 1];
            // for (int i = 0; i < _array.Length; i++)
            // {
            //     _newArray[i] = _array[i];
            // }
            Array.Copy(_array, _newArray, _array.Length);
            _newArray[_array.Length] = _value;
            return _newArray;
        }
        public static GameObject[] GameObjectsAdd2(GameObject[] _array, GameObject _value)
        {
            int index = GameObjectsIndex(_array, _value);
            if (index == -1)
            {
                return GameObjectsAdd(_array, _value);
            }
            else
            {
                return _array;
            }
        }
        public static GameObject[] GameObjectsRemoveIndex(GameObject[] _array, int index)
        {
            if (index < 0 || index >= _array.Length)
            {
                return _array;
            }
            else
            {
                GameObject[] _newArray = new GameObject[_array.Length - 1];
                for (int i = 0; i < index; i++)
                {
                    _newArray[i] = _array[i];
                }
                for (int i = index; i < _array.Length - 1; i++)
                {
                    _newArray[i] = _array[i + 1];
                }
                return _newArray;
            }
        }
        public static GameObject[] GameObjectsRemove(GameObject[] _array, GameObject _value)
        {
            int index = GameObjectsIndex(_array, _value);
            if (index == -1)
            {
                return _array;
            }
            else
            {
                return GameObjectsRemoveIndex(_array, index);
            }
        }
        // UdonBehaviour
        public static UdonBehaviour UdonBehavioursFindName(UdonBehaviour[] _array, string _name)
        {
            var index = UdonBehavioursIndexName(_array, _name);
            return index == -1 ? null : _array[index];
        }
        public static UdonBehaviour[] UdonBehavioursFindNameAll(UdonBehaviour[] _array, string _name)
        {
            UdonBehaviour[] _newArray = new UdonBehaviour[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].name == _name)
                {
                    _newArray = UdonBehavioursAdd(_newArray, _array[i]);
                }
            }
            return _newArray;
        }
        public static int UdonBehavioursIndex(UdonBehaviour[] _array, UdonBehaviour _value)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                    return i;
            }
            return -1;
        }
        public static int[] UdonBehavioursIndexProgramVariable(UdonBehaviour[] _array, string _name)
        {
            int[] _newArray = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].GetProgramVariable(_name) != null)
                {
                    _newArray = IntsAdd(_newArray, i);
                }
            }
            return _newArray;
        }
        public static int[] UdonBehavioursIndexProgramVariableValue(UdonBehaviour[] _array, string _name, object _value)
        {
            int[] _newArray = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].GetProgramVariable(_name) == _value)
                {
                    _newArray = IntsAdd(_newArray, i);
                }
            }
            return _newArray;
        }
        public static UdonBehaviour[] UdonBehavioursFindProgramVariable(UdonBehaviour[] _array, string _name)
        {
            UdonBehaviour[] _newArray = new UdonBehaviour[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].GetProgramVariable(_name) != null)
                {
                    _newArray = UdonBehavioursAdd(_newArray, _array[i]);
                }
            }
            return _newArray;
        }
        public static UdonBehaviour[] UdonBehavioursFindProgramVariableValue(UdonBehaviour[] _array, string _name, object _value)
        {
            UdonBehaviour[] _newArray = new UdonBehaviour[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].GetProgramVariable(_name) == _value)
                {
                    _newArray = UdonBehavioursAdd(_newArray, _array[i]);
                }
            }
            return _newArray;
        }
        public static int UdonBehavioursIndexName(UdonBehaviour[] _array, string _name)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].name == _name)
                    return i;
            }
            return -1;
        }
        public static int[] UdonBehavioursIndexNameAll(UdonBehaviour[] _array, string _name)
        {
            int[] _newArray = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].name == _name)
                {
                    _newArray = IntsAdd(_newArray, i);
                }
            }
            return _newArray;
        }
        public static UdonBehaviour[] UdonBehavioursAdd(UdonBehaviour[] _array, UdonBehaviour _value)
        {
            UdonBehaviour[] _newArray = new UdonBehaviour[_array.Length + 1];
            // for (int i = 0; i < _array.Length; i++)
            // {
            //     _newArray[i] = _array[i];
            // }
            Array.Copy(_array, _newArray, _array.Length);
            _newArray[_array.Length] = _value;
            return _newArray;
        }
        public static UdonBehaviour[] UdonBehavioursAdd2(UdonBehaviour[] _array, UdonBehaviour _value)
        {
            int index = UdonBehavioursIndex(_array, _value);
            if (index == -1)
            {
                return UdonBehavioursAdd(_array, _value);
            }
            else
            {
                return _array;
            }
        }
        public static UdonBehaviour[] UdonBehavioursRemoveIndex(UdonBehaviour[] _array, int index)
        {
            if (index < 0 || index >= _array.Length)
            {
                return _array;
            }
            else
            {
                UdonBehaviour[] _newArray = new UdonBehaviour[_array.Length - 1];
                for (int i = 0; i < index; i++)
                {
                    _newArray[i] = _array[i];
                }
                for (int i = index; i < _array.Length - 1; i++)
                {
                    _newArray[i] = _array[i + 1];
                }
                return _newArray;
            }
        }
        public static UdonBehaviour[] UdonBehavioursRemove(UdonBehaviour[] _array, UdonBehaviour _value)
        {
            int index = UdonBehavioursIndex(_array, _value);
            if (index == -1)
            {
                return _array;
            }
            else
            {
                return UdonBehavioursRemoveIndex(_array, index);
            }
        }
        // UdonSharpBehaviour
        public static UdonSharpBehaviour UdonSharpBehavioursFindName(UdonSharpBehaviour[] _array, string _name)
        {
            var index = UdonSharpBehavioursIndexName(_array, _name);
            return index == -1 ? null : _array[index];
        }
        public static UdonSharpBehaviour[] UdonSharpBehavioursFindNameAll(UdonSharpBehaviour[] _array, string _name)
        {
            UdonSharpBehaviour[] _newArray = new UdonSharpBehaviour[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].name == _name)
                {
                    _newArray = UdonSharpBehavioursAdd(_newArray, _array[i]);
                }
            }
            return _newArray;
        }
        public static int UdonSharpBehavioursIndexName(UdonSharpBehaviour[] _array, string _name)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].name == _name)
                    return i;
            }
            return -1;
        }
        public static int[] UdonSharpBehavioursIndexNameAll(UdonSharpBehaviour[] _array, string _name)
        {
            int[] _newArray = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].name == _name)
                {
                    _newArray = IntsAdd(_newArray, i);
                }
            }
            return _newArray;
        }
        public static UdonSharpBehaviour UdonSharpBehavioursFindTypeName(UdonSharpBehaviour[] _array, string _name)
        {
            var index = UdonSharpBehavioursIndexTypeName(_array, _name);
            return index == -1 ? null : _array[index];
        }
        public static UdonSharpBehaviour[] UdonSharpBehavioursFindTypeNameAll(UdonSharpBehaviour[] _array, string _name)
        {
            UdonSharpBehaviour[] _newArray = new UdonSharpBehaviour[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].GetUdonTypeName() == _name)
                {
                    _newArray = UdonSharpBehavioursAdd(_newArray, _array[i]);
                }
            }
            return _newArray;
        }
        public static int UdonSharpBehavioursIndexTypeName(UdonSharpBehaviour[] _array, string _name)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].GetUdonTypeName() == _name)
                    return i;
            }
            return -1;
        }
        public static int[] UdonSharpBehavioursIndexTypeNameAll(UdonSharpBehaviour[] _array, string _name)
        {
            int[] _newArray = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].GetUdonTypeName() == _name)
                {
                    _newArray = IntsAdd(_newArray, i);
                }
            }
            return _newArray;
        }
        public static UdonSharpBehaviour[] UdonSharpBehavioursFindProgramVariable(UdonSharpBehaviour[] _array, string _name)
        {
            UdonSharpBehaviour[] _newArray = new UdonSharpBehaviour[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].GetProgramVariable(_name) != null)
                {
                    _newArray = UdonSharpBehavioursAdd(_newArray, _array[i]);
                }
            }
            return _newArray;
        }
        public static UdonSharpBehaviour[] UdonSharpBehavioursFindProgramVariableValue(UdonSharpBehaviour[] _array, string _name, object _value)
        {
            UdonSharpBehaviour[] _newArray = new UdonSharpBehaviour[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].GetProgramVariable(_name) == _value)
                {
                    _newArray = UdonSharpBehavioursAdd(_newArray, _array[i]);
                }
            }
            return _newArray;
        }
        public static int[] UdonSharpBehavioursIndexProgramVariable(UdonSharpBehaviour[] _array, string _name)
        {
            int[] _newArray = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].GetProgramVariable(_name) != null)
                {
                    _newArray = IntsAdd(_newArray, i);
                }
            }
            return _newArray;
        }
        public static int[] UdonSharpBehavioursIndexProgramVariableValue(UdonSharpBehaviour[] _array, string _name, object _value)
        {
            int[] _newArray = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].GetProgramVariable(_name) == _value)
                {
                    _newArray = IntsAdd(_newArray, i);
                }
            }
            return _newArray;
        }
        public static int UdonSharpBehavioursIndex(UdonSharpBehaviour[] _array, UdonSharpBehaviour _value)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                    return i;
            }
            return -1;
        }
        public static UdonSharpBehaviour[] UdonSharpBehavioursAdd(UdonSharpBehaviour[] _array, UdonSharpBehaviour _value)
        {
            UdonSharpBehaviour[] _newArray = new UdonSharpBehaviour[_array.Length + 1];
            // for (int i = 0; i < _array.Length; i++)
            // {
            //     _newArray[i] = _array[i];
            // }
            Array.Copy(_array, _newArray, _array.Length);
            _newArray[_array.Length] = _value;
            return _newArray;
        }
        public static UdonSharpBehaviour[] UdonSharpBehavioursAdd2(UdonSharpBehaviour[] _array, UdonSharpBehaviour _value)
        {
            int index = UdonSharpBehavioursIndex(_array, _value);
            if (index == -1)
            {
                return UdonSharpBehavioursAdd(_array, _value);
            }
            else
            {
                return _array;
            }
        }
        public static UdonSharpBehaviour[] UdonSharpBehavioursRemoveIndex(UdonSharpBehaviour[] _array, int index)
        {
            if (index < 0 || index >= _array.Length)
            {
                return _array;
            }
            else
            {
                UdonSharpBehaviour[] _newArray = new UdonSharpBehaviour[_array.Length - 1];
                for (int i = 0; i < index; i++)
                {
                    _newArray[i] = _array[i];
                }
                for (int i = index; i < _array.Length - 1; i++)
                {
                    _newArray[i] = _array[i + 1];
                }
                return _newArray;
            }
        }
        public static UdonSharpBehaviour[] UdonSharpBehavioursRemove(UdonSharpBehaviour[] _array, UdonSharpBehaviour _value)
        {
            int index = UdonSharpBehavioursIndex(_array, _value);
            if (index == -1)
            {
                return _array;
            }
            else
            {
                return UdonSharpBehavioursRemoveIndex(_array, index);
            }
        }
        // bool
        public static bool[] BoolsAdd(bool[] _array, bool _value)
        {
            bool[] _newArray = new bool[_array.Length + 1];
            // for (int i = 0; i < _array.Length; i++)
            // {
            //     _newArray[i] = _array[i];
            // }
            Array.Copy(_array, _newArray, _array.Length);
            _newArray[_array.Length] = _value;
            return _newArray;
        }
        public static bool[] BoolsRemove(bool[] _array, int index)
        {
            if (index < 0 || index >= _array.Length)
            {
                return _array;
            }
            else
            {
                bool[] _newArray = new bool[_array.Length - 1];
                for (int i = 0; i < index; i++)
                {
                    _newArray[i] = _array[i];
                }
                for (int i = index; i < _array.Length - 1; i++)
                {
                    _newArray[i] = _array[i + 1];
                }
                return _newArray;
            }
        }
    }
}
