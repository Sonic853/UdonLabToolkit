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
            for (int i = 0; i < _array.Length; i++)
            {
                _newArray[i] = _array[i];
            }
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
            for (int i = 0; i < _array.Length; i++)
            {
                _newArray[i] = _array[i];
            }
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
            for (int i = 0; i < _array.Length; i++)
            {
                _newArray[i] = _array[i];
            }
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
        public VRCPlayerApi[] PlayersArray()
        {
            VRCPlayerApi[] players = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()];
            players = VRCPlayerApi.GetPlayers(players);
            return players;
        }
        public VRCPlayerApi PlayersArrayGetID(VRCPlayerApi[] players, int _id)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].playerId == _id)
                    return players[i];
            }
            return null;
        }
        public VRCPlayerApi PlayersArrayGetName(VRCPlayerApi[] players, string _name)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].displayName == _name)
                    return players[i];
            }
            return null;
        }
        public int PlayersArrayIndex(VRCPlayerApi[] players, VRCPlayerApi _player)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] == _player)
                    return i;
            }
            return -1;
        }
        public int PlayersArrayIndexID(VRCPlayerApi[] players, int _id)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].playerId == _id)
                    return i;
            }
            return -1;
        }
        public int PlayersArrayIndexName(VRCPlayerApi[] players, string _name)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].displayName == _name)
                    return i;
            }
            return -1;
        }
        public VRCPlayerApi[] PlayersArrayAdd(VRCPlayerApi[] players, VRCPlayerApi _player)
        {
            int index = PlayersArrayIndex(players, _player);
            if (index == -1)
            {
                VRCPlayerApi[] newPlayers = new VRCPlayerApi[players.Length + 1];
                for (int i = 0; i < players.Length; i++)
                {
                    newPlayers[i] = players[i];
                }
                newPlayers[players.Length] = _player;
                return newPlayers;
            }
            else
            {
                return players;
            }
        }
        public VRCPlayerApi[] PlayersArrayRemove(VRCPlayerApi[] players, VRCPlayerApi _player)
        {
            int index = PlayersArrayIndex(players, _player);
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
        public VRCPlayerApi PlayersGetID(int _id)
        {
            VRCPlayerApi[] players = PlayersArray();
            return PlayersArrayGetID(players, _id);
        }
        public VRCPlayerApi PlayersGetName(string _name)
        {
            VRCPlayerApi[] players = PlayersArray();
            return PlayersArrayGetName(players, _name);
        }
        // bool
        public bool[] boolsAdd(bool[] _array, bool _value)
        {
            bool[] _newArray = new bool[_array.Length + 1];
            for (int i = 0; i < _array.Length; i++)
            {
                _newArray[i] = _array[i];
            }
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
