using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ajc.HIMineSweeper
{
    public class UIMainMenu : MonoBehaviour
    {
        private void Awake()
        {
            m_follow = GetComponent<SmoothFollowTarget>();
        }

        // Use this for initialization
        void Start()
        {
            m_animator = GetComponent<Animator>();
            Invoke("Show",3);
        }

        // Update is called once per frame
        void Update()
        {

        }
        /// <summary>
        /// Close the menu when :
        ///  - the Start Button is clicked
        /// </summary>
        public void Close()
        {
            m_animator.SetBool("HideBool", true);
        }
        /// <summary>
        /// Show the menu when :
        /// - The Scene starts
        /// </summary>
        public void Show()
        {
            m_follow.TeleportToPosition();
            m_animator.SetBool("HideBool", false);
        }
        private SmoothFollowTarget m_follow;
        private Animator m_animator;
    }

}
