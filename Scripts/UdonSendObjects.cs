
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonSendObjects : UdonSharpBehaviour
    {
        /// <summary>
        /// 需要设置的GameObject
        /// </summary>
        [Header("需要设置的GameObject")]
        [SerializeField] private GameObject[] gameObjects;
        /// <summary>
        /// 每个GameObject的启用状态
        /// </summary>
        private bool[] gameObjectsActive;
        /// <summary>
        /// 设置GameObject方式：0：反复反转 1：全部启/禁用 2：启用 3：禁用 4：反转
        /// </summary>
        [Header("设置GameObject方式：0：反复反转 1：全部启/禁用 2：启用 3：禁用 4：反转")]
        [Range(0, 4)]
        [SerializeField] private int setActive = 0;
        /// <summary>
        /// 是否已启用（setActive 为 1 时可用）
        /// </summary>
        [Header("是否已启用（setActive 为 1 时可用）")]
        [SerializeField] private bool isActive = true;
        /// <summary>
        /// 只允许触发一次
        /// </summary>
        [Header("只允许触发一次")]
        [SerializeField] private bool isOnce = false;
        /// <summary>
        /// 是否已触发
        /// </summary>
        [NonSerialized] private bool _isSended = false;
        void Start()
        {
            gameObjectsActive = new bool[gameObjects.Length];
            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (gameObjects[i] == null)
                    continue;
                gameObjectsActive[i] = gameObjects[i].activeSelf;
                if (setActive == 1) gameObjects[i].SetActive(isActive);
            }
        }
        public void SendFunctions()
        {
            if (isOnce && _isSended)
                return;
            if (setActive == 1)
            {
                isActive = !isActive;
                foreach (var _gameObject in gameObjects)
                {
                    if (_gameObject == null)
                        continue;
                    _gameObject.SetActive(isActive);
                }
            }
            else for (int i = 0; i < gameObjects.Length; i++)
                {
                    if (gameObjects[i] == null)
                        continue;
                    switch (setActive)
                    {
                        case 0:
                            gameObjects[i].SetActive(!gameObjects[i].activeSelf);
                            break;
                        case 2:
                            gameObjects[i].SetActive(true);
                            break;
                        case 3:
                            gameObjects[i].SetActive(false);
                            break;
                        case 4:
                            gameObjects[i].SetActive(!gameObjectsActive[i]);
                            break;
                    }
                }
            _isSended = true;
        }
    }
}
