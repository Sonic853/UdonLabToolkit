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
        public int stringsFind(string[] _array, string _value)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                    return i;
            }
            return -1;
        }
        public int[] stringsFindAll(string[] _array, string _value)
        {
            int[] _index = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                {
                    _index = intsAdd(_index, i);
                }
            }
            return _index;
        }
        public int stringsIndex(string[] _array, string _value)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                    return i;
            }
            return -1;
        }
        public string[] stringsAdd(string[] _array, string _value)
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
        public string[] stringsAdd2(string[] _array, string _value)
        {
            int index = stringsIndex(_array, _value);
            if (index == -1)
            {
                return stringsAdd(_array, _value);
            }
            else
            {
                return _array;
            }
        }
        public string[] stringsRemoveIndex(string[] _array, int index)
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
        public string[] stringsRemove(string[] _array, string _value)
        {
            int index = stringsIndex(_array, _value);
            if (index == -1)
            {
                return _array;
            }
            else
            {
                return stringsRemoveIndex(_array, index);
            }
        }
        // Int
        public int intsFind(int[] _array, int _value)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                    return i;
            }
            return -1;
        }
        public int[] intsFindAll(int[] _array, int _value)
        {
            int[] _index = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                {
                    _index = intsAdd(_index, i);
                }
            }
            return _index;
        }
        public int intsIndex(int[] _array, int _value)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                    return i;
            }
            return -1;
        }
        public int[] intsAdd(int[] _array, int _value)
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
        public int[] intsAdd2(int[] _array, int _value)
        {
            int index = intsIndex(_array, _value);
            if (index == -1)
            {
                return intsAdd(_array, _value);
            }
            else
            {
                return _array;
            }
        }
        public int[] intsRemoveIndex(int[] _array, int index)
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
        public int[] intsRemove(int[] _array, int _value)
        {
            int index = intsIndex(_array, _value);
            if (index == -1)
            {
                return _array;
            }
            else
            {
                return intsRemoveIndex(_array, index);
            }
        }
        public int[] intsSort(int[] _array, bool reverse)
        {
            int[] _newArray = new int[_array.Length];
            _array.CopyTo(_newArray, 0);
            for (int i = 0; i < _newArray.Length - 1; i++)
            {
                for (int j = i + 1; j < _newArray.Length; j++)
                {
                    if (_newArray[i] > _newArray[j])
                    {
                        int temp = _newArray[i];
                        _newArray[i] = _newArray[j];
                        _newArray[j] = temp;
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
        public int floatsFind(float[] _array, float _value)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                    return i;
            }
            return -1;
        }
        public int[] floatsFindAll(float[] _array, float _value)
        {
            int[] _index = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                {
                    _index = intsAdd(_index, i);
                }
            }
            return _index;
        }
        public int floatsIndex(float[] _array, float _value)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                    return i;
            }
            return -1;
        }
        public float[] floatsAdd(float[] _array, float _value)
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
        public float[] floatsAdd2(float[] _array, float _value)
        {
            int index = floatsIndex(_array, _value);
            if (index == -1)
            {
                return floatsAdd(_array, _value);
            }
            else
            {
                return _array;
            }
        }
        public float[] floatsRemoveIndex(float[] _array, int index)
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
        public float[] floatsRemove(float[] _array, float _value)
        {
            int index = floatsIndex(_array, _value);
            if (index == -1)
            {
                return _array;
            }
            else
            {
                return floatsRemoveIndex(_array, index);
            }
        }
        public float[] floatsSort(float[] _array, bool reverse)
        {
            float[] _newArray = new float[_array.Length];
            _array.CopyTo(_newArray, 0);
            for (int i = 0; i < _newArray.Length - 1; i++)
            {
                for (int j = i + 1; j < _newArray.Length; j++)
                {
                    if (_newArray[i] > _newArray[j])
                    {
                        float temp = _newArray[i];
                        _newArray[i] = _newArray[j];
                        _newArray[j] = temp;
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
        public VRCPlayerApi[] Players()
        {
            VRCPlayerApi[] players = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()];
            players = VRCPlayerApi.GetPlayers(players);
            return players;
        }
        public VRCPlayerApi PlayersFindID(VRCPlayerApi[] players, int _id)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].playerId == _id)
                    return players[i];
            }
            return null;
        }
        public VRCPlayerApi PlayersFindName(VRCPlayerApi[] players, string _name)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].displayName == _name)
                    return players[i];
            }
            return null;
        }
        public int PlayersIndex(VRCPlayerApi[] players, VRCPlayerApi _player)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] == _player)
                    return i;
            }
            return -1;
        }
        public int PlayersIndexID(VRCPlayerApi[] players, int _id)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].playerId == _id)
                    return i;
            }
            return -1;
        }
        public int PlayersIndexName(VRCPlayerApi[] players, string _name)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].displayName == _name)
                    return i;
            }
            return -1;
        }
        public VRCPlayerApi[] PlayersAdd(VRCPlayerApi[] players, VRCPlayerApi _player)
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
        public VRCPlayerApi[] PlayersRemove(VRCPlayerApi[] players, VRCPlayerApi _player)
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
        public VRCPlayerApi PlayersFindID(int _id)
        {
            VRCPlayerApi[] players = Players();
            return PlayersFindID(players, _id);
        }
        public VRCPlayerApi PlayersFindName(string _name)
        {
            VRCPlayerApi[] players = Players();
            return PlayersFindName(players, _name);
        }
        // gameobject
        public GameObject GameObjectsFindName(GameObject[] _array, string _name)
        {
            var index = GameObjectsIndexName(_array, _name);
            return index == -1 ? null : _array[index];
        }
        public GameObject[] GameObjectsFindNameAll(GameObject[] _array, string _name)
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
        public int GameObjectsIndex(GameObject[] _array, GameObject _value)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                    return i;
            }
            return -1;
        }
        public int GameObjectsIndexName(GameObject[] _array, string _name)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].name == _name)
                    return i;
            }
            return -1;
        }
        public int[] GameObjectsIndexNameAll(GameObject[] _array, string _name)
        {
            int[] _newArray = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].name == _name)
                {
                    _newArray = intsAdd(_newArray, i);
                }
            }
            return _newArray;
        }
        public GameObject[] GameObjectsAdd(GameObject[] _array, GameObject _value)
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
        public GameObject[] GameObjectsAdd2(GameObject[] _array, GameObject _value)
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
        public GameObject[] GameObjectsRemoveIndex(GameObject[] _array, int index)
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
        public GameObject[] GameObjectsRemove(GameObject[] _array, GameObject _value)
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
        public UdonBehaviour UdonBehavioursFindName(UdonBehaviour[] _array, string _name)
        {
            var index = UdonBehavioursIndexName(_array, _name);
            return index == -1 ? null : _array[index];
        }
        public UdonBehaviour[] UdonBehavioursFindNameAll(UdonBehaviour[] _array, string _name)
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
        public int UdonBehavioursIndex(UdonBehaviour[] _array, UdonBehaviour _value)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                    return i;
            }
            return -1;
        }
        public int[] UdonBehavioursIndexProgramVariable(UdonBehaviour[] _array, string _name)
        {
            int[] _newArray = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].GetProgramVariable(_name) != null)
                {
                    _newArray = intsAdd(_newArray, i);
                }
            }
            return _newArray;
        }
        public int[] UdonBehavioursIndexProgramVariableValue(UdonBehaviour[] _array, string _name, object _value)
        {
            int[] _newArray = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].GetProgramVariable(_name) == _value)
                {
                    _newArray = intsAdd(_newArray, i);
                }
            }
            return _newArray;
        }
        public UdonBehaviour[] UdonBehavioursFindProgramVariable(UdonBehaviour[] _array, string _name)
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
        public UdonBehaviour[] UdonBehavioursFindProgramVariableValue(UdonBehaviour[] _array, string _name, object _value)
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
        public int UdonBehavioursIndexName(UdonBehaviour[] _array, string _name)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].name == _name)
                    return i;
            }
            return -1;
        }
        public int[] UdonBehavioursIndexNameAll(UdonBehaviour[] _array, string _name)
        {
            int[] _newArray = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].name == _name)
                {
                    _newArray = intsAdd(_newArray, i);
                }
            }
            return _newArray;
        }
        public UdonBehaviour[] UdonBehavioursAdd(UdonBehaviour[] _array, UdonBehaviour _value)
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
        public UdonBehaviour[] UdonBehavioursAdd2(UdonBehaviour[] _array, UdonBehaviour _value)
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
        public UdonBehaviour[] UdonBehavioursRemoveIndex(UdonBehaviour[] _array, int index)
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
        public UdonBehaviour[] UdonBehavioursRemove(UdonBehaviour[] _array, UdonBehaviour _value)
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
        public UdonSharpBehaviour UdonSharpBehavioursFindName(UdonSharpBehaviour[] _array, string _name)
        {
            var index = UdonSharpBehavioursIndexName(_array, _name);
            return index == -1 ? null : _array[index];
        }
        public UdonSharpBehaviour[] UdonSharpBehavioursFindNameAll(UdonSharpBehaviour[] _array, string _name)
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
        public int UdonSharpBehavioursIndexName(UdonSharpBehaviour[] _array, string _name)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].name == _name)
                    return i;
            }
            return -1;
        }
        public int[] UdonSharpBehavioursIndexNameAll(UdonSharpBehaviour[] _array, string _name)
        {
            int[] _newArray = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].name == _name)
                {
                    _newArray = intsAdd(_newArray, i);
                }
            }
            return _newArray;
        }
        public UdonSharpBehaviour UdonSharpBehavioursFindTypeName(UdonSharpBehaviour[] _array, string _name)
        {
            var index = UdonSharpBehavioursIndexTypeName(_array, _name);
            return index == -1 ? null : _array[index];
        }
        public UdonSharpBehaviour[] UdonSharpBehavioursFindTypeNameAll(UdonSharpBehaviour[] _array, string _name)
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
        public int UdonSharpBehavioursIndexTypeName(UdonSharpBehaviour[] _array, string _name)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].GetUdonTypeName() == _name)
                    return i;
            }
            return -1;
        }
        public int[] UdonSharpBehavioursIndexTypeNameAll(UdonSharpBehaviour[] _array, string _name)
        {
            int[] _newArray = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].GetUdonTypeName() == _name)
                {
                    _newArray = intsAdd(_newArray, i);
                }
            }
            return _newArray;
        }
        public UdonSharpBehaviour[] UdonSharpBehavioursFindProgramVariable(UdonSharpBehaviour[] _array, string _name)
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
        public UdonSharpBehaviour[] UdonSharpBehavioursFindProgramVariableValue(UdonSharpBehaviour[] _array, string _name, object _value)
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
        public int[] UdonSharpBehavioursIndexProgramVariable(UdonSharpBehaviour[] _array, string _name)
        {
            int[] _newArray = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].GetProgramVariable(_name) != null)
                {
                    _newArray = intsAdd(_newArray, i);
                }
            }
            return _newArray;
        }
        public int[] UdonSharpBehavioursIndexProgramVariableValue(UdonSharpBehaviour[] _array, string _name, object _value)
        {
            int[] _newArray = new int[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i].GetProgramVariable(_name) == _value)
                {
                    _newArray = intsAdd(_newArray, i);
                }
            }
            return _newArray;
        }
        public int UdonSharpBehavioursIndex(UdonSharpBehaviour[] _array, UdonSharpBehaviour _value)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _value)
                    return i;
            }
            return -1;
        }
        public UdonSharpBehaviour[] UdonSharpBehavioursAdd(UdonSharpBehaviour[] _array, UdonSharpBehaviour _value)
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
        public UdonSharpBehaviour[] UdonSharpBehavioursAdd2(UdonSharpBehaviour[] _array, UdonSharpBehaviour _value)
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
        public UdonSharpBehaviour[] UdonSharpBehavioursRemoveIndex(UdonSharpBehaviour[] _array, int index)
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
        public UdonSharpBehaviour[] UdonSharpBehavioursRemove(UdonSharpBehaviour[] _array, UdonSharpBehaviour _value)
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
        public bool[] boolsAdd(bool[] _array, bool _value)
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
        public bool[] boolsRemove(bool[] _array, int index)
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
