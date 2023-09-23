
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonAnimatorController : UdonSharpBehaviour
    {
        /// <summary>
        /// Animator
        /// </summary>
        [Header("Animator")]
        [SerializeField] private Animator animator;
        /// <summary>
        /// 操作的 Layer
        /// </summary>
        [Header("操作的 Layer")]
        [SerializeField] private string currentLayerName = "Base Layer";
        /// <summary>
        /// 操作的 Layer Index
        /// </summary>
        [Header("操作的 Layer Index")]
        [SerializeField] private int currentLayerIndex = -1;
        /// <summary>
        /// 当前的 State
        /// </summary>
        [Header("当前的 State")]
        [SerializeField] private string currentStateName = "[None]";
        /// <summary>
        /// 停止用的 State
        /// </summary>
        [Header("停止用的 State")]
        [SerializeField] private string stopStateName = "[None]";
        /// <summary>
        /// 当前 Layer 的所有 State。当然，目前可能只能通过 Editor 来获取赋予进去。
        /// </summary>
        [Header("当前 Layer 的所有 State")]
        [SerializeField] private string[] stateNames;
        /// <summary>
        /// 动画控制器的速度
        /// </summary>
        private float prevSpeed = 1f;
        /// <summary>
        /// 所有 Layer 的名称
        /// </summary>
        [SerializeField] private string[] layerNames;
        /// <summary>
        /// 所有 Layer State 的名称
        /// </summary>
        [SerializeField] private string[] layerStateNames;
        /// <summary>
        /// 所有 Layer State 的 Index
        /// </summary>
        [SerializeField] private int[] layerIndex;
        /// <summary>
        /// 获取当前操作的 Animator
        /// </summary>
        /// <returns></returns>
        public Animator GetAnimator()
        {
            return animator;
        }
        /// <summary>
        /// 获取当前操作的 Layer 名称
        /// </summary>
        /// <returns></returns>
        public string GetCurrentLayerName()
        {
            return currentLayerName;
        }
        /// <summary>
        /// 获取当前操作的 Layer Index
        /// </summary>
        /// <returns></returns>
        public int GetCurrentLayerIndex()
        {
            return currentLayerIndex;
        }
        /// <summary>
        /// 获取当前操作的 State 名称
        /// </summary>
        /// <returns></returns>
        public string GetCurrentStateName()
        {
            return currentStateName;
        }
        /// <summary>
        /// 获取当前 Layer 的所有 State 名称
        /// </summary>
        /// <returns></returns>
        public string[] GetStateNames()
        {
            return stateNames;
        }
        /// <summary>
        /// 获取当前所有 Layer 的名称
        /// </summary>
        /// <returns></returns>
        public string[] GetLayerNames()
        {
            return layerNames;
        }
        /// <summary>
        /// 获取所有 Layer State 的名称
        /// </summary>
        /// <returns></returns>
        public string[] GetLayerStateNames()
        {
            return layerStateNames;
        }
        void Start()
        {
            if (animator == null)
                animator = GetComponent<Animator>();
            if (animator == null)
                return;
            currentLayerIndex = animator.GetLayerIndex(currentLayerName);
            prevSpeed = animator.speed;
            // Debug.Log(animator.GetLayerIndex("NotFound"));
            // // 输出所有 Layer 的名称
            // layerNames = new string[animator.layerCount];
            // for (int i = 0; i < animator.layerCount; i++)
            // {
            //     layerNames[i] = animator.GetLayerName(i);
            //     Debug.Log(layerNames[i]);
            // }
        }
        public void Stop()
        {
            PlayState($"{stopStateName}", 0f);
        }
        public void PlayState(string stateName, float normalizedTime)
        {
            if (animator == null)
                return;
            currentLayerIndex = animator.GetLayerIndex(currentLayerName);
            if (currentLayerIndex == -1)
                return;
            animator.speed = prevSpeed;
            animator.Play($"{currentLayerName}.{stateName}", currentLayerIndex, normalizedTime);
        }
        public void PlayLayerState(string layerStateName, int index, float normalizedTime)
        {
            if (animator == null)
                return;
            animator.speed = prevSpeed;
            animator.Play(layerStateName, index, normalizedTime);
        }
        public void PlayLSIndex(int index, float normalizedTime)
        {
            if (animator == null)
                return;
            animator.speed = prevSpeed;
            animator.Play(layerStateNames[index], layerIndex[index], normalizedTime);
        }
        // public void PlayLSName(string layerStateName, float normalizedTime)
        // {
        //     animator.speed = prevSpeed;
        // }
        public void Pause()
        {
            if (animator == null)
                return;
            if (prevSpeed == animator.speed)
            {
                animator.speed = 0f;
            }
            else
            {
                animator.speed = prevSpeed;
            }
        }
        public bool isPaused()
        {
            if (animator == null)
                return false;
            return animator.speed == 0f;
        }
        public void PauseOn()
        {
            if (animator == null)
                return;
            animator.speed = 0f;
        }
        public void PauseOff()
        {
            if (animator == null)
                return;
            animator.speed = prevSpeed;
        }
    }
}
